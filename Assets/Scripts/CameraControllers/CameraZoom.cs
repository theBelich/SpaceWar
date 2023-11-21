using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetOnView;
    [SerializeField] private Transform _highViewTarget;

    private float _hiddenAccelerator = 1;

    void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        


        if (mouseWheel < 0 && transform.localPosition.y > 3500)
        {
            return;
        }

        if (mouseWheel > 0)
        {
            _hiddenAccelerator += 1;
            transform.position = Vector3.MoveTowards(transform.position, _targetOnView.position, mouseWheel * _speed * _hiddenAccelerator);
        }

        if (mouseWheel < 0)
        {
            _hiddenAccelerator += 1;
            transform.position = Vector3.MoveTowards(transform.position, _highViewTarget.position, mouseWheel * _speed * _hiddenAccelerator);
        }

        if (_hiddenAccelerator > 1)
        {
            _hiddenAccelerator -= Time.deltaTime;
        }
    }

    public void SetTarget(Transform targetOnView, Transform highViewTarget)
    {
        _targetOnView = targetOnView;
        _highViewTarget = highViewTarget;
    }
}
