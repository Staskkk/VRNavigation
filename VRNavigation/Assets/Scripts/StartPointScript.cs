using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointScript : MonoBehaviour
{
    private Renderer pointRenderer;

    void Start()
    {
        pointRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pointRenderer.enabled = false;
            StudyScript.instance.StartPointReached(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pointRenderer.enabled = true;
            StudyScript.instance.StartPointReached(false);
        }
    }
}
