using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageMonsterController: MonoBehaviour
    {
        private void Update()
        {
            Vector3 direction = RobotRampagePlayerMovement.currentPosition - this.transform.position;
            this.transform.Translate(direction.normalized * 0.7f * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, RobotRampagePlayerMovement.currentPosition) > 15f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}