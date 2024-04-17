using UnityEngine;

namespace PeanutDashboard._04_FlappyIdiots
{
    public class TopPlayerLine : MonoBehaviour
    {
        // Start is called before the first frame update

        public UnityEngine.UI.Text NameText;
        public UnityEngine.UI.Text ScoreText;
        public UnityEngine.UI.Text DateText;
        void Start()
        {

        }

        public void SetData(string name, string score, string date)
        {
            NameText.text = name;
            ScoreText.text = score;
            DateText.text = date;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}