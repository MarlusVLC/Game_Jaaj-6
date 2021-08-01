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
	private Renderer renderer;
	[SerializeField] private CleaningToolSelector _cleaningToolSelector;
	[SerializeField] private CinemachineSwitcher _cinemachineSwitcher;
	[SerializeField] private GameObject gloves;
	
	public float force = 600;
	public float damping = 6;

	Transform jointTrans;
	float dragDepth;
	
	// to activate the handleInput method on the Update method. (the original code used OnMouseDrag, but there is no
	// alternative to check if the mouse is *not* being held down using MonoBehaviour.OnMouse...blalabla
	// anyway, activating it on the Update method makes it possible to click the object once, and it stays on your cursor
	private bool handleInput = false;

	// position of the object after being applied force
	private Vector3 afterForcePosition;

	// force position object's transform
	public Transform forcePosition;

	private bool isGoingForward = false;
	//DragForcePosition forcePositionScript;

	private void Start()
	{
		renderer = GetComponent<Renderer>();
		
	}

	private void Update()
	{
		if (handleInput)
		{
			HandleInput (Input.mousePosition);
			renderer.enabled = false;

			if (_cinemachineSwitcher._currentCamera == CinemachineSwitcher.CurrentCamera.Gameplay)
			{
				Cursor.visible = false;
				Destroy(gloves);
			}
			else
			{
				Cursor.visible = true;
			}
		}
		
		if (handleInput && Input.GetMouseButton(0))
		{
			if (_cleaningToolSelector.currentCleaningTool.name == "Sponge" ||
			    _cleaningToolSelector.currentCleaningTool.name == "Steel Sponge" ||
			_cleaningToolSelector.currentCleaningTool.name == "Cloth")
			{
				// making the object thrust forward when holding the mouse button
				transform.position = afterForcePosition;
				isGoingForward = true;	
			}
		}
		else
		{
			isGoingForward = false;
		}

		if (!isGoingForward)
		{
			afterForcePosition = forcePosition.position;
		}
	}

	void OnMouseDown ()
	{
		// click on object -> object's position follows cursor
		if (!handleInput)
		{
			handleInput = true;
			HandleInputBegin (Input.mousePosition);
			//forcePositionScript.HandleInputBegin(Input.mousePosition);
		}
	}

	public void HandleInputBegin (Vector3 screenPosition)
	{
		var ray = Camera.main.ScreenPointToRay (screenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Interactive")) {
				// dragDepth = CameraPlane.CameraToPointDepth (Camera.main, hit.point);
				// jointTrans = AttachJoint (hit.rigidbody, hit.point);
				AttachObject(hit.point, hit.rigidbody);
			}
		}
	}

	private void AttachObject(Vector3 attachPosition, Rigidbody attachRb)
	{
		dragDepth = CameraPlane.CameraToPointDepth (Camera.main, attachPosition);
		jointTrans = AttachJoint (attachRb, attachPosition);
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