using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<int> OnJumpUsed;
    public static event Action OnLevelCompleted;

    public static void PublishJumpUsed(int jumps)
    {
        OnJumpUsed?.Invoke(jumps);
    }

    public static void PublishLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
}