using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private Transform currentObject;
    private float rotateSpeed = 100f;
    void Update()
    {
        // rotating the current object using the horizontal and vertical inputs
        currentObject.transform.Rotate(Input.GetAxisRaw("Vertical") * rotateSpeed * Time.deltaTime, 
            -Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime, 0f, Space.World);
    }
}
