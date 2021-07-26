using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=0yHBDZHLRbQ&t=97s

public class NewCleaningToolController : MonoBehaviour
{

   private Camera _mainCam;
   private float _CameraZDistance;
   private void Start()
   {
      _mainCam = Camera.main;
      _CameraZDistance = _mainCam.WorldToScreenPoint(transform.position).z;
   }

   void OnMouseDrag()
   {
      var screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _CameraZDistance);
      var newWorldPosition = _mainCam.ScreenToWorldPoint(screenPosition);
      newWorldPosition.z = transform.position.z;
      transform.position = newWorldPosition;
   }
}