using System.Collections;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

public class RPSWaitingTextAnimController : MonoBehaviour
{
    [Header(InspectorNames.DebugDynamic)]
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private string _dots = "";

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(UpdateText());
    }

    private IEnumerator UpdateText()
    {
        while (true){
            yield return new WaitForSeconds(0.25f);
            _dots += ".";
            _text.text = "Waiting for opponent" + _dots;
            if (_dots.Length == 3){
                _dots = "";
            }
        }
    }
}
