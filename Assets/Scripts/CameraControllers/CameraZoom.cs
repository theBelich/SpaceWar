using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _hiddenAccelerator = 1;

    void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel < 0 && transform.localPosition.y > 3500)
        {
            return;
        }

        if (mouseWheel == 0) return;

        _hiddenAccelerator += 1;
        transform.Translate(Vector3.forward * mouseWheel * _speed * _hiddenAccelerator);

        if (_hiddenAccelerator < 1)
        {
            _hiddenAccelerator = 1;
        }

        if (_hiddenAccelerator > 1)
        {
            _hiddenAccelerator -= Time.deltaTime;
        }
    }
    
}
