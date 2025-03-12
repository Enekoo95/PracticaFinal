using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource hitSound;
    public int score = 0;

    void OnEnable()
    {
        Obstacle.OnObstacleHit += HandleObstacleHit;
    }

    void OnDisable()
    {
        Obstacle.OnObstacleHit -= HandleObstacleHit;
    }

    void HandleObstacleHit()
    {
        score++;
        hitSound.Play();
        Debug.Log("Score: " + score);
    }
}