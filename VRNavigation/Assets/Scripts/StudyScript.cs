using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyScript : MonoBehaviour
{
    public static StudyScript instance;

    public GameObject player;

    public MazeScript[] mazes;

    public GameObject readyWindow;

    public GameObject debugWindow;

    public int mazeId;

    public int condId;

    public bool isTrialRunning;

    public bool isDebugMode;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void StartScene(int mazeId, int condId, bool isDebugMode)
    {
        this.isDebugMode = isDebugMode;
        if (isDebugMode)
        {
            debugWindow.SetActive(true);
        }

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

    private void Update()
    {
        if (readyWindow.activeSelf && OVRInput.GetDown(OVRInput.Button.One))
        {
            ReadyButtonOnClick();
        }
    }

    public void StartTrial()
    {
        mazes[mazeId].startPoint.SetActive(false);
        mazes[mazeId].textures.SetActive(true);
        readyWindow.SetActive(false);
        isTrialRunning = true;
    }
}
