using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputPlayer inputPlayer;
    [SerializeField]
    private float speed = 10f;
    private Vector3 velocity;
    [SerializeField]
    private Rigidbody rb;
    public bool isMoving;

    [Header("Walking properties")]
    [SerializeField] private AudioSource stepAudio;

    // Start is called before the first frame update
    void Awake()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /// Movimiento con translate
        velocity = new Vector3(inputPlayer.x, 0, inputPlayer.y);


        /// Movimiento con
        velocity = Vector3.ClampMagnitude(velocity, 1) * speed;
        // "Z" Hacia delante, "X" lateral y "Y" frontal
        rb.velocity = transform.right * velocity.x +
                      transform.up * rb.velocity.y +
                      transform.forward * velocity.z;

        if(inputPlayer.x+inputPlayer.y!=0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        /// Movement sound (Steps...)
        // if player is moving and audiosource is not playing play it
        if (isMoving && !stepAudio.isPlaying)
        {
            stepAudio.volume = Random.Range(0.8f, 1f);
            stepAudio.pitch = Random.Range(0.8f, 1.1f);
            stepAudio.Play();
        }
        // if player is not moving and audiosource is playing stop it
        if (!isMoving) stepAudio.Stop(); 
    }
}
