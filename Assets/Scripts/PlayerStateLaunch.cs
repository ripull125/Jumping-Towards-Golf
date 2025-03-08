using UnityEngine;

public class PlayerStateLaunch : PlayerState
{
    protected PlayerController player;
    private int frames;

    public PlayerStateLaunch(PlayerController p, int f) {
        player = p;
        frames = f;
    }

    public void HandleRight() {
        //Player should not be able to control tradjectory during this
        return;
    }
    public void HandleLeft() {
        //Player should not be able to control tradjectory during this
        return;
    }
    public void HandleJump() {
        //Player should not be able to control tradjectory during this
        return;
    }
    public void Respawn() {
        player.Respawn();
    }
    public void AdvanceState() {
        frames --;
        if (frames <= 0) {
            player.SetState(new PlayerStateFreefall(player));
        }
    }
}