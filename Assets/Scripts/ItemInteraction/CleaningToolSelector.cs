using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CleaningToolSelector : MonoBehaviour
{
    [SerializeField] private Transform sponge;
    [SerializeField] private Transform steelSponge;
    [SerializeField] private Transform cloth;
    public Transform mainCleaningTool;

    private Transform currentCleaningTool;
    private DragRigidbody mainDragRigidbody;

    enum CleanerType
    {
        None,
        Sponge,
        SteelSponge,
        Cloth
    }

    private CleanerType _selectedCleanerType;

    public LayerMask Interactive;
    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        mainDragRigidbody = mainCleaningTool.GetComponent<DragRigidbody>();
        mainDragRigidbody.HandleInputBegin(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1000f, ~Interactive))
            {
                switch (hit.collider.name)
                {
                    case "Sponge": _selectedCleanerType = CleanerType.Sponge;
                        break;
                    case "Steel Sponge": _selectedCleanerType = CleanerType.SteelSponge;
                        break;
                    case "Cloth": _selectedCleanerType = CleanerType.Cloth;
                        break;
                }
            }
            
            SelectCleaningTool();
        }
    }

    void SelectCleaningTool()
    {
        if (currentCleaningTool)
        {
            currentCleaningTool.gameObject.layer = 0;
            currentCleaningTool.SetParent(null);
        }
        
        switch (_selectedCleanerType)
        {
            case CleanerType.Sponge: 
                currentCleaningTool = sponge;
                break;
            case CleanerType.SteelSponge: 
                currentCleaningTool = steelSponge;
                break;
            case CleanerType.Cloth: 
                currentCleaningTool = cloth;
                break;
        }

        if (_selectedCleanerType != CleanerType.None)
        {
            currentCleaningTool.SetParent(mainCleaningTool);
            currentCleaningTool.localPosition = Vector3.zero;
            currentCleaningTool.gameObject.layer = LayerMask.NameToLayer("Interactive");
        }
    }
}