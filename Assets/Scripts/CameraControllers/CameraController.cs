using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed;

    private float viewSpeedCoefficient = 1;
    private float hiddenAccelerator = 1;

    private Vector3 oldMousePosition;

    void Update()
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
