using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CleaningToolSelector : MonoBehaviour
{
    [SerializeField] private Transform placement1;
    [SerializeField] private Transform placement2;
    [SerializeField] private Transform placement3;
    [SerializeField] private Transform placement4;
    [SerializeField] private Transform placement5;
    [SerializeField] private Transform placement6;
    [SerializeField] private CinemachineSwitcher cinemachineSwitcher;
    public Transform mainCleaningTool;

    public Transform currentCleaningTool;
    public Transform currentPlacement;
    private DragRigidbody mainDragRigidbody;

    enum Placements
    {
        None,
        Placement1,
        Placement2,
        Placement3,
        Placement4,
        Placement5,
        Placement6
    }
    private Placements _selectedPlacement;

    public LayerMask Interactive;
    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        mainDragRigidbody = mainCleaningTool.GetComponent<DragRigidbody>();
        mainDragRigidbody.HandleInputBegin(Input.mousePosition);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1000f, ~Interactive))
            {
                switch (hit.collider.name)
                {
                    case "Placement 1":
                        _selectedPlacement = Placements.Placement1;
                        break;
                    case "Placement 2": 
                        _selectedPlacement = Placements.Placement2;
                        break;
                    case "Placement 3": 
                        _selectedPlacement = Placements.Placement3;
                        break;
                    case "Placement 4": 
                        _selectedPlacement = Placements.Placement4;
                        break;
                    case "Placement 5": 
                        _selectedPlacement = Placements.Placement5;
                        break;
                    case "Placement 6": 
                        _selectedPlacement = Placements.Placement6;
                        break;
                }
            }

            if (cinemachineSwitcher._currentCamera == CinemachineSwitcher.CurrentCamera.Gameplay)
            {
                SelectCleaningTool();
            }
        }
    }

    public void LeaveCleaningTool()
    {
        currentCleaningTool.gameObject.layer = 0;
        currentCleaningTool.SetParent(currentPlacement);
        currentCleaningTool.localPosition = Vector3.zero;
    }

    private void SelectCleaningTool()
    {
        if (currentCleaningTool)
        {
            LeaveCleaningTool();
        }

        switch (_selectedPlacement)
        {
            case Placements.Placement1:
                currentPlacement = placement1;
                break;
            case Placements.Placement2:
                currentPlacement = placement2;
                break;
            case Placements.Placement3:
                currentPlacement = placement3;
                break;
            case Placements.Placement4:
                currentPlacement = placement4;
                break;
            case Placements.Placement5:
                currentPlacement = placement5;
                break;
            case Placements.Placement6:
                currentPlacement = placement6;
                break;
        }

        if (_selectedPlacement != null)
        {
            currentCleaningTool = currentPlacement.gameObject.transform.GetChild(0);
            currentCleaningTool.SetParent(mainCleaningTool);
            currentCleaningTool.localPosition = Vector3.zero;
            currentCleaningTool.gameObject.layer = LayerMask.NameToLayer("Interactive");
        }
        
    }
}