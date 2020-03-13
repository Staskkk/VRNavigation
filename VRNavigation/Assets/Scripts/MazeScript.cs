using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    public int condId;

    public GameObject[] conds;

    public GameObject startPoint;

    public GameObject textures;

    public void SetCondition(int condId)
    {
        foreach (var cond in conds)
        {
            cond.SetActive(false);
        }

        this.condId = condId;
        conds[condId].SetActive(true);
    }

    public void StartMaze()
    {
        startPoint.SetActive(true);
    }
}
