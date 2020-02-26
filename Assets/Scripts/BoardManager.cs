using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; set; }
    private bool[,] allowedMoves { set; get; }

    private Player selectedPlayer;
    public Player[,] Players { set; get; }

    private const float tileSize = 1f;
    private const float tileOffset = 0.5f;
    private int selectionX = -1;
    private int selectionY = -1;
    [SerializeField]
    private List<GameObject> playerPrefabs;

    private void Start()
    {
        Instance = this;
        SpawnPlayer(0,7,0);
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessboard();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedPlayer == null)
                {
                    SelectPlayer(selectionX, selectionY);
                }
                else
                {
                    MovePlayer(selectionX, selectionY);
                }
            }
        }
    }

    private void SelectPlayer(int x, int y)
    {
        if (Players[x, y] == null)
            return;

        allowedMoves = Players[x, y].PossibleMove();
        selectedPlayer = Players[x, y];
        MoveController.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MovePlayer(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Players [selectedPlayer.CurrentX, selectedPlayer.CurrentY] = null;
            selectedPlayer.transform.position = GetTileCenter(x, y);
            selectedPlayer.SetPosition(x, y);
            Players[x, y] = selectedPlayer;
        }

        selectedPlayer = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f,LayerMask.GetMask("BoardPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnPlayer(int index,int x,int y)
    {
        Players = new Player[14, 20];
        GameObject go = Instantiate(playerPrefabs [index], GetTileCenter(x,y), Quaternion.identity);
        go.transform.SetParent(transform);
        Players[x, y] = go.GetComponent<Player>();
        Players[x, y].SetPosition(x, y);
        
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * y) + tileOffset;
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
        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY+1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
}
