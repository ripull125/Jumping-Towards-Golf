using UnityEngine;

public class Thorns : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)  // Corrected function signature
    {
        Debug.Log("hey");
        if (collision.gameObject.CompareTag("Player"))  // Corrected CompareTag usage
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)  // Check if the component exists before using it
            {
                player.Respawn();
            }
            else
            {
                Debug.LogWarning("PlayerController script not found on the Player object!");
            }
        }
    }
}