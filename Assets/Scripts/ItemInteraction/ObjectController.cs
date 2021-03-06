using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ItemInteraction;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private Transform currentObject;
    [SerializeField] private float rotateSpeed = 100f;

    public Dish CurrentDish { get; private set; }

    private static ObjectController _instance;

    public static ObjectController Instance => _instance;
    public Transform CurrentObject
    {
        get => currentObject;
        set => currentObject = value;
    }
    
    public event Action<Transform> OnObjectChange = delegate {  };    


    private void Awake()
    {
        if (_instance && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        // rotating the current object using the horizontal and vertical inputs
        currentObject.transform.Rotate(Input.GetAxisRaw("Vertical") * rotateSpeed * Time.deltaTime, 
            -Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime, 0f, Space.World);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DebugDishCleanliness();
        }
    }

    public void SetObject(MonoCache obj)
    {
        obj.SetActive(true);
        currentObject = obj.Transform;
        CurrentDish = currentObject.GetComponent<Dish>();
        if (CurrentDish.CleanLevel > 1)
        {
            CurrentDish.ResetDirtness();
        }
        
        OnObjectChange(currentObject);
    }

    public void DebugDishCleanliness()
    {
        Debug.Log($"This dish has {CurrentDish.CleanLevel} levels of cleanliness");
    }

}
