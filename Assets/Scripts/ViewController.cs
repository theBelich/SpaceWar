using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private List<GameObject> _views;
    [SerializeField] private CameraController _cameraController;
    private int _currentView;

    public System.Action<int> OnViewChanges;

    void Update()
    {
        if (Mathf.Abs(_cameraPosition.localPosition.y) >= 150  && _currentView == 0)
        {
            ChangeView(1);
        }

        if (Mathf.Abs(_cameraPosition.localPosition.y) >= 700 && _currentView == 1)
        {
            ChangeView(1);
        }

        if (_cameraPosition.position.y < 150 && _currentView == 1)
        {
            ChangeView(-1);
        }

        if (_cameraPosition.position.y < 700 && _currentView == 2)
        {
            ChangeView(-1);
        }
    }

    private void ChangeView(int count)
    {
        _views[_currentView].SetActive(false);
        _currentView += count;
        _views[_currentView].SetActive(true);
        _cameraController.ChangeViewSpeedCoefficient(_currentView);
        OnViewChanges?.Invoke(_currentView);
    }
}
