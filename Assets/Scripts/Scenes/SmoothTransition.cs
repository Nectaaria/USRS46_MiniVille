using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTransition : MonoBehaviour
{
    public GameObject targetObject { get; set; } // Assign in the Inspector or set dynamically
    public float smoothSpeed = 5f; // Adjust speed of transition

    public List<GameObject> targets;

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = GameObject.Find("CameraCardPlayer1");
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Smoothly interpolate the position of the camera towards the target object
            transform.position = Vector3.Lerp(
                transform.position,
                targetObject.transform.position,
                Time.deltaTime * smoothSpeed
            );

            //Quaternion targetRotation = Quaternion.Euler(90f, 90f, 180f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetObject.transform.rotation, Time.deltaTime * smoothSpeed);
        }
    }

    public void SetCameraPosition(int positionIndex)
    {
        if (positionIndex < 0 || positionIndex >= targets.Count)
            return;

        targetObject = targets[positionIndex];
    }
}
