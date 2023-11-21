using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed;

    [SerializeField] private Transform _targetOnView;
    [SerializeField] private Transform _highViewTarget;

    private float viewSpeedCoefficient = 1;
    private float hiddenAccelerator = 1;


    private Vector3 oldMousePosition;

    void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetOnView.position, mouseWheel * cameraMoveSpeed * hiddenAccelerator);
        }

        MouseMovement();
    }

    private void MouseMovement()
    {

        if (Input.GetMouseButtonDown(0))
        {
            oldMousePosition = Input.mousePosition;
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
        diff = diff.normalized * cameraMoveSpeed * viewSpeedCoefficient * hiddenAccelerator * Time.deltaTime;

        if (hiddenAccelerator < 10)
        {
            hiddenAccelerator += Time.deltaTime;
        }

        transform.Translate(diff);
    }

    public void SetTarget(Transform targetOnView, Transform highViewTarget)
    {
        _targetOnView = targetOnView;
        if (highViewTarget == null)
        {
            _highViewTarget = targetOnView;
        }
        _highViewTarget = highViewTarget;
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
