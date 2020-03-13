using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyScript : MonoBehaviour
{
    public static StudyScript instance;

    public GameObject player;

    public MazeScript[] mazes;

    public GameObject readyWindow;

    public int mazeId;

    public int condId;

    public bool isTrialRunning;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void ShowStartPoint(int mazeId, int condId)
    {
        this.mazeId = mazeId;
        this.condId = condId;

        foreach (var maze in mazes)
        {
            maze.gameObject.SetActive(false);
        }

        mazes[mazeId].gameObject.SetActive(true);
        mazes[mazeId].SetCondition(condId);
        mazes[mazeId].StartMaze();
    }

    public void StartPointReached(bool isReached)
    {
        readyWindow.SetActive(isReached);
    }

    public void ReadyButtonOnClick()
    {
        StartTrial();
    }

    public void StartTrial()
    {
        mazes[mazeId].startPoint.SetActive(false);
        mazes[mazeId].textures.SetActive(true);
        readyWindow.SetActive(false);
        isTrialRunning = true;
    }
}
