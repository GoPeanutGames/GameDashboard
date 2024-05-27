using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampagePlayerMovement: MonoBehaviour
    {
        public static Vector3 currentPosition;

        [SerializeField]
        private GameObject _playerImage;
        
        private void Update()
        {
            RotateToMouse();
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(-_playerImage.transform.up *Time.deltaTime*2f);
            }

            currentPosition = this.transform.position;
        }

        private void RotateToMouse()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            _playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
    }
}