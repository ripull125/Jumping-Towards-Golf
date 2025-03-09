using UnityEngine;
using System.Collections;

public class WindFactory : MonoBehaviour
{
    public static WindFactory instance;

    public GameObject windPrefab;
    public int maxWindCount = 3;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float spawnInterval = 2f;
    public LayerMask collisionMask;

    private int currentWindCount = 0;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnWindOverTime());
    }

    private IEnumerator SpawnWindOverTime()
    {
        while (currentWindCount < maxWindCount)
        {
            yield return new WaitForSeconds(Random.Range(spawnInterval / 2, spawnInterval * 1.5f));

            SpawnWind();
        }
    }

    public void OnWindDestroyed()
    {
        currentWindCount--;
        SpawnWind();
    }

    public void SpawnWind()
    {
        if (currentWindCount >= maxWindCount)
            return;

        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        int maxAttempts = 10;
        int attempts = 0;

        while (Physics2D.OverlapCircle(spawnPosition, 0.5f, collisionMask) && attempts < maxAttempts)
        {
            spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            attempts++;
        }

        if (attempts < maxAttempts)
        {
            Instantiate(windPrefab, spawnPosition, Quaternion.identity);
            currentWindCount++;
        }
    }
}
