using ItemInteraction;
using UnityEngine;

public class WashDish : MonoBehaviour
{
	[SerializeField] private Transform currentObject;
	public Transform sink;

	private Dish currentDish;
    private Vector3 sinkPosition;
    private Vector3 startingPosition;
    private float washForce = 8f;
    private bool isMovingDown = false;

    private void Start()
    {
	    SetObject(ObjectController.Instance.CurrentObject);
	    startingPosition = currentObject.transform.position;
        sinkPosition = sink.position;
        ObjectController.Instance.OnObjectChange += SetObject;
    }

    private void Update()
    {
	    if (Input.GetKey("space"))
	    {
		    currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, sinkPosition, washForce * Time.deltaTime);
		    if (Vector3.Distance(currentObject.position, sinkPosition) < 0.1f && currentDish.CleanLevel == 1)
		    {
			    currentDish.CleanLevel = 2;
			    Debug.Log("This dish is almost cleansed");
		    }
	    }
	    else
	    {
		    currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, startingPosition, washForce * Time.deltaTime);
		    if (Vector3.Distance(currentObject.position, startingPosition) < 0.1f && currentDish.CleanLevel == 2)
		    {
			    currentDish.EndCleaning();
			    currentDish.CleanLevel = 3;
			    Debug.Log("This dish is FINALLY cleansed");
		    }
	    }
    }

    private void SetObject(Transform obj)
    {
	    currentObject = obj;
	    currentDish = obj.GetComponent<Dish>();
    }
    
    
}
