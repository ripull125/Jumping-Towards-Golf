using UnityEngine;
using System.Collections;

public class WindFactory : MonoBehaviour
{
    public GameObject windPrefab;
    public int maxWindCount = 3;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float spawnInterval = 2f;
    public LayerMask collisionMask;

    private int currentWindCount = 0;
    private bool isActive = false; 

    void Start()
    {
        isActive = true; 
        StartCoroutine(SpawnWindOverTime());
    }

    void OnEnable()
    {
        isActive = true;
        StartCoroutine(SpawnWindOverTime());
    }

    void OnDisable()
    {
        isActive = false; 
    }

    private IEnumerator SpawnWindOverTime()
    {
        while (isActive && currentWindCount < maxWindCount)
        {
            yield return new WaitForSeconds(Random.Range(spawnInterval / 2, spawnInterval * 1.5f));

            SpawnWind();
        }
    }

    public void OnWindDestroyed()
    {
        currentWindCount--;
        if (isActive) 
        {
            SpawnWind();
        }
    }

    public void SpawnWind()
    {
        if (!isActive || currentWindCount >= maxWindCount)
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
