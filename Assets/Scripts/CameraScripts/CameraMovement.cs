using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform jellowTransform;
    public Transform roseTransform;
    [SerializeField] bool _lockYAxis, _lockXAxis, _lockZoom;
    private Camera mainCamera;
    float lockedZoom, lockedXPos, lockedYPos;
    public bool lockZoom
    {
        get 
        {
            return _lockZoom;
        }
        set 
        {
            _lockZoom = value;
            if (value)
                lockedZoom = mainCamera.orthographicSize;
        }
    }
    public bool lockXAxis
    {
        get
        {
            return _lockXAxis;
        }
        set
        {
            _lockXAxis = value;
            if (value)
                lockedYPos = transform.position.x;
        }
    }
    public bool lockYAxis
    {
        get
        {
            return _lockYAxis;
        }
        set
        {
            _lockYAxis = value;
            if (value)
                lockedYPos = transform.position.y;
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        lockedZoom = mainCamera.orthographicSize;
        lockedXPos = transform.position.x;
        lockedYPos = transform.position.y;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    private void LateUpdate()
    {
        CameraFollow(mainCamera, roseTransform, jellowTransform);
        if (lockZoom)
            mainCamera.orthographicSize = lockedZoom;
        if (lockXAxis)
            mainCamera.transform.position = new Vector3(lockedXPos, transform.position.y);
        if (lockYAxis)
            mainCamera.transform.position = new Vector3(transform.position.x, lockedYPos);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

    }

    public void CameraFollow(Camera mainCam, Transform player1, Transform player2)
    {
        float zoomSpeed = 1.5f;
        float smoothAmount = 0.8f;

        Vector3 midpoint = (player1.position + player2.position) / 2f;

        float distance = (player1.position - player2.position).magnitude + 2;

        Vector3 newCameraPosition = midpoint - mainCam.transform.forward * distance * zoomSpeed;

        if (mainCam.orthographic)
            mainCam.orthographicSize = distance;

        mainCam.transform.position = Vector3.Slerp(new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -15), newCameraPosition, smoothAmount);

        if ((newCameraPosition - mainCam.transform.position).magnitude <= 0.05f)
            mainCam.transform.position = newCameraPosition;

        if (mainCam.orthographicSize < 9f)
            mainCam.orthographicSize = 9f;

        if (mainCam.orthographicSize > 12f)
            mainCam.orthographicSize = 12f;
    }
}
