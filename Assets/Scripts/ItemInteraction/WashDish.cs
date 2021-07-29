using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class WashDish : MonoBehaviour
{
	[SerializeField] private Transform currentObject;
	public Transform sink;
    private Vector3 sinkPosition;
    private Vector3 startingPosition;
    private float washForce = 8f;

    private void Start()
    {
	    startingPosition.Set(currentObject.transform.position.x, currentObject.transform.position.y, currentObject.transform.position.z);
        sinkPosition.Set(sink.position.x, sink.position.y, sink.position.z);
    }

    private void Update()
    {
	    if (Input.GetKey("space"))
	    {
		    currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, sinkPosition, washForce * Time.deltaTime);
	    }
	    else
	    {
		    currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, startingPosition, washForce * Time.deltaTime);
	    }
    }
}
