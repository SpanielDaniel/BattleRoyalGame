using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code by Daniel Pobijanski

public class BoardController : MonoBehaviour
{
    public Player[,] Players { set; get; }
    public static BoardController Instance { get; set; }
    public GameObject Player1;
    public GameObject Player2;
    public static bool _isPlayer1Turn;

    private bool[,] _allowedMoves { set; get; }
    private FieldController[,] _field;
    private Player _selectedPlayer;
    private Player _enemyPlayer;
    private const float _tileSize = 1f;
    private const float _tileOffset = 0.5f;
    private int _selectionX = -1;
    private int _selectionY = -1;
    [SerializeField]
    private TurnController _turnController;
    [SerializeField]
    private List<GameObject> _playerPrefabs;
    [SerializeField]
    private List<GameObject> _fieldPrefabs;
    private int _currentTurn = 1;
    private int _destroyRound = 1;

    private void Awake() {
        SpawnPlayer();
    }
    private void Start()
    {
        Instance = this;
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
                }
            }
        }

        if ((TurnController.GetTurn() - 1) % 4 == 0 && TurnController.GetTurn() != _currentTurn && _destroyRound < 9) {
            _currentTurn = TurnController.GetTurn();
            DestroyField(_destroyRound);
            _destroyRound++;
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
            CheckSurrounding(x,y);
            _turnController.EndTurn();
        }

        MoveController.Instance.HideHighlights();
        _selectedPlayer = null;
    }

    private void CheckSurrounding(int x, int y)
    {
        _enemyPlayer = Instance.Players[x, y];
        if (_enemyPlayer != null && Players[x+1,y])
        {
            _turnController.Fight();
        }
        if (_enemyPlayer != null && Players[x - 1, y])
        {
            _turnController.Fight();
        }
        if (_enemyPlayer != null && Players[x, y+1])
        {
            _turnController.Fight();
        }
        if (_enemyPlayer != null && Players[x, y-1])
        {
            _turnController.Fight();
        }
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

    private void InstancePlayer1(int index,int x,int y)
    {
        GameObject go = Instantiate(_playerPrefabs [index], GetTileCenter(x,y,0), Quaternion.identity);
        if (ChooseClasses.Classes1 == ChooseClasses._Classes.Fighter)
        {
            go.AddComponent<Figther>();
        }
        if (ChooseClasses.Classes1 == ChooseClasses._Classes.Rouge)
        {
            go.AddComponent<Rouge>();
        }
        if (ChooseClasses.Classes1 == ChooseClasses._Classes.Tank)
        {
            go.AddComponent<Tank>();
        }
        go.transform.SetParent(transform);
        Players[x, y] = go.GetComponent<Player>();
        Players[x, y].SetPosition(x, y);
        Players[x, y].IsPlayer1 = true;
    }
    private void InstancePlayer2(int index, int x, int y)
    {
        GameObject go = Instantiate(_playerPrefabs[index], GetTileCenter(x, y, 0), Quaternion.identity);
        if (ChooseClasses.Classes2 == ChooseClasses._Classes.Fighter)
        {
            go.AddComponent<Figther>();
        }
        if (ChooseClasses.Classes2 == ChooseClasses._Classes.Rouge)
        {
            go.AddComponent<Rouge>();
        }
        if (ChooseClasses.Classes2 == ChooseClasses._Classes.Tank)
        {
            go.AddComponent<Tank>();
        }
        go.transform.SetParent(transform);
        Players[x, y] = go.GetComponent<Player>();
        Players[x, y].SetPosition(x, y);
    }

    private void SpawnPlayer()
    {
        Players = new Player[14, 20];
        Player1 = _playerPrefabs[0];
        InstancePlayer1(0, Random.Range(6, 9), 19);
        Player2 = _playerPrefabs[1];
        InstancePlayer2(1, Random.Range(6, 9),0);
        
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

    private void DestroyField(int destroyRound) {
        foreach (Transform child in transform) {
            if (child.position.z == destroyRound - 0.5 || child.position.z == 20.5 - destroyRound) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
