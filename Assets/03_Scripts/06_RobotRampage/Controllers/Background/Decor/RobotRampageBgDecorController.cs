using System.Collections.Generic;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageBgDecorController: MonoBehaviour
    {
        private const float HorizontalIncreaseHalf = 5.4F;
        private const float VerticalIncreaseHalf = 9.6F;
        
        [SerializeField]
        private GameObject _prefab;
        
        [SerializeField]
        private List<Sprite> _possibleDecor;

        public void Setup(List<Sprite> decor)
        {
            _possibleDecor = decor;
            SetupDecor();
        }

        private void SetupDecor()
        {
            int decorAmount = Random.Range(2, 5);
            for (int i = 0; i < decorAmount; i++)
            {
                Vector3 decorPosition = this.transform.position +
                                        new Vector3(Random.Range(-HorizontalIncreaseHalf, HorizontalIncreaseHalf),
                                            Random.Range(-VerticalIncreaseHalf, VerticalIncreaseHalf), 0);
                GameObject decor = Instantiate(_prefab, this.transform);
                decor.GetComponent<SpriteRenderer>().sprite = _possibleDecor[Random.Range(0, _possibleDecor.Count)];
                decor.GetComponent<SpriteRenderer>().sortingOrder = 1;
                decor.transform.position = decorPosition;
            }
        }
    }
}