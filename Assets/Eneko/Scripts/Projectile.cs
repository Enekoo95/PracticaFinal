using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BreakableWall"))
        {
            Destroy(collision.gameObject); // Destruye la pared
        }
    }

}