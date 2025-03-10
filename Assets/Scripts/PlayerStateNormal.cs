using UnityEngine;

public class PlayerStateNormal: PlayerState
{
    protected PlayerController player;
    private bool driveMode = false;

    public PlayerStateNormal(PlayerController p){
        player = p;
    }

    public void HandleRight() {
        player.Move(0.5f);
    }

    public void HandleLeft() {
        player.Move(-0.5f);
    }

    public void HandleJump() {
        if (Input.GetKey("down")) {
            driveMode = true;
        }
        if (player.jumps > 0 && !driveMode) {
            player.Jump(7);
            player.jumps --;
            player.totalJumps++;
        }
    }

    public void Respawn() {
        player.Respawn();
    }

    public void AdvanceState() {
        if (driveMode) {
            player.SetState(new PlayerStateDrive(player, 120));
        }
        if (!player.isOnGround) {
            player.SetState(new PlayerStateJumping(player));
        }
    }
}
