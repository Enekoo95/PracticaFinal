using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void ObstacleHit();
    public static event ObstacleHit OnObstacleHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            OnObstacleHit?.Invoke();
            Destroy(gameObject);
        }
    }
}
