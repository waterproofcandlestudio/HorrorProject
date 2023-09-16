using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rigidbody obligatorio para el objeto al q se le adjunte el código...
[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jmpStr = 10f;
    // Radio
    [SerializeField]
    private float grndCheckR = 0.3f;
    // Genera una esfera en los pies del personaje para comprobar si toca el suelo
    [SerializeField]
    private LayerMask grndMask = 0;

    private bool grounded = false;
    private Vector3 velocity;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(transform.position, grndCheckR, grndMask);

        /// Movimiento con translate
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //velocity = velocity.normalized * speed * Time.deltaTime;

        //transform.Translate(velocity.x, 0, velocity.y);

        /// Movimiento con transform
        velocity = Vector3.ClampMagnitude(velocity, 1) * speed;
        // "Z" Hacia delante, "X" lateral y "Y" vertical
        rb.velocity = transform.right * velocity.x +
                      transform.up * rb.velocity.y +
                      transform.forward * velocity.z;

        /// Salto
        if(Input.GetButtonDown("Jump") && grounded)
        {
            /// SALTO usando FUERZAS
            //// ForceMode.Impulse ==> impulsa en un instante solamente
            //// ForceMode.Acceleration ==> como 1 coche, impulsa y tras dejar de pulsar la tecla el movimiento sigue un rato...
            //rb.AddForce(Vector3.up * jmpStr, ForceMode.Impulse);

            /// SALTO usando VELOCIDADES
            rb.velocity = new Vector3(rb.velocity.x, jmpStr, rb.velocity.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, grndCheckR);
    }
}
