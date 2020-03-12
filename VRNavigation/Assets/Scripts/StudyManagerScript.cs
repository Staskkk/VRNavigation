using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyManagerScript : MonoBehaviour
{
    public GameObject player;

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
