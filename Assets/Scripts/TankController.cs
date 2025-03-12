using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float gravity = 9.81f;


    private CharacterController controller;
    private Vector3 velocity;



    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("No se encontró un CharacterController en " + gameObject.name);
        }
    }

    void Update()
    {
        if (controller == null) return;

        // Capturar entrada de teclas (WASD o Flechas)
        float moveX = Input.GetAxis("Horizontal"); // A/D o Izq/Der (Rotación)
        float moveZ = Input.GetAxis("Vertical");   // W/S o Arriba/Abajo (Avance/Retroceso)

        // Rotación del tanque (Gira sobre su eje)
        transform.Rotate(Vector3.up, moveX * rotationSpeed * Time.deltaTime);

        // Movimiento hacia adelante/atrás en la dirección que mira el tanque
        Vector3 move = transform.forward * moveZ * speed;

        // Aplicar gravedad
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f; // Pequeño empuje para mantener contacto con el suelo
        }

        // Mover el tanque
        controller.Move((move + velocity) * Time.deltaTime);
    }
}
