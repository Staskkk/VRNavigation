using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class StudyScript : MonoBehaviour
{
    public static StudyScript instance;

    public GameObject player;

    public MazeScript[] mazes;

    public GameObject readyWindow;

    public DebugWindowScript debugWindow;

    public int mazeId;

    public int condId;

    public bool isTrialRunning;

    public bool isDebugMode;

    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public float sessionTime;
    public DateTime timestamp;
    public string participantId;
    public string blockId;
    public string groupId;
    public string trialId;
    public string maze;
    public string condition;
    public int collisionCount;
    public float distanceTraveled;
    public float distanceRate;
    public float accumulateRotation;
    public float trialTime;

    private float trialStartTime;

    private void Awake()
    {
        instance = this;
    }

    public void StartScene(string participantId, string blockId, string groupId, string trialId,
        int mazeId, int condId, bool isDebugMode)
    {
        this.distanceTraveled = 0;
        this.accumulateRotation = 0;
        this.collisionCount = 0;
        this.trialTime = 0;
        this.participantId = participantId;
        this.blockId = blockId;
        this.groupId = groupId;
        this.trialId = trialId;
        this.trialStartTime = Time.unscaledTime;
        this.maze = "Maze" + mazeId;
        switch (this.condId)
        {
            case 0:
                this.condition = "NoCues";
                break;
            case 1:
                this.condition = "VisualCues";
                break;
            case 2:
                this.condition = "AudioCues";
                break;
            case 3:
                this.condition = "OlfactionCues";
                break;
        }

        this.isDebugMode = isDebugMode;
        if (isDebugMode)
        {
            debugWindow.gameObject.SetActive(true);
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
        ////if (readyWindow.activeSelf && OVRInput.GetDown(OVRInput.Button.One))
        ////{
        ////    ReadyButtonOnClick();
        ////}
        
        if (isTrialRunning)
        {
            distanceTraveled += Vector3.Distance(playerPosition, player.transform.position);
            accumulateRotation += Mathf.Abs(Mathf.DeltaAngle(playerRotation.y, player.transform.rotation.eulerAngles.y));
            distanceRate = mazes[mazeId].optimalDistance / distanceTraveled;
            trialTime = Time.unscaledTime - trialStartTime;
        }
        
        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation.eulerAngles;
        sessionTime = Time.unscaledTime;
        timestamp = DateTime.Now;

        debugWindow.ShowLog(GetLogString());
    }

    public void StartTrial()
    {
        mazes[mazeId].startPoint.SetActive(false);
        mazes[mazeId].textures.SetActive(true);
        readyWindow.SetActive(false);
        isTrialRunning = true;
    }

    private string GetLogString()
    {
        string res = string.Empty;

        res += "Participant Id:\t" + participantId + "\n";
        res += "Block Id:\t\t" + blockId + "\n";
        res += "Group Id:\t\t" + groupId + "\n";
        res += "Trial Id:\t\t" + trialId + "\n";
        res += "Maze:\t\t\t" + maze + "\n";
        res += "Condition:\t\t" + condition + "\n";

        res += "Session time:\t" + sessionTime.ToString("F3", CultureInfo.InvariantCulture) + "\n";
        res += "Position:\t" + playerPosition.ToString("F3") + "\n";
        res += "Rotation:\t" + playerRotation.ToString("F1") + "\n";
        res += "Timestamp:\t" + timestamp.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture) + "\n";

        if (isTrialRunning)
        {
            res += "Collision count:\t" + collisionCount.ToString(CultureInfo.InvariantCulture) + "\n";
            res += "Dist traveled:\t" + distanceTraveled.ToString("F3", CultureInfo.InvariantCulture) + "\n";
            res += "Accum rotation:\t" + accumulateRotation.ToString("F1", CultureInfo.InvariantCulture) + "\n";
            res += "Distance rate:\t" + distanceRate.ToString("F3", CultureInfo.InvariantCulture) + "\n";
            res += "Trial time:\t\t" + trialTime.ToString("F3", CultureInfo.InvariantCulture) + "\n";
        }

        return res;
    }
}
