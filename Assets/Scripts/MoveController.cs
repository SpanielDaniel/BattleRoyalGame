using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static MoveController Instance { get; set; }

    public GameObject HighlightPrefab;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHightlightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);


        if (go == null)
        {
            go = Instantiate(HighlightPrefab);
            highlights.Add(go);
        }

        return go;
    }

    public void HighLightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHightlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i+0.5f, 0.5f, j+0.5f);
                }
            }
        }
    }

    public void HideHighlights()
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }
}
