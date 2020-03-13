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

    public void ToggleValueChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            runAutoToggle = true;
            var toggles = changedToggle.CompareTag("mazeToggle") ? mazeToggles : conditionToggles;
            foreach (var toggle in toggles)
            {
                if (toggle != changedToggle)
                {
                    toggle.isOn = false;
                }
            }

            runAutoToggle = false;
        }
        else
        {
            if (!runAutoToggle)
            {
                changedToggle.isOn = true;
            }
        }
    }

    public void ToggleDebugChanged(Toggle debugToggle)
    {
        debugMode = debugToggle.isOn;
    }

    public void StartButtonClick()
    {
        gameObject.SetActive(false);
    }
}
