using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Toggle[] mazeToggles;

    public Toggle[] condToggles;

    public Toggle debugToggle;

    public TMP_InputField[] inputs;

    private bool runAutoToggle = false;

    public bool isDebugMode;

    public int mazeId;

    public int condId;

    private void Start()
    {
        mazeToggles[PlayerPrefs.GetInt("mazeToggle", 0)].isOn = true;
        condToggles[PlayerPrefs.GetInt("condToggle", 0)].isOn = true;
        debugToggle.isOn = PlayerPrefs.GetInt("debugToggle", 0) != 0;

        foreach (var inputField in inputs)
        {
            string storedValue = PlayerPrefs.GetString(inputField.name, string.Empty);
            if (!string.IsNullOrWhiteSpace(storedValue))
            {
                inputField.text = storedValue;
            }
        }
    }

    public void ToggleValueChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            runAutoToggle = true;
            bool isMaze = changedToggle.CompareTag("mazeToggle");
            var toggles = isMaze ? mazeToggles : condToggles;
            for (int i = 0; i < toggles.Length; ++i)
            {
                if (toggles[i] != changedToggle)
                {
                    toggles[i].isOn = false;
                }
                else if (isMaze)
                {
                    mazeId = i;
                    PlayerPrefs.SetInt("mazeToggle", mazeId);
                }
                else
                {
                    condId = i;
                    PlayerPrefs.SetInt("condToggle", condId);
                }
            }

            PlayerPrefs.Save();
            runAutoToggle = false;
        }
        else if (!runAutoToggle)
        {
            changedToggle.isOn = true;
        }
    }

    public void ToggleDebugChanged(Toggle debugToggle)
    {
        isDebugMode = debugToggle.isOn;
        PlayerPrefs.SetInt("debugToggle", isDebugMode ? 1 : 0);
        PlayerPrefs.Save();
    }

    ////private void Update()
    ////{
    ////    if (gameObject.activeSelf && OVRInput.GetDown(OVRInput.Button.One))
    ////    {
    ////        StartButtonClick();
    ////    }
    ////}

    public void StartButtonClick()
    {
        gameObject.SetActive(false);
        StudyScript.instance.StartScene(inputs[0].text, inputs[1].text, inputs[2].text, inputs[3].text,
            mazeId, condId, isDebugMode);
    }

    public void ButtonIncrement(TMP_InputField input)
    {
        int newVal = Convert.ToInt32(input.text.Substring(1)) + 1;
        SetInputValue(input, newVal);
    }

    public void ButtonDecrement(TMP_InputField input)
    {
        int newVal = Convert.ToInt32(input.text.Substring(1)) - 1;
        SetInputValue(input, newVal);
    }

    private void SetInputValue(TMP_InputField input, int value)
    {
        input.text = input.text[0].ToString() + value;
        PlayerPrefs.SetString(input.name, input.text);
        PlayerPrefs.Save();
    }
}
