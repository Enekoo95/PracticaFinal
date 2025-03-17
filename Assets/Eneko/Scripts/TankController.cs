using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float gravity = 9.81f;
    public GameObject projectilePrefab;
    public Transform firePoint;     
    public float projectileForce = 20f;
    public float projectileLifetime = 0.4f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("No se encontr� un CharacterController en " + gameObject.name);
        }
    }

    void Update()
    {
        if (controller == null) return;

        // Capturar entrada de teclas (WASD o Flechas)
        float moveX = Input.GetAxis("Horizontal"); // A/D o Izq/Der (Rotaci�n)
        float moveZ = Input.GetAxis("Vertical");   // W/S o Arriba/Abajo (Avance/Retroceso)

        // Rotaci�n del tanque (Gira sobre su eje)
        transform.Rotate(Vector3.up, moveX * rotationSpeed * Time.deltaTime);

        // Movimiento hacia adelante/atr�s en la direcci�n que mira el tanque
        Vector3 move = transform.forward * moveZ * speed;

        // Aplicar gravedad
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f; // Peque�o empuje para mantener contacto con el suelo
        }

        // Mover el tanque
        controller.Move((move + velocity) * Time.deltaTime);

        // Disparar proyectiles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = projectile.AddComponent<Rigidbody>();
            }
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.AddForce(firePoint.forward * projectileForce, ForceMode.Impulse);

            // Destruir el proyectil despu�s de projectileLifetime segundos
            Destroy(projectile, projectileLifetime);
        }
    }
}
