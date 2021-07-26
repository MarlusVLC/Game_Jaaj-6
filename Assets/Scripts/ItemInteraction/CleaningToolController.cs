using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=0yHBDZHLRbQ&t=97s

public class CleaningToolController : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    
    void OnMouseDown()
    {
        // mZCoord = Camera.main.WorldToScreenPoint(
        //     transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = transform.position - GetMouseAsWorldPoint(mZCoord);
    }
    
    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint(mZCoord) + mOffset;
    }

    private Vector3 GetMouseAsWorldPoint(float zCoord)
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = zCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


}