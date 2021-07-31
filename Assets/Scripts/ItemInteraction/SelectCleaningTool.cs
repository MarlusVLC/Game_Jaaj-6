using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCleaningTool : MonoBehaviour
{
    [SerializeField] private Transform sponge;
    [SerializeField] private Transform steelSponge;
    [SerializeField] private Transform cloth;
    [SerializeField] private Transform wand;
    [SerializeField] private Transform blowTorch;
    [SerializeField] private Transform book;
    public Transform mainCleaningTool;

    enum CleanerType
    {
        None,
        Sponge,
        SteelSponge,
        Cloth,
        Wand,
        BlowTorch,
        Book
    }

    private CleanerType _selectedCleanerType;

    public LayerMask Interactive;
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        switch (_selectedCleanerType)
        {
            case CleanerType.Sponge: sponge.transform.position = mainCleaningTool.transform.position;
                sponge.rotation = mainCleaningTool.rotation;
                break;
            case CleanerType.SteelSponge: steelSponge.transform.position = mainCleaningTool.transform.position;
                steelSponge.rotation = mainCleaningTool.rotation;
                break;
            case CleanerType.Cloth: cloth.transform.position = mainCleaningTool.transform.position;
                cloth.rotation = mainCleaningTool.rotation;
                break;
            case CleanerType.Wand: 
        }
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000f, ~Interactive))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
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
        }
    }
}

