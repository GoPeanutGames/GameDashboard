using UnityEngine;

public class MovePlayerUpDown : MonoBehaviour
{
	public float speed;

	void Update()
	{
		if (Input.GetKey(KeyCode.W)){
			transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
		}
		if (Input.GetKey(KeyCode.S)){
			transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
		}
	}
}