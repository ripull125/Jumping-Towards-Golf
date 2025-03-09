using UnityEngine;
using UnityEngine.UI;

public class StatDisplay  : MonoBehaviour
{
    public Text statsText;

    private void Update()
    {
        statsText.text = "Jumps Used: " + StatTracker.Instance.totalJumpsUsed +
                         "\nLevels Completed: " + StatTracker.Instance.levelsCompleted;
    }
}