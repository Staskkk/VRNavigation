using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCollideScript : MonoBehaviour
{
    public GameObject mazeLeaveWarning;

    public bool isDebugVersion;

    private bool isInsideWall;
    private bool isInsideBorder;

    private void Awake()
    {
        if (isDebugVersion)
        {
#if !UNITY_EDITOR
            Component.Destroy(this);
#endif
        }
        else
        {
#if UNITY_EDITOR
            Component.Destroy(this);
#endif
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("border"))
        {
            isInsideBorder = true;
        }

        if (other.CompareTag("maze"))
        {
            isInsideWall = true;
        }

        if (!StudyScript.instance.isTrialRunning)
        {
            return;
        }

        SetWarning();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("border"))
        {
            isInsideBorder = false;
        }

        if (other.CompareTag("maze"))
        {
            isInsideWall = false;
        }

        if (!StudyScript.instance.isTrialRunning)
        {
            return;
        }

        SetWarning();
    }

    private void SetWarning()
    {
        bool isEnabled = !isInsideBorder || isInsideWall;
        mazeLeaveWarning.SetActive(isEnabled);
    }
}
