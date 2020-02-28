using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public Player[,] Players { set; get; }
    public static BoardController Instance { get; set; }
    public GameObject Player1;
    public GameObject Player2;

    private bool[,] _allowedMoves { set; get; }
    private GameObject[,] fields;
    private FieldController _stateField;
    private FieldController[,] _field;
    private Player _selectedPlayer;
    private const float _tileSize = 1f;
    private const float _tileOffset = 0.5f;
    private int _selectionX = -1;
    private int _selectionY = -1;
    private bool _isPlayer1Turn;
    [SerializeField]
    private TurnController _turnController;
    [SerializeField]
    private List<GameObject> _playerPrefabs;
    [SerializeField]
    private List<GameObject> _fieldPrefabs;

    private void Start()
    {
        Instance = this;
        SpawnPlayer();
        SpawnField();
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessboard();

        if (Input.GetMouseButtonDown(0))
        {
            if (_selectionX >= 0 && _selectionY >= 0)
            {
                if (_selectedPlayer == null)
                {
                    SelectPlayer(_selectionX, _selectionY);
                }
                else
                {
                    MovePlayer(_selectionX, _selectionY);
                    _turnController.EndTurn();
                }
            }
        }
    }

    private void SelectPlayer(int x, int y)
    {
        if (Players[x, y] == null)
            return;
        if (Players[x, y].IsPlayer1 != _isPlayer1Turn)
            return;
        _allowedMoves = Players[x, y].PossibleMove();
        _selectedPlayer = Players[x, y];
        MoveController.Instance.HighLightAllowedMoves(_allowedMoves);
    }

    private void MovePlayer(int x, int y)
    {
        if (_allowedMoves[x,y])
        {
            Players [_selectedPlayer.CurrentX, _selectedPlayer.CurrentY] = null;
            _selectedPlayer.transform.position = GetTileCenter(x, y,0);
            _selectedPlayer.SetPosition(x, y);
            Players[x, y] = _selectedPlayer;
            _isPlayer1Turn = !_isPlayer1Turn;
            DiceController.Number = 0;
        }

        MoveController.Instance.HideHighlights();
        _selectedPlayer = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f,LayerMask.GetMask("BoardPlane")))
        {
            _selectionX = (int)hit.point.x;
            _selectionY = (int)hit.point.z;
        }
        else
        {
            _selectionX = -1;
            _selectionY = -1;
        }
    }

    private void InstancePlayer(int index,int x,int y)
    {
        GameObject go = Instantiate(_playerPrefabs [index], GetTileCenter(x,y,0), Quaternion.identity);
        go.transform.SetParent(transform);
        Players[x, y] = go.GetComponent<Player>();
        Players[x, y].SetPosition(x, y);
        
    }

    private void SpawnPlayer()
    {
        Players = new Player[14, 20];
        InstancePlayer(0, Random.Range(6, 9), 19);
        Player1 = _playerPrefabs[0];
        InstancePlayer(1, Random.Range(6, 9),0);
        Player2 = _playerPrefabs[1];
        
    }

    private void InstanceField(int index,int x, int y, float z)
    {
        GameObject _fields = Instantiate(_fieldPrefabs[index], GetTileCenter(x, y,z), Quaternion.identity);
        _fields.transform.SetParent(transform);
        _fields.SetActive(true);

    }

    private void SpawnField()
    {
        //normal Fields
        _field = new FieldController[14, 20];
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                InstanceField(0, i, j,-0.3f);
            }
        }
       
        //start fields
        for (int h = 6; h < 10 ; h++)
        {
            InstanceField(1, h, 0,-0.2f);
            InstanceField(1, h, 19,-0.2f);

        }

        //blue Team Event Cards

        InstanceField(2, 7, 1, -0.1f);
        InstanceField(2, 10, 3, -0.1f);
        InstanceField(2, 1, 3, -0.1f);
        InstanceField(2, 6, 6, -0.1f);
        InstanceField(2, 3, 7, -0.1f);
        InstanceField(2, 12, 5, -0.1f);
        InstanceField(2, 0, 1, -0.1f);

        //red Team Event Cards
        InstanceField(3, 7, 18, -0.1f);
        InstanceField(3, 10, 16, -0.1f);
        InstanceField(3, 1, 16, -0.1f);
        InstanceField(3, 6, 13, -0.1f);
        InstanceField(3, 3, 12, -0.1f);
        InstanceField(3, 12, 14, -0.1f);
        InstanceField(3, 0, 18, -0.1f);

    }


    private Vector3 GetTileCenter(int x, int y,float z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (_tileSize * x) + _tileOffset;
        origin.z += (_tileSize * y) + _tileOffset;
        origin.y += (_tileSize * z) + _tileOffset;
        return origin;
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 14;
        Vector3 heigthLine = Vector3.forward * 20;

        for (int i = 0; i <= 20; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start,start+widthLine);
            for (int j = 0; j <= 14; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heigthLine);
            }

        }
        //Draw Selection
        if(_selectionX >= 0 && _selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * _selectionY + Vector3.right * _selectionX,
                Vector3.forward * (_selectionY + 1) + Vector3.right * (_selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (_selectionY+1) + Vector3.right * _selectionX,
                Vector3.forward * _selectionY + Vector3.right * (_selectionX + 1));
        }
    }


}
