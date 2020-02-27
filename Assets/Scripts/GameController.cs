﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player[,] Players { set; get; }
    public static GameController Instance { get; set; }

    private FieldController _field;
    private bool[,] _allowedMoves { set; get; }
    private Player _selectedPlayer;
    private const float _tileSize = 1f;
    private const float _tileOffset = 0.5f;
    private int _selectionX = -1;
    private int _selectionY = -1;
    [SerializeField]
    private List<GameObject> _playerPrefabs;

    private void Start()
    {
        Instance = this;
        SpawnPlayer();

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
    }

    private void SelectPlayer(int x, int y)
    {
        if (Players[x, y] == null)
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
            _selectedPlayer.transform.position = GetTileCenter(x, y);
            _selectedPlayer.SetPosition(x, y);
            Players[x, y] = _selectedPlayer;
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
        GameObject go = Instantiate(_playerPrefabs [index], GetTileCenter(x,y), Quaternion.identity);
        go.transform.SetParent(transform);
        Players[x, y] = go.GetComponent<Player>();
        Players[x, y].SetPosition(x, y);
        
    }

    private void SpawnPlayer()
    {
        Players = new Player[14, 20];
        InstancePlayer(0, Random.Range(6, 9), 0);
        InstancePlayer(1, Random.Range(6, 9),19);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (_tileSize * x) + _tileOffset;
        origin.z += (_tileSize * y) + _tileOffset;
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