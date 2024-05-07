using PeanutDashboard._04_FlappyIdiots;
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

    public void UpdateTopPlayers(LeaderBoardEltData[] data)
    {
        var childs = GetComponentsInChildren<TopPlayerLine>();
        
        for (var i = 0; i < childs.Length; i++)
        {
            var playerLine = childs[i];
            var scoreInfo = new LeaderBoardEltData();
            if (data.Length > i) 
            {
                scoreInfo = data[i];
            }
            playerLine.SetData(scoreInfo.playerName, scoreInfo.highestScore.ToString(), "");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}