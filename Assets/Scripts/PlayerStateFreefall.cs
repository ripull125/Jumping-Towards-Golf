using UnityEngine;

public class PlayerStateFreefall: PlayerState
{
    protected PlayerController player;

    public PlayerStateFreefall(PlayerController p){
        player = p;
    }

    public void HandleRight() {
        player.Move(0.2f);
        player.CapVelocity();
    }

    public void HandleLeft() {
        player.Move(-0.2f);
        player.CapVelocity();
    }

    public void HandleJump() {
        //Player cannot jump while in freefall
        return;
    }

    public void Respawn() {
        player.Respawn();
    }

    public void AdvanceState() {
        if (player.isOnGround) {
            player.SetState(new PlayerStateNormal(player));
        }
    }
}
