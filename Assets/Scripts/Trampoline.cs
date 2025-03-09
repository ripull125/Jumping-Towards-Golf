using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float strength;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)  // Corrected function signature
    {
        if (collision.gameObject.CompareTag("Player"))  // Corrected CompareTag usage
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)  // Check if the component exists before using it
            {
                player.Jump(strength);  // Corrected method call
            }
            else
            {
                Debug.LogWarning("PlayerController script not found on the Player object!");
            }
        }
    }
}