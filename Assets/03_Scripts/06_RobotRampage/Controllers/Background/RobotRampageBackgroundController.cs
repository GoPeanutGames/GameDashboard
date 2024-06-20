using System.Collections.Generic;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageBackgroundController: MonoBehaviour
    {
        private const float HorizontalIncrease = 10.8f;
        private const float VerticalIncrease = 19.2f;
        
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _bgPrefab;

        [SerializeField]
        private float _centralWidth = 4f;

        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private List<GameObject> _backgroundObjectsSpawned;

        [SerializeField]
        private int _currentGridPosX = 0;
        
        [SerializeField]
        private int _currentGridPosY = 0;

        [SerializeField]
        private Sprite _defaultBackground;
        
        [SerializeField]
        private List<Sprite> _decor;

        [SerializeField]
        private List<GameObject> _propPrefabs;
        
        private void Start()
        {
            _defaultBackground = RobotRampageStageService.currentStageData.DefaultBackground;
            _decor = RobotRampageStageService.currentStageData.PossibleDecor;
            _propPrefabs = RobotRampageStageService.currentStageData.PropPrefabs;
            GameObject bg = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f, 0, 0), Quaternion.identity);
            bg.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bg.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgRight = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + HorizontalIncrease, 0, 0), Quaternion.identity);
            bgRight.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgRight.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgLeft = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f - HorizontalIncrease, 0, 0), Quaternion.identity);
            bgLeft.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgLeft.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgTop = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f, VerticalIncrease, 0), Quaternion.identity);
            bgTop.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgTop.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgDown = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f, -VerticalIncrease, 0), Quaternion.identity);
            bgDown.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgDown.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgTopRight = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + HorizontalIncrease, VerticalIncrease, 0), Quaternion.identity);
            bgTopRight.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgTopRight.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgTopLeft = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f - HorizontalIncrease, VerticalIncrease, 0), Quaternion.identity);
            bgTopLeft.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgTopLeft.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgBottomLeft = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f - HorizontalIncrease, -VerticalIncrease, 0), Quaternion.identity);
            bgBottomLeft.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgBottomLeft.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            GameObject bgBottomRight = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + HorizontalIncrease, -VerticalIncrease, 0), Quaternion.identity);
            bgBottomRight.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            bgBottomRight.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
            _backgroundObjectsSpawned.Add(bg);
            _backgroundObjectsSpawned.Add(bgRight);
            _backgroundObjectsSpawned.Add(bgLeft);
            _backgroundObjectsSpawned.Add(bgTop);
            _backgroundObjectsSpawned.Add(bgDown);
            _backgroundObjectsSpawned.Add(bgTopRight);
            _backgroundObjectsSpawned.Add(bgTopLeft);
            _backgroundObjectsSpawned.Add(bgBottomLeft);
            _backgroundObjectsSpawned.Add(bgBottomRight);
        }

        private void Update()
        {
            ClearAllNull();
            CheckPos();
        }

        private void ClearAllNull()
        {
            _backgroundObjectsSpawned.RemoveAll((bg) => bg == null);
        }

        private void CheckPos()
        {
            int newGridPosX = Mathf.FloorToInt(RobotRampagePlayerController.currentPosition.x / HorizontalIncrease);
            int newGridPosY = (int)(RobotRampagePlayerController.currentPosition.y / VerticalIncrease);
            if (_currentGridPosX != newGridPosX || _currentGridPosY != newGridPosY)
            {
                CheckBgs(newGridPosX, newGridPosY);
            }

            _currentGridPosX = newGridPosX;
            _currentGridPosY = newGridPosY;
        }

        private void CheckBgs(int newGridPosX, int newGridPosY)
        {
            bool bgDeleted = false;
            foreach (GameObject bg in _backgroundObjectsSpawned)
            {
                int bgGridPosX = Mathf.FloorToInt(bg.transform.position.x / HorizontalIncrease);
                int bgGridPosY = (int)(bg.transform.position.y / VerticalIncrease);
                if (Mathf.Abs(newGridPosX - bgGridPosX) >= 2 || Mathf.Abs(newGridPosY - bgGridPosY) >=2)
                {
                    Destroy(bg);
                    bgDeleted = true;
                }
            }

            if (!bgDeleted)
            {
                return;
            }

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    bool found = false;
                    foreach (GameObject bg in _backgroundObjectsSpawned)
                    {
                        int bgGridPosX = Mathf.FloorToInt(bg.transform.position.x / HorizontalIncrease);
                        int bgGridPosY = (int)(bg.transform.position.y / VerticalIncrease);
                        if (bgGridPosX == i + newGridPosX && bgGridPosY == j+newGridPosY)
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        GameObject bg = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + (i + newGridPosX)*HorizontalIncrease, (j+newGridPosY)*VerticalIncrease, 0), Quaternion.identity);
                        bg.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
                        bg.GetComponent<RobotRampageBgDecorController>().Setup(_decor, _propPrefabs);
                        _backgroundObjectsSpawned.Add(bg);
                    }
                }
            }
        }
    }
}