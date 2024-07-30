using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
    public class LoginSceneController: MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private AudioClip _bgMusic;

        private void Start()
        {
            RobotRampageAudioEvents.RaisePlayBgMusicEvent(_bgMusic,true);
        }
    }
}