using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class WashDish : MonoBehaviour
{
	public Transform sink;
    private Vector3 sinkPosition;
    private Vector3 startingPosition;
    private float washForce = 8f;

    private void Start()
    {
	    startingPosition.Set(transform.position.x, transform.position.y, transform.position.z);
        sinkPosition.Set(sink.position.x, sink.position.y, sink.position.z);
    }

    private void Update()
    {
	    if (Input.GetKey("space"))
	    {
		    transform.position = Vector3.MoveTowards(transform.position, sinkPosition, washForce * Time.deltaTime);
	    }
	    else
	    {
		    transform.position = Vector3.MoveTowards(transform.position, startingPosition, washForce * Time.deltaTime);
	    }
    }
}
