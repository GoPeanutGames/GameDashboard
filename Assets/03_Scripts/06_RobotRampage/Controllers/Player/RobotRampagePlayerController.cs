using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampagePlayerController : MonoBehaviour
	{
		public static Vector3 currentPosition;

		[SerializeField]
		private GameObject _playerImage;

		private void OnEnable()
		{
			RobotRampagePlayerEvents.OnPlayerKilled += OnPlayerKilled;
		}

		private void OnDisable()
		{
			RobotRampagePlayerEvents.OnPlayerKilled -= OnPlayerKilled;
		}

		private void Start()
		{
			Camera.main.transform.SetParent(this.transform);
		}

		private void Update()
		{
			Vector3 direction = Vector3.zero;
			if (Input.GetKey(KeyCode.W)){
				direction += new Vector3(0, 1, 0);
			}
			else if (Input.GetKey(KeyCode.S)){
				direction += new Vector3(0, -1, 0);
			}
			if (Input.GetKey(KeyCode.A)){
				direction += new Vector3(-1, 0, 0);
			}
			else if (Input.GetKey(KeyCode.D)){
				direction += new Vector3(1, 0, 0);
			}
			if (direction != Vector3.zero){
				direction.Normalize();
				this.transform.Translate(direction * Time.deltaTime * 2f);

				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				_playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
				currentPosition = this.transform.position;
			}

		}

		private void OnPlayerKilled()
		{
			Camera.main.transform.SetParent(null);
			Destroy(this.gameObject);
		}
	}
}