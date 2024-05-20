using System;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampagePlayerMovement: MonoBehaviour
    {
        public static Vector3 currentPosition;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.up *Time.deltaTime*2f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.down *Time.deltaTime*2f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left *Time.deltaTime*2f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right *Time.deltaTime*2f);
            }

            currentPosition = this.transform.position;
        }
    }
}