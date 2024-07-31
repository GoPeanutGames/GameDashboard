using PeanutDashboard._06_RobotRampage;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using UnityEngine.UI;

public class ConnectTonButton : MonoBehaviour
{
    [Header(InspectorNames.DebugDynamic)]
    [SerializeField]
    private Button _button;
        
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ConnectTon);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ConnectTon);
    }

    private void ConnectTon()
    {
        TonAuthEvents.RaiseTonConnectEvent();
    }
}
