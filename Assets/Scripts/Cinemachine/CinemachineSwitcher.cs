using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;
    [SerializeField] private Button playButton;
    [SerializeField] private PlayableAsset doorPivotTimeline;
    [SerializeField] private GameObject doorPivot;
    [SerializeField] private PlayableDirector director;

    private void Awake()
    {
        playButton.onClick.AddListener(TaskOnClick);

        director = doorPivot.GetComponent<PlayableDirector>();
    }

    private void TaskOnClick()
    {
        SwitchPriority();
        director.Play(doorPivotTimeline);
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
