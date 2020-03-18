using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScript : MonoBehaviour
{
    public string currentTaskName;

    public string nextTaskName;

    public SECTR_PropagationSource audioSource;

    public Material foundMaterial;

    private Material defMaterial;

    private MeshRenderer currRenderer;

    private void Awake()
    {
        currRenderer = GetComponent<MeshRenderer>();
        defMaterial = currRenderer.material;
    }

    private void OnEnable()
    {
        currRenderer.material = defMaterial;
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
            if (currRenderer != null)
            {
                currRenderer.material = foundMaterial;
            }
        }    
    }
}
