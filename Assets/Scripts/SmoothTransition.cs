using UnityEngine;

public class SmoothTransition : MonoBehaviour
{
    public GameObject targetObject; // Assign in the Inspector or set dynamically
    public float smoothSpeed = 5f; // Adjust speed of transition

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

            Quaternion targetRotation = Quaternion.Euler(90f, 90f, 180f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }
    }
}
