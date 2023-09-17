using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale = new Vector3(1, 1f, 1);

    public Vector3 velocity;
    public bool isMoving = false;

    //x
    public float speed = 12f;
    public float VelocidadNormal = 12f;
    public float VelocidadSprint = 20f;
    public float VelocidadDeslizamiento = 30f;
    public float Acceleration = 0.05f;

    //y
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float velocidadCaidaMax = -30f;

    //z
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    void Update()
    {
        //movimiento basico
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.x = move.x;
        velocity.z = move.z;

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        //salto

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
        if (velocity.y < velocidadCaidaMax)
        {
            velocity.y = velocidadCaidaMax;
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;


        //velocidadNormal
        if (speed > VelocidadNormal)
        {
            speed -= Acceleration;
        }
        if (speed < VelocidadNormal)
        {
            speed += Acceleration;
        }

        //comprobar si se mueve el personaje eje xz(horizontal)
        if (velocity.x == 0 && velocity.z == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        //sprint        
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && isMoving)
        {
            speed = VelocidadSprint;
            Acceleration = 0.025f;
        }

        //crouch
        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            VelocidadNormal = 5f;
            Acceleration = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            VelocidadNormal = 12f;
            Acceleration = 0.3f;
        }

        //deslizamiento
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            speed = VelocidadDeslizamiento;
            VelocidadNormal = 3f;
            Acceleration = 0.2f;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            VelocidadNormal = 12f;
            Acceleration = 0.1f;
        }
    }
}
