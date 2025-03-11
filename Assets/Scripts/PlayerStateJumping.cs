using UnityEngine;

public class PlayerStateJumping: PlayerState
{
    protected PlayerController player;
    private bool jumped = false;
    private bool driveMode = false;

    public PlayerStateJumping(PlayerController p){
        player = p;
    }

    public void HandleRight() {
        player.Move(0.5f);
        player.CapVelocity();
    }

    public void HandleLeft() {
        player.Move(-0.5f);
        player.CapVelocity();
    }

    public void HandleJump() {
        if (Input.GetKey("down")) {
            driveMode = true;
            return;
        }
        if (player.jumps > 0) {
            player.Jump(6);
            player.jumps --;
            player.totalJumps++;
            //EventBus.PublishJumpUsed(1);
            jumped = true;
            return;
        }
    }

    public void Respawn() {
        player.Respawn();
    }

    public void AdvanceState() {
        if (driveMode) {
            player.SetState(new PlayerStateDrive(player, 120));
        }
        if (player.isOnGround) {
            player.SetState(new PlayerStateNormal(player));
        }
        if (jumped) {
            player.SetState(new PlayerStateFreefall(player));
        }
        
    }
}
