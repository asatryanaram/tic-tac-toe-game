using UnityEngine;
using UnityEngine.UI;

public class PausePopup : Popup
{
    [SerializeField] private Button _buttonQuit;

    [SerializeField] private Button _buttonRestart;
    
    void Start()
    {
    }

    void Update()
    {
    }

    public void ButtonClosePressedHandler()
    {
        Hide();
    }

    public void ButtonChangeCellsPressedHandler(int count)
    {
        GameController.Instance.ChangeCellsCount(count);
        Hide();
    }

    public override void Show()
    {
#if UNITY_WEBGL
        _buttonQuit.gameObject.SetActive(false); 
        _buttonRestart.transform.localPosition = new Vector2(0, _buttonRestart.transform.localPosition.y);

#endif
        base.Show();
    }
}