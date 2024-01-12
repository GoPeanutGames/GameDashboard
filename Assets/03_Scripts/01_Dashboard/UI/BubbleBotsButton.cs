using UnityEngine;
using UnityEngine.UI;

public class BubbleBotsButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnBubbleBotsClick);
    }

    private void OnBubbleBotsClick()
    {
        Application.OpenURL("https://peanutgames.com/download");
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnBubbleBotsClick);
    }
}
