using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine;
using System.Text;

public class StudyScript : MonoBehaviour
{
    public static StudyScript instance;

    public GameObject player;

    public MazeScript[] mazes;

    public MenuScript menuWindow;

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
    public string task;
    public int collisionCount;
    public float distanceTraveled;
    public float distanceRate;
    public float accumulateRotation;
    public float trialTime;

    private float trialStartTime;

    private Coroutine trackerLogCoroutine;

    private void Awake()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
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

        this.task = "FindObj1";

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
        StartLogging();
    }

    public void SetTask(string task)
    {
        SaveTaskLogs();
        this.task = task;
        if (task == "Finish")
        {
            EndTrial();
        }
    }

    private void EndTrial()
    {
        isTrialRunning = false;
        mazes[mazeId].gameObject.SetActive(false);
        if (trackerLogCoroutine != null)
        {
            StopCoroutine(trackerLogCoroutine);
        }

        debugWindow.gameObject.SetActive(false);
        menuWindow.gameObject.SetActive(true);
    }

    private void SaveTaskLogs()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/experiment_logs/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/experiment_logs/");
        }

        string logFile = Application.persistentDataPath + "/experiment_logs/"
            + participantId + "_" + blockId + "_" + groupId + "_" + maze + "_" + condition + "_" + trialId + ".csv";
        if (!File.Exists(logFile))
        {
            var sb = new StringBuilder();
            sb.AppendLine("Participant,Block,Group,Trial,Maze,Condition,Task,Session time,"
                + "Timestamp,Collision count,Dist traveled:,Accum rotation,Distance rate,Trial time");
            sb.AppendLine(GetLogCSV());
            File.AppendAllText(logFile, sb.ToString());
        }
    }

    private void StartLogging()
    {
        trackerLogCoroutine = StartCoroutine(WriteLogTrackerCoroutine());
    }

    private IEnumerator WriteLogTrackerCoroutine()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/experiment_logs/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/experiment_logs/");
        }

        while (true)
        {
            string trackerLogFile = Application.persistentDataPath + "/experiment_logs/"
                + participantId + "_" + blockId + "_" + groupId + "_" + maze + "_" + condition + "_" + trialId + "_" + task + "_tracker.csv";
            if (!File.Exists(trackerLogFile))
            {

                string csvHeaders = "Participant,Block,Group,Trial,Maze,Condition,Task,Session time,"
                    + "Pos(x),Pos(y),Pos(z),Rot(x),Rot(y),Rot(z),Timestamp,Collision count,Dist traveled:,Accum rotation,Distance rate,Trial time\r\n";
                File.AppendAllText(trackerLogFile, csvHeaders);
            }

            var sb = new StringBuilder();
            sb.AppendLine(GetTrackerLogCSV());
            File.AppendAllText(trackerLogFile, sb.ToString());
            yield return new WaitForSecondsRealtime(0.1f);
        }
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
        res += "Task:\t\t\t" + task + "\n";

        res += "Session time:\t" + sessionTime.ToString("F3") + "\n";
        res += "Position:\t" + playerPosition.ToString("F3") + "\n";
        res += "Rotation:\t" + playerRotation.ToString("F1") + "\n";
        res += "Timestamp:\t" + timestamp.ToString("MM/dd/yyyy HH:mm:ss") + "\n";

        if (isTrialRunning)
        {
            res += "Collision count:\t" + collisionCount.ToString() + "\n";
            res += "Dist traveled:\t" + distanceTraveled.ToString("F3") + "\n";
            res += "Accum rotation:\t" + accumulateRotation.ToString("F1") + "\n";
            res += "Distance rate:\t" + distanceRate.ToString("F3") + "\n";
            res += "Trial time:\t\t" + trialTime.ToString("F3") + "\n";
        }

        return res;
    }

    private string GetTrackerLogCSV()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}",
            participantId, blockId, groupId, trialId, maze, condition, task, sessionTime.ToString("F3"),
            playerPosition.x.ToString("F3"), playerPosition.y.ToString("F3"), playerPosition.z.ToString("F3"),
            playerRotation.x.ToString("F1"), playerRotation.y.ToString("F1"), playerRotation.z.ToString("F1"),
            timestamp.ToString("MM/dd/yyyy HH:mm:ss"), collisionCount.ToString(), distanceTraveled.ToString("F3"),
            accumulateRotation.ToString("F1"), distanceRate.ToString("F3"), trialTime.ToString("F3"));
        return sb.ToString();
    }

    private string GetLogCSV()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
            participantId, blockId, groupId, trialId, maze, condition, task, sessionTime.ToString("F3"),
            timestamp.ToString("MM/dd/yyyy HH:mm:ss"), collisionCount.ToString(), distanceTraveled.ToString("F3"),
            accumulateRotation.ToString("F1"), distanceRate.ToString("F3"), trialTime.ToString("F3"));
        return sb.ToString();
    }
}
