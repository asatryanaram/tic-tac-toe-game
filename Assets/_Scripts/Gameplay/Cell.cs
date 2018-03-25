using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IController, IPointerClickHandler
{
    private const string X = "X";
    private const string Zero = "0";

    private Text _textCell;

    private bool _isEmpty;

    private State _state;

    public State CurrentState
    {
        get { return _state; }
    }

    private PlayerController _playerController;

    public Action ActionPlayerMakeMove;


    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _textCell = GetComponentInChildren<Text>();
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
        _state = State.Empty;
        ShowCell();
    }

    public void Restart()
    {
        Init();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsEmpty()
    {
        return _state.Equals(State.Empty);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CheckIsCanMove();
    }

    public void CheckIsCanMove()
    {
        if (!IsEmpty()) return;
        
        MakeMove();
    }

    private void MakeMove()
    {
        _state = _playerController.Current;
        ShowCell();

        if (null != ActionPlayerMakeMove)
        {
            ActionPlayerMakeMove();
        }
    }

    private void ShowCell()
    {
        if (_state.Equals(State.X))
        {
            _textCell.text = X;
        }
        else if (_state.Equals(State.O))
        {
            _textCell.text = Zero;
        }
        else
        {
            _textCell.text = String.Empty;
        }
    }
}