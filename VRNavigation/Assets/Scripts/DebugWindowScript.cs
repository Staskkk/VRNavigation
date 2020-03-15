using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugWindowScript : MonoBehaviour
{
    public TextMeshProUGUI logText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLog(string logString)
    {
        logText.text = logString;
    }
}
