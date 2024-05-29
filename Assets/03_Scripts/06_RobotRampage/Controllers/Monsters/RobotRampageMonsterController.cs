﻿using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class RobotRampageMonsterController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _enemyImage;

        [SerializeField]
        private float _maxHealthRef;

        [SerializeField]
        private GameObject _scrapPrefab;
        
        [Header(InspectorNames.DebugDynamic)]
        [SerializeField]
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealthRef;
        }

        private void Update()
        {
            Vector3 direction = RobotRampagePlayerController.currentPosition - this.transform.position;
            this.transform.Translate(direction.normalized * 0.7f * Time.deltaTime);

            Vector3 currentPos = this.transform.position;
            
            currentPos.x = currentPos.x - RobotRampagePlayerController.currentPosition.x;
            currentPos.y = currentPos.y - RobotRampagePlayerController.currentPosition.y;

            float angle = Mathf.Atan2(currentPos.y, currentPos.x) * Mathf.Rad2Deg;
            _enemyImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            
            if (Vector2.Distance(this.transform.position, RobotRampagePlayerController.currentPosition) > 15f)
            {
                Destroy(this.gameObject);
            }
        }

        public void Damage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0){
                Instantiate(_scrapPrefab, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}