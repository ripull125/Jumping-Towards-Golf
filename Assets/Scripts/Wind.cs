using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector2 windForce = new Vector2(5f, 0f);
    public float moveSpeed = 2f;
    public float despawnXPosition = 10f;

    void Update()
    {

        transform.position += Vector3.right * moveSpeed * Time.deltaTime;


        if (transform.position.x > despawnXPosition)
        {
            Destroy(gameObject);


            WindFactory windFactory = FindObjectOfType<WindFactory>();
            if (windFactory != null)
            {
                windFactory.OnWindDestroyed();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(windForce, ForceMode2D.Force);
            }
        }
    }
}
