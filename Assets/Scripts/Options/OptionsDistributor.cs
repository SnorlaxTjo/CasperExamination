using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDistributor : MonoBehaviour
{
    [SerializeField] OptionSaver optionsToLoad;

    [Space]
    [SerializeField] CinemachineFreeLook freeLookCamera;

    //Distributes all the options from the ScriptableObject to the stuff that needs it upon loading the scene
    private void Start()
    {
        freeLookCamera.m_XAxis.m_MaxSpeed = optionsToLoad.mouseSesnitivity * 300;
        freeLookCamera.m_YAxis.m_MaxSpeed = optionsToLoad.mouseSesnitivity;
    }
}
