using UnityEngine;

public class PlayerStateNormal: PlayerState
{
    protected PlayerController player;

    public PlayerStateNormal(PlayerController p){
        player = p;
    }

    public void HandleRight() {
        player.Move(300);
    }

    public void HandleLeft() {
        player.Move(-300);
    }

    public void HandleJump() {
        player.Jump(500);
    }

    public void HandleDrive() {
        return;
    }

    public void AdvanceState() {
        return;
    }
}
