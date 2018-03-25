using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : Popup
{
    [SerializeField] private Text _textTitle;
    
    [SerializeField] private Button _buttonQuit;

    [SerializeField] private Button _buttonRestart;

    private const string ItIsADraw = "IT'S A DRAW";
    private const string Won = " WON !!!";

    void Start()
    {
    }

    void Update()
    {
    }
    
    public void Show(bool isDraw)
    {
        _textTitle.text = ItIsADraw;
        Show();
    }

    public void Show(string winnerName)
    {
        _textTitle.text = winnerName + Won;
        Show();
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