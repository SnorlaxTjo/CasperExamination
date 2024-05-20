using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDistributor : MonoBehaviour
{
    [SerializeField] OptionSaver optionsToLoad;

    [Space]
    [SerializeField] CinemachineFreeLook freeLookCamera;

    private void Start()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = optionsToLoad.mouseSesnitivity * 300;
        freeLookCamera.m_YAxis.m_MaxSpeed = optionsToLoad.mouseSesnitivity;
    }
}
