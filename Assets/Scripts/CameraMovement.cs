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
        CameraFollow(mainCamera, roseTransform, jellowTransform);
    }

    public void CameraFollow(Camera mainCam, Transform player1, Transform player2)
    {
        float zoomSpeed = 1.5f;
        float smoothAmount = 0.8f;

        Vector3 midpoint = (player1.position + player2.position) / 2f;

        float distance = (player1.position - player2.position).magnitude;

        Vector3 newCameraPosition = midpoint - mainCam.transform.forward * distance * zoomSpeed;

        if (mainCam.orthographic)
            mainCam.orthographicSize = distance;

        mainCam.transform.position = Vector3.Slerp(new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -15), newCameraPosition, smoothAmount);

        if ((newCameraPosition - mainCam.transform.position).magnitude <= 0.05f)
            mainCam.transform.position = newCameraPosition;

        if (mainCam.orthographicSize < 10f)
            mainCam.orthographicSize = 10f;

        if (mainCam.orthographicSize > 18f)
            mainCam.orthographicSize = 18f;
    }
}
