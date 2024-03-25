using UnityEngine;

namespace PeanutDashboard.UnityServer.Core
{
    public class TargetFPS : MonoBehaviour
    {
        [SerializeField] private int _target = 60;
        
        //TODO: sync server - client so they're the same, trigger game scene load on both
        //TODO: fix battledash
        //TODO: rps - create server scene that will just have the logic necessary to decide who wins and so on
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _target;
        }
    }
}
