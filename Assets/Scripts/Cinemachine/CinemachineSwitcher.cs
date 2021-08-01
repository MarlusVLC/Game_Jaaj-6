using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;
    [SerializeField] private CinemachineVirtualCamera creditsCamera;
    [SerializeField] private CinemachineVirtualCamera itemsCamera;
    [SerializeField] private CinemachineBrain mainCamera;
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button itemsButton;
    [SerializeField] private PlayableAsset doorPivotTimeline;
    [SerializeField] private GameObject doorPivot;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private CleaningToolSelector cleaningToolSelector;
    private bool isBlending;

    public enum CurrentCamera
    {
        Menu,
        Gameplay,
        Credits,
        Items
    }

    public CurrentCamera _currentCamera;

    private void Awake()
    {
        playButton.onClick.AddListener(ClickPlayButton);
        creditsButton.onClick.AddListener(ClickCreditsButton);
        itemsButton.onClick.AddListener(ClickItemsButton);

        director = doorPivot.GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        isBlending = mainCamera.IsBlending;
        
        if (Input.GetAxisRaw("Cancel") != 0 && !isBlending)
        {
            cleaningToolSelector.LeaveCleaningTool();
            
            switch (_currentCamera)
            {
                case CurrentCamera.Gameplay:
                    GameplayToMenu();
                    break;
                case CurrentCamera.Credits:
                    CreditsToMenu();
                    break;
                case CurrentCamera.Items:
                    ItemsToMenu();
                    break;
                case CurrentCamera.Menu:
                    Application.Quit();
                    break;
            }
        }
    }

    private void ClickPlayButton()
    {
        if (!isBlending)
        {
            MenuToGameplay();
            director.Play(doorPivotTimeline);
            _currentCamera = CurrentCamera.Gameplay;
        }
    }

    private void ClickCreditsButton()
    {
        if (!isBlending)
        {
            MenuToCredits();
            director.Play(doorPivotTimeline);
            _currentCamera = CurrentCamera.Credits;
        }
    }
    
    private void ClickItemsButton()
    {
        if (!isBlending)
        {
            MenuToItems();
            director.Play(doorPivotTimeline);
            _currentCamera = CurrentCamera.Items;
        }
    }

    private void MenuToGameplay()
    {
        if (_currentCamera == CurrentCamera.Menu)
        {
            menuCamera.Priority = 0;
            gameplayCamera.Priority = 1;
            creditsCamera.Priority = 0;
            itemsCamera.Priority = 0;
        }
    }

    private void MenuToCredits()
    {
        if (_currentCamera == CurrentCamera.Menu)
        {
            menuCamera.Priority = 0;
            gameplayCamera.Priority = 0;
            creditsCamera.Priority = 1;
            itemsCamera.Priority = 0;
        }
    }

    private void MenuToItems()
    {
        if (_currentCamera == CurrentCamera.Menu)
        {
            menuCamera.Priority = 0;
            gameplayCamera.Priority = 0;
            creditsCamera.Priority = 0;
            itemsCamera.Priority = 1;
        }
    }

    private void GameplayToMenu()
    {
        _currentCamera = CurrentCamera.Menu;
        director.Play(doorPivotTimeline);
        menuCamera.Priority = 1;
        gameplayCamera.Priority = 0;
        creditsCamera.Priority = 0;
        itemsCamera.Priority = 0;
    }

    private void CreditsToMenu()
    {
        _currentCamera = CurrentCamera.Menu;
        director.Play(doorPivotTimeline);
        menuCamera.Priority = 1;
        gameplayCamera.Priority = 0;
        creditsCamera.Priority = 0;
        itemsCamera.Priority = 0;
    }

    private void ItemsToMenu()
    {
        _currentCamera = CurrentCamera.Menu;
        director.Play(doorPivotTimeline);
        menuCamera.Priority = 1;
        gameplayCamera.Priority = 0;
        creditsCamera.Priority = 0;
        itemsCamera.Priority = 0;
    }
}
