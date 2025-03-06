using UnityEngine;

public class PlayerStateDrive: PlayerState
{
    protected PlayerController player;
    protected GameObject arrow;
    private int frames;

    public PlayerStateDrive(PlayerController p, int f){
        player = p;
        arrow = player.arrow;
        frames = f;
        ShowArrow();
    }

    public void HandleRight() {
        arrow.transform.Rotate(-1 * Vector3.forward);
    }

    public void HandleLeft() {
        arrow.transform.Rotate(1 * Vector3.forward);
    }

    public void HandleJump() {
        //Player cannot jump while in freefall
        return;
    }

    public void Respawn() {
        player.Respawn();
    }

    public void AdvanceState() {
        frames--;
        if (frames <= 0) {
            if (player.jumps > 1) {
                player.jumps -= 2;
                LaunchPlayer();
            }
            player.SetState(new PlayerStateFreefall(player));
        }
    }

    void LaunchPlayer() {
        float angle = (arrow.transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        player.Jump((int)(Mathf.Sin(angle) * 12));
        player.Move((int)(Mathf.Cos(angle) * 12));
        HideArrow();
    }

    void ShowArrow() {
        player.ShowAimArrow();
    }

    void HideArrow() {
        player.HideAimArrow();
    }
}
