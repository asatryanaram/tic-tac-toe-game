using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour, IController
{
    [SerializeField] private GameObject _aiButtonDisabled;
    
    private Turn _turn;
    private Popup _pausePopup;
    private ResultPopup _resultPopup;
    
    void Awake()
    {
        _turn = FindObjectOfType<Turn>();
        _pausePopup = FindObjectOfType<PausePopup>();
        _resultPopup = FindObjectOfType<ResultPopup>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void Init()
    {
        _pausePopup.Hide();
        _resultPopup.Hide();
        ShowAIButtonState();
    }

    public void Restart()
    {
        Init();
    }

    public void UpdateTurn(string text)
    {
        _turn.UpdateTurn(text);
    }

    public void ButtonMenuPressedHandler()
    {
        ShowPausePopup();
    }

    public void ShowPausePopup()
    {
        _pausePopup.Show();
    }

    public void HidePausePopup()
    {
        _pausePopup.Hide();
    }

    public void ShowResultPopup()
    {
        _resultPopup.Show(true);
    }
    
    public void ShowResultPopup(string winner)
    {
        _resultPopup.Show(winner);
    }

    public void ButtonAIPressedHandler()
    {
        ShowAIButtonState();

    }

    private void ShowAIButtonState()
    {
    }
}