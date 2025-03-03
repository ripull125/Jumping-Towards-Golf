using UnityEngine;

public interface PlayerState
{
    public void HandleRight();
    public void HandleLeft();
    public void HandleJump();
    public void HandleDrive();
    public void AdvanceState();
}
