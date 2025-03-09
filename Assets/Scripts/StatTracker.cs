using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public static StatTracker Instance;

    public int totalJumpsUsed = 0;
    public int levelsCompleted = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddJumps(int jumps)
    {
        totalJumpsUsed += jumps;
    }

    public void LevelCompleted()
    {
        levelsCompleted++;
    }
}