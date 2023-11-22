using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;    
    [SerializeField] private Transform _targetOnView;
    [SerializeField] private float _cameraMoveSpeed;
    [SerializeField] private float _cameraMoveToTargetSpeed;

    private float viewSpeedCoefficient = 1;
    private float hiddenAccelerator = 1;

    private int clickCount;
    private float timeBetweenClicks;
    private const float TIME_TO_DOUBLECLICK = 0.5f;

    private Vector3 starPositon;
    private bool InPosition = true;

    private Vector3 oldMousePosition;

    void Update()
    {
        MouseMovement();
        MoveToTarget();
    }

    private void MouseMovement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            oldMousePosition = Input.mousePosition;
            clickCount++;
        }

        if (!Input.GetMouseButton(0))
        {
            if (hiddenAccelerator > 1)
            {
                hiddenAccelerator -= 1;
            }
            return;
        }

        var diff = oldMousePosition - Input.mousePosition;
        diff = new Vector3(diff.x, 0, diff.y);
        diff = diff.normalized * _cameraMoveSpeed * viewSpeedCoefficient * hiddenAccelerator * Time.deltaTime;

        if (hiddenAccelerator < 10)
        {
            hiddenAccelerator += Time.deltaTime;
        }

        transform.Translate(diff);
    }

    private void MoveToTarget()
    {
        if (clickCount != 0)
        {
            timeBetweenClicks += Time.deltaTime;
        }

        if (timeBetweenClicks > TIME_TO_DOUBLECLICK)
        {
            timeBetweenClicks = 0;
            clickCount = 0;
            return;
        }

        if (clickCount == 2)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
            {
                starPositon = hitInfo.transform.position;
                starPositon.y = 0;
                InPosition = false;
            }
            clickCount = 0;
        }

        if (InPosition)
        {
            return;
        }

        if (transform.position != starPositon)
        {
            transform.position = Vector3.MoveTowards(transform.position, starPositon, _cameraMoveToTargetSpeed);
        }
        else
        {
            InPosition = true;
        }
    }

    public void SetTarget(Transform targetOnView)
    {
        _targetOnView = targetOnView;
        
    }

    public void ChangeViewSpeedCoefficient(int currentViewIndex)
    {
        switch (currentViewIndex)
        {
            case 0:
                viewSpeedCoefficient = 1;
                break;
            case 1:
                viewSpeedCoefficient = 10;
                break;
            case 2:
                viewSpeedCoefficient = 30;
                break;

            default:
                break;
        }
    }
}
