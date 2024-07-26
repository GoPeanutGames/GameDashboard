using System;
using UnityEngine;

namespace PeanutDashboard.Utils
{
    public class OnAnimationDoneTrigger: MonoBehaviour
    {
        private Action _listener;

        public void AddListener(Action l)
        {
            _listener += l;
        }

        public void RemoveListener(Action l)
        {
            _listener -= l;
        }
        
        public void OnAnimationDone()
        {
            _listener?.Invoke();
        }
    }
}