using UnityEngine;

public class LeaderboardContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var resetPos = gameObject.transform.position;
        resetPos.y = resetPos.y - 100;
        gameObject.transform.position = resetPos;
    }

    // Update is called once per frame
    void Update()
    {

    }
}