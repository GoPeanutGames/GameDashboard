using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public CanvasGroup loadingscreenCanvas;

    [SerializeField]
    private bool _isActive = false;

    public RectTransform loadingCircle;
    public float rotateSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        displayCanvas();
    }

    void displayCanvas()
    {
        loadingscreenCanvas.alpha = _isActive? 1f : 0f;
        loadingscreenCanvas.gameObject.SetActive(_isActive);
    }
    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            loadingCircle.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        _isActive = true;
        displayCanvas();
    }

    public void OnDisactivate()
    {
       _isActive=false;
        displayCanvas();
    }
    public void OnCancel()
    {
        _isActive = false;
        displayCanvas();
    }
}
