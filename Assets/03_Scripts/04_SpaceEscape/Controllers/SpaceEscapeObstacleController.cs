using PeanutDashboard._04_SpaceEscape.Controllers;
using UnityEngine;

public class SpaceEscapeObstacleController : MonoBehaviour
{
    private void Update()
    {
        this.transform.Translate(Vector3.back *Time.deltaTime * 2f);
        if (this.transform.position.z < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            other.GetComponent<SpaceEscapePlayerMovement>().Damage(1);
            Destroy(this.gameObject);
        }
    }
}
