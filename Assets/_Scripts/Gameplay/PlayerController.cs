using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    private State _currentState;

    public State Current
    {
        get { return _currentState; }
        private set
        {
            _currentState = value;
            _uiController.UpdateTurn(_currentState.ToString());
        }
    }

    private UIController _uiController;


    void Awake()
    {
        _uiController = FindObjectOfType<UIController>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void Init()
    {
        Current = State.X;
    }

    public void Restart()
    {
    }

    public void TurnPlayer()
    {
        Current = _currentState.Equals(State.X) ? State.O : State.X;
    }
}