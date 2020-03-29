using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScript : MonoBehaviour
{
    public string currentTaskName;

    public string nextTaskName;

    public SECTR_PropagationSource audioSource;

    public Material foundMaterial;

    public Material defMaterial;

    public MeshRenderer objectrRenderer;

    private void OnEnable()
    {
        if (objectrRenderer != null)
        {
            objectrRenderer.material = defMaterial;
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StudyScript.instance.task == currentTaskName)
        {
            StudyScript.instance.SetTask(nextTaskName);
            if (objectrRenderer != null)
            {
                objectrRenderer.material = foundMaterial;
            }
        }    
    }
}
