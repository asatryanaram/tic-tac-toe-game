using System;
using UnityEngine;

public class GameController : GenericSingletonClass<GameController>, IController
{
    private UIController _uiController;
    private BoardController _boardController;
    private PlayerController _playerController;

    public override void Awake()
    {
        base.Awake();

        _uiController = FindObjectOfType<UIController>();
        _boardController = FindObjectOfType<BoardController>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
    }

    public void Init()
    {
        _uiController.Init();
        _boardController.Init();       
        _playerController.Init();       

        _boardController.ActionGameDraw = ActioGameDrawHandler;
        _boardController.ActionGameOver = ActioGameOverHandler;
    }

    public void Restart()
    {
        _uiController.Restart();
        _boardController.Restart();
        _playerController.Restart();
    }

    public void ChangeCellsCount(int count)
    {
        _boardController.ChangeCellsCount(count);
    }

    private void ActioGameDrawHandler()
    {
        _uiController.ShowResultPopup();
    }

    private void ActioGameOverHandler(string winner)
    {
        _uiController.ShowResultPopup(winner);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}