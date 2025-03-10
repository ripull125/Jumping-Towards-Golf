using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int jumpsToComplete;
    public Vector2 respawnPoint;
    public Vector3 newCameraPos;

    void Start()
    {
        respawnPoint = transform.position;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)  // Corrected function signature
    {
        Debug.Log("hey");
        if (collision.gameObject.CompareTag("Player"))  // Corrected CompareTag usage
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)  // Check if the component exists before using it
            {
                player.ReachCheckpoint(this);  // Corrected method call
            }
            else
            {
                Debug.LogWarning("PlayerController script not found on the Player object!");
            }
        }
    }
}