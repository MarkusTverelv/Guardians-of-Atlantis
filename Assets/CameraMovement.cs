using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform jellowTransform;
    public Transform roseTransform;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        FixedCameraFollowSmooth(mainCamera, roseTransform, jellowTransform);
    }

    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        float zoomFactor = 1.5f;
        float followTimeDelta = 0.8f;

        Vector3 midpoint = (t1.position + t2.position) / 2f;

        float distance = (t1.position - t2.position).magnitude;

        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        if (cam.orthographic)
            cam.orthographicSize = distance;

        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;

        if (cam.orthographicSize < 3.5f)
            cam.orthographicSize = 3.5f;

        if (cam.orthographicSize > 6.5f)
            cam.orthographicSize = 6.5f;
    }
}
