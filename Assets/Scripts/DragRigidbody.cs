using System;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Drag a rigidbody with the mouse using a spring joint.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DragRigidbody : MonoBehaviour
{
	public float force = 600;
	public float damping = 6;

	Transform jointTrans;
	float dragDepth;
	
	// to activate the handleInput method on the Update method. (the original code used OnMouseDrag, but there is no
	// alternative to check if the mouse is *not* being held down using MonoBehaviour.OnMouse...blalabla
	// anyway, activating it on the Update method makes it possible to click the object once, and it stays on your cursor
	private bool handleInput = false;

	// to get the starting position of the object
	private Vector3 startPosition;

	// current position of the object
	private Vector3 currentPosition;

	// position after applying force
	public Transform forwardPosition;

	private bool isGoingForward = false;
	
	private void Start()
	{
		startPosition = this.transform.position;
	}
	
	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (handleInput)
			{
				// making the object thrust forward when holding the mouse button
				transform.position = currentPosition;
			}
			
			isGoingForward = true;
		}
		else
		{
			isGoingForward = false;
		}
		
		if (handleInput)
		{
			HandleInput (Input.mousePosition);
		}

		if (!isGoingForward)
		{
			currentPosition.Set(forwardPosition.position.x, forwardPosition.position.y, forwardPosition.position.z + 0.01f);
		}

		// Debug.Log(isGoingForward);
	}
	

	void OnMouseDown ()
	{
		// click on object -> object's position follows cursor
		// click on object again -> object goes back to it's starting position
		if (!handleInput)
		{
			handleInput = true;
			HandleInputBegin (Input.mousePosition);
		}
	}

	
	private void OnMouseDrag()
	{
	}
	

	public void HandleInputBegin (Vector3 screenPosition)
	{
		var ray = Camera.main.ScreenPointToRay (screenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Interactive")) {
				dragDepth = CameraPlane.CameraToPointDepth (Camera.main, hit.point);
				jointTrans = AttachJoint (hit.rigidbody, hit.point);
			}
		}
	}
	
	public void HandleInput (Vector3 screenPosition)
	{
		if (jointTrans == null)
			return;
		var worldPos = Camera.main.ScreenToWorldPoint (screenPosition);
		jointTrans.position = CameraPlane.ScreenToWorldPlanePoint (Camera.main, dragDepth, screenPosition);
	}
	
	public void HandleInputEnd (Vector3 screenPosition)
	{
		Destroy (jointTrans.gameObject);
	}
	
	Transform AttachJoint (Rigidbody rb, Vector3 attachmentPosition)
	{
		GameObject go = new GameObject ("Attachment Point");
		go.hideFlags = HideFlags.HideInHierarchy; 
		go.transform.position = attachmentPosition;
		
		var newRb = go.AddComponent<Rigidbody> ();
		newRb.isKinematic = true;
		
		var joint = go.AddComponent<ConfigurableJoint> ();
		joint.connectedBody = rb;
		joint.configuredInWorldSpace = true;
		joint.xDrive = NewJointDrive (force, damping);
		joint.yDrive = NewJointDrive (force, damping);
		joint.zDrive = NewJointDrive (force, damping);
		joint.slerpDrive = NewJointDrive (force, damping);
		joint.rotationDriveMode = RotationDriveMode.Slerp;
		
		return go.transform;
	}
	
	private JointDrive NewJointDrive (float force, float damping)
	{
		JointDrive drive = new JointDrive ();
		drive.mode = JointDriveMode.Position;
		drive.positionSpring = force;
		drive.positionDamper = damping;
		drive.maximumForce = Mathf.Infinity;
		return drive;
	}
}