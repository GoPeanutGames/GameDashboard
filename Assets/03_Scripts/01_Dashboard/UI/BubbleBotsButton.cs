using UnityEngine;
using UnityEngine.UI;

public class BubbleBotsButton : MonoBehaviour
{
    private Button _button;
    
    private void Start()
    {
        _button = GetComponent<Button>();
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
