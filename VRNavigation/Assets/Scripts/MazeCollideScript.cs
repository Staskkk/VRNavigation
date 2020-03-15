using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCollideScript : MonoBehaviour
{
    public GameObject mazeLeaveWarning;

    public LayerMask everythingMask;

    public LayerMask uiMask;

    private bool isInsideWall;
    private bool isInsideBorder;

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

        SetCollision();
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

        SetCollision();
    }

    private void SetCollision()
    {
        bool isEnabled = !isInsideBorder || isInsideWall;
        foreach (var camera in Camera.allCameras)
        {
            if (!camera.orthographic)
            {
                camera.cullingMask = isEnabled ? uiMask : everythingMask;
            }
        }

        mazeLeaveWarning.SetActive(isEnabled);
        if (isEnabled)
        {
            StudyScript.instance.collisionCount++;
        }
    }
}
