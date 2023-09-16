using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour 
{
    HorrorProject playerInput;
    private HorrorProject.OnFootActions onFoot;

    [Header("Position")]
    [SerializeField] public float amount = 0.015f;
    [SerializeField] public float maxAmount = 0.03f;
    [SerializeField] public float smoothAmount = 4f;
    [Header("Sway")]
    [SerializeField] private float smooth = 8f;
    [SerializeField] public float intensity = 0.5f;
    [SerializeField] private float multiplier = 1;
    [SerializeField] private float maxSway = 0;
    [Header("Tilt")]
    [SerializeField] private float rotationAmount = 4;
    [SerializeField] public float maxRotationAmount = 5;
    [SerializeField] private float smoothRotation = 12;
    [Space]
    // [X == False ==> Heavy gun]  [Y == False ==> Medium gun]  X or Y must be false to work properly
    [SerializeField] public bool rotationX = false;
    [SerializeField] public bool rotationY = true;
    [SerializeField] public bool rotationZ = true;
    [Space]
    [Header("Breathe")]
    [SerializeField] Transform weaponParent;
    [SerializeField] float breathForce = 1; // 2
    [SerializeField] float breathForceRotating = 2; // 10
    [SerializeField] float breathForceSprinting = 2; // 6
    [Space]
    [Header("Extra Options")]
    [SerializeField] public bool isMine;

    // Internal privates
    private Vector3 initialPosition;
    private Quaternion originRotation;
    private float movementCounter;
    private float idleCounter;

    // Mouse movement calculator
    Vector2 input;
    Vector2 mouseInput;
    Vector2 padInput;

    // Breath internal
    private Vector3 weaponParentOrigin;
    Vector3 targetWeaponBobPosition;

    // Enable the player input (to use the new input system!)
    private void OnEnable()
    {
        playerInput = new HorrorProject();
        playerInput.Enable();
    }

    private void Start()
    {
        initialPosition = transform.localPosition;
        // Get the rotation of the object before the game starts to make it appear rotated
        originRotation = transform.localRotation;

        weaponParentOrigin = weaponParent.transform.localPosition;
    }
    private void Update()
    {
        // If it's activated the movement physics work, if it's not, it doesn't
        //  (Basically made to save resouces with npcs that also have guns...)
        if (!isMine) return;

        if (isMine)
        {
            UpdatePosition();
            UpdateSway();
            UpdateTilt();
            UpdateBreath();
        }
    }


    ////////////////////////////////////    IMPORTANT CODE     ////////////////////////////////////


    private void MouseCalculate()
    {
        /// Old Way
            //float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
            //float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;
        /// New input system way! 
            // Mouse
        mouseInput = playerInput.OnFoot.Look.ReadValue<Vector2>();

            // Gamepad
        padInput = playerInput.OnFootGamePad.Look.ReadValue<Vector2>();

        input = mouseInput + padInput; // As Keyboard and Gamepad won't be used at the same time by the same player, I just add them together in the same variable!
    }


    /// Move gun position
    private void UpdatePosition()
    {
        ///// Calculate target Position ==> [To change movement direction change "amount" sign!]
        float moveX = Mathf.Clamp(input.x * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(input.y * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

    /// Sway Movement (Rotation)
    private void UpdateSway()
    {
        ///// Get mouse input!!!
        MouseCalculate();
        ///// Calculate target rotation ==> [To change movement direction change "intensity" sign!]
        /// Old Way
        //Quaternion rotationX = Quaternion.AngleAxis(-intensity * mouseX, Vector3.up);
        //Quaternion rotationY = Quaternion.AngleAxis(-intensity * mouseY, Vector3.right);
        /// New input system way! 
        Quaternion rotationX = Quaternion.AngleAxis(intensity * input.x, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(intensity * input.y, Vector3.right);
        Quaternion targetRotation = originRotation * rotationX * rotationY;

        ///// Apply rotation towards target 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    /// Tilt Sway Rotation Movement
    private void UpdateTilt()
    {
        ///// Calculate target rotation ==> [To change movement direction change "rotationAmount" sign!]
        float tiltX = Mathf.Clamp(input.x * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(input.y * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3
            (
            // [ -tiltX / -tiltY ] ==> Change rotation direction!
            //      [?] ==> If rotationX == true ==> it uses -tiltX amount inserted in script to rotate
            //                  If not, it uses 0 value ==> It doesn't work!
            rotationX == true   ?   tiltX : 0f,
            rotationY == true   ?   tiltY : 0f,
            rotationZ == true   ?   tiltY : 0f
            ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * originRotation, smooth * Time.deltaTime);
    }

    /// Breath Idle Bobbing
    private void UpdateBreath()
    {
        // When player doesn't look around, the breath effect is less powerful
        if (input.x == 0 && input.y == 0) 
        { 
            BreathCalculator(idleCounter, 0.025f, 0.025f);  // 0.025f, 0.025f
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * breathForce);  
        }
        // When player looks around, while sprinting
        else if (playerInput.OnFoot.Sprint.triggered)
        {
            BreathCalculator(movementCounter, 0.025f, 0.025f);  // 0.35f, 0.35f
            movementCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * breathForceSprinting); 
        }
        // When player looks around, the breath effect is more powerful/exagerated!
        else
        { 
            BreathCalculator(movementCounter, 0.025f, 0.025f);  // 0.15f, 0.075f
            movementCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * breathForceRotating); 
        }
    }
    private void BreathCalculator(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
    }
}
