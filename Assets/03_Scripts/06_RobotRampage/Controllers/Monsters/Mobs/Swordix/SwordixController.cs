using PeanutDashboard._06_RobotRampage;
using UnityEngine;

public class SwordixController : RobotRampageMonsterController
{
    [SerializeField]
    private SwordixAttackTrigger _swordixAttackTrigger;

    protected override void Start()
    {
        base.Start();
        _swordixAttackTrigger.Initialise(_cooldown, _damage);
    }

    protected override void Update()
    {
        base.Update();
        if (Vector2.Distance(this.transform.position, RobotRampagePlayerController.currentPosition) < 0.6f)
        {
            stopMoving = true;
        }
        else
        {
            stopMoving = false;
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
    }
}