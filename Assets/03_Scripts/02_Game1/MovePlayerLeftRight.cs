using UnityEngine;

public class MovePlayerLeftRight : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A)){
            transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        }
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        }
    }
}
