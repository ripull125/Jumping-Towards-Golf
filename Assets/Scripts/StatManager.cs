using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;

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

    private void OnEnable()
    {
        EventBus.OnJumpUsed += UpdateJumpCount;
        EventBus.OnLevelCompleted += UpdateLevelCount;
    }

    private void OnDisable()
    {
        EventBus.OnJumpUsed -= UpdateJumpCount;
        EventBus.OnLevelCompleted -= UpdateLevelCount;
    }

    private void UpdateJumpCount(int jumps)
    {
        totalJumpsUsed += jumps;
    }

    private void UpdateLevelCount()
    {
        levelsCompleted++;
    }
}