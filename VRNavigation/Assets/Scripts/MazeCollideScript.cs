using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCollideScript : MonoBehaviour
{
    public GameObject worldObjects;
    public MeshRenderer mazeRenderer;
    public GameObject mazeLeaveWarning;

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

        SetWarning();
    }

    private void SetWarning()
    {
        bool isEnabled = !isInsideBorder || isInsideWall;
        mazeRenderer.enabled = !isEnabled;
        worldObjects.SetActive(!isEnabled);
        mazeLeaveWarning.SetActive(isEnabled);
    }
}
