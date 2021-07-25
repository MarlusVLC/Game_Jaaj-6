using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ShelfHighlight : MonoBehaviour
{
    public Color startColor;
    public Color mouseOverColor;
    
    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.SetColor("_Color", mouseOverColor);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetColor("_Color", startColor);
    }
}
