using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyScript : MonoBehaviour
{
    public static StudyScript instance;

    public GameObject player;

    public GameObject[] mazes;
    public GameObject[] conditions;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartTrial();
        }
    }

    public void StartTrial()
    {
        var startPoint = GameObject.FindGameObjectWithTag("startLocation");
        startPoint.GetComponent<Renderer>().enabled = true;
    }
}
