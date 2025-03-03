using UnityEngine;

public class PlayerStateFreefall: PlayerState
{
    protected PlayerController player;

    public PlayerStateFreefall(PlayerController p){
        player = p;
    }

    public void HandleRight() {
        player.Move(0.2f);
    }

    public void HandleLeft() {
        player.Move(-0.2f);
    }

    public void HandleJump() {
        //Player cannot jump while in freefall
        return;
    }

    public void HandleDrive() {
        //Player cannot enter drive mode while in freefall
        return;
    }

    public void AdvanceState() {
        if (player.isOnGround) {
            player.SetState(new PlayerStateNormal(player));
        }
    }
}
