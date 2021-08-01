using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        SwitchPriority();
    }

    private void SwitchPriority()
    {
        if (menuCamera)
        {
            menuCamera.Priority = 0;
            gameplayCamera.Priority = 1;
        }
        else
        {
            menuCamera.Priority = 1;
            gameplayCamera.Priority = 0;
        }
    }
}
