using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour, IController
{
    [SerializeField] private Transform _boardContainer;

    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] [Range(MinCellsCount, MaxCellsCount)] private int _cellsCount = 3;

    private const int MinCellsCount = 3;
    
    private const int MaxCellsCount = 7;
    
    private int _cellSize;

    private int _boardContainerWidth;

    private List<Cell> _cells = new List<Cell>();

    public List<Cell> Cells
    {
        get { return _cells; }
    }

    private PlayerController _playerController;

    private int _moveCount;

    public Action ActionGameDraw;

    public Action<string> ActionGameOver;


    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void Init()
    {
        _boardContainerWidth = (int) _boardContainer.GetComponent<RectTransform>().sizeDelta.x;
        InitParams();
        InstantiatePrefabs();
    }

    public void Restart()
    {
        for (int i = 0, count = _cells.Count; i < count; ++i)
        {
            _cells[i].Restart();
        }

        _moveCount = 0;
    }

    private void InitParams()
    {
        _cellSize = _boardContainerWidth / _cellsCount;
        _cellPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(_cellSize, _cellSize);
    }

    private void InstantiatePrefabs()
    {
        for (int i = 0; i < _cellsCount; ++i)
        {
            for (int j = 0; j < _cellsCount; ++j)
            {
                GameObject prefab = Instantiate(_cellPrefab, _boardContainer, false);
                int row = _cellSize * j;
                int col = _cellSize * -i;
                prefab.transform.localPosition = new Vector2(row, col);
                Cell cell = prefab.GetComponent<Cell>();
                _cells.Add(cell);
                cell.ActionPlayerMakeMove = PlayerMakeMoveActionHandler;
            }
        }
    }

    public void ChangeCellsCount(int count)
    {
        count = Math.Max(MinCellsCount, count);
        count = Math.Min(MaxCellsCount, count);

        _cellsCount = count;
        
        ClearBoard();
        InitParams();
        InstantiatePrefabs();
    }

    private void ClearBoard()
    {
        for (int i = 0, count = _cells.Count; i < count; ++i)
        {
            _cells[i].Hide();
            Destroy(_cells[i].gameObject);
        }
        _cells.Clear();
    }

    private void PlayerMakeMoveActionHandler()
    {
        // Player must move at least "CellsCount" time to win game
        // So I don't unnecessary check win condition
        if (++_moveCount < _cellsCount * 2 - 1)
        {
            _playerController.TurnPlayer();
            return;
        }

        if (CheckWinCondition())
        {
            GameWin();
            return;
        }

        if (IsAllCellsFill())
        {
            GameDraw();
        }
        else
        {
            _playerController.TurnPlayer();
        }
    }

    private bool CheckWinCondition()
    {
        for (int i = 0; i < _cellsCount; ++i)
        {
            if (CheckHorizontalWin(i, _playerController.Current))
            {
                return true;
            }

            if (CheckVerticalWin(i, _playerController.Current))
            {
                return true;
            }
        }

        if (CheckFirstDiagonalWin(_playerController.Current))
        {
            return true;
        }

        if (CheckSecondDiagonalWin(_playerController.Current))
        {
            return true;
        }

        return false;
    }

    private bool CheckHorizontalWin(int col, State player)
    {
        int collumn = col * _cellsCount;
        for (int i = 0; i < _cellsCount; ++i)
        {
            if (!_cells[collumn + i].CurrentState.Equals(player))
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckVerticalWin(int row, State player)
    {
        for (int i = 0; i < _cellsCount; ++i)
        {
            int line = i * _cellsCount;
            if (!_cells[line + row].CurrentState.Equals(player))
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckFirstDiagonalWin(State player)
    {
        for (int i = 0; i < _cellsCount; ++i)
        {
            int number = (i * _cellsCount) + _cellsCount - i - 1;
            if (!_cells[number].CurrentState.Equals(player))
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckSecondDiagonalWin(State player)
    {
        for (int i = 0; i < _cellsCount; ++i)
        {
            int num = (i * _cellsCount) + i;
            if (!_cells[num].CurrentState.Equals(player))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsAllCellsFill()
    {
        for (int i = 0, count = _cells.Count; i < count; ++i)
        {
            if (_cells[i].IsEmpty())
            {
                return false;
            }
        }

        return true;
    }

    private void GameDraw()
    {
        if (null != ActionGameDraw)
        {
            ActionGameDraw();
        }
    }

    private void GameWin()
    {
        if (null != ActionGameOver)
        {
            ActionGameOver(_playerController.Current.ToString());
        }
    }
}