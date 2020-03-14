using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Toggle[] mazeToggles;

    public Toggle[] conditionToggles;

    private bool runAutoToggle = false;

    public bool debugMode;

    public int mazeId;

    public int condId;

    public void ToggleValueChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            runAutoToggle = true;
            bool isMaze = changedToggle.CompareTag("mazeToggle");
            var toggles = isMaze ? mazeToggles : conditionToggles;
            for (int i = 0; i < toggles.Length; ++i)
            {
                if (toggles[i] != changedToggle)
                {
                    toggles[i].isOn = false;
                }
                else if (isMaze)
                {
                    mazeId = i;
                }
                else
                {
                    condId = i;
                }
            }

            runAutoToggle = false;
        }
        else if (!runAutoToggle)
        {
            changedToggle.isOn = true;
        }
    }

    public void ToggleDebugChanged(Toggle debugToggle)
    {
        debugMode = debugToggle.isOn;
    }

    private void Update()
    {
        if (gameObject.activeSelf && OVRInput.GetDown(OVRInput.Button.One))
        {
            StartButtonClick();
        }
    }

    public void StartButtonClick()
    {
        gameObject.SetActive(false);
        StudyScript.instance.ShowStartPoint(mazeId, condId);
    }
}
