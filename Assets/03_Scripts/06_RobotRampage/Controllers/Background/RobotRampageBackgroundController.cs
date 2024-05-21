using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageBackgroundController: MonoBehaviour
    {
        [SerializeField]
        private GameObject _bgPrefab;

        [SerializeField]
        private Sprite _defaultBackground;

        [SerializeField]
        private float _centralWidth = 4f;

        [SerializeField]
        private List<GameObject> _backgroundObjectsSpawned;

        private int currentGridPosX = 0;
        private int currentGridPosY = 0;

        private float horizontalIncrease = 10.8f;
        private float verticalIncrease = 19.2f;

        private void Start()
        {
            GameObject bg = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f, 0, 0), Quaternion.identity);
            bg.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgRight = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + horizontalIncrease, 0, 0), Quaternion.identity);
            bgRight.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgLeft = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f - horizontalIncrease, 0, 0), Quaternion.identity);
            bgLeft.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgTop = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f, verticalIncrease, 0), Quaternion.identity);
            bgTop.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgDown = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f, -verticalIncrease, 0), Quaternion.identity);
            bgDown.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgTopRight = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + horizontalIncrease, verticalIncrease, 0), Quaternion.identity);
            bgTopRight.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgTopLeft = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f - horizontalIncrease, verticalIncrease, 0), Quaternion.identity);
            bgTopLeft.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgBottomLeft = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f - horizontalIncrease, -verticalIncrease, 0), Quaternion.identity);
            bgBottomLeft.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
            GameObject bgBottomRight = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + horizontalIncrease, -verticalIncrease, 0), Quaternion.identity);
            bgBottomRight.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
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
            int newGridPosX = Mathf.FloorToInt(RobotRampagePlayerMovement.currentPosition.x / horizontalIncrease);
            int newGridPosY = (int)(RobotRampagePlayerMovement.currentPosition.y / verticalIncrease);
            if (currentGridPosX != newGridPosX || currentGridPosY != newGridPosY)
            {
                CheckBgs(newGridPosX, newGridPosY);
            }

            currentGridPosX = newGridPosX;
            currentGridPosY = newGridPosY;
        }

        private void CheckBgs(int newGridPosX, int newGridPosY)
        {
            bool bgDeleted = false;
            foreach (GameObject bg in _backgroundObjectsSpawned)
            {
                int bgGridPosX = Mathf.FloorToInt(bg.transform.position.x / horizontalIncrease);
                int bgGridPosY = (int)(bg.transform.position.y / verticalIncrease);
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
                        int bgGridPosX = Mathf.FloorToInt(bg.transform.position.x / horizontalIncrease);
                        int bgGridPosY = (int)(bg.transform.position.y / verticalIncrease);
                        if (bgGridPosX == i + newGridPosX && bgGridPosY == j+newGridPosY)
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        GameObject bg = Instantiate(_bgPrefab, new Vector3(_centralWidth / 2f + (i + newGridPosX)*horizontalIncrease, (j+newGridPosY)*verticalIncrease, 0), Quaternion.identity);
                        bg.GetComponent<SpriteRenderer>().sprite = _defaultBackground;
                        _backgroundObjectsSpawned.Add(bg);
                    }
                }
            }
        }
    }
}