using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] OptionSaver optionsToSaveTo;

    [Header("Mouse Sensitivity")]
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityDisplayText;

    public void SaveOptions()
    {
        optionsToSaveTo.mouseSesnitivity = mouseSensitivitySlider.value;
    }

    public void LoadOptions()
    {
        mouseSensitivitySlider.value = optionsToSaveTo.mouseSesnitivity;
        sensitivityDisplayText.text = mouseSensitivitySlider.value.ToString();
    }

    public void ChangeMouseSensitivity()
    {
        sensitivityDisplayText.text = mouseSensitivitySlider.value.ToString();
    }
}
