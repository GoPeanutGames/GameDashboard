using System;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace PeanutDashboard._06_RobotRampage
{
    public class ShopPack: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private RewardType _rewardType;

        [SerializeField]
        private int _rewardAmount;

        [SerializeField]
        private CurrencyType _currencyType;

        [SerializeField]
        private int _currencyAmount;

        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnBuyButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnBuyButtonClick);
        }

        private void OnBuyButtonClick()
        {
            //TODO:
        }
    }
}