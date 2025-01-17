using System;
using UnityEngine;

public class CubeSelector : MonoBehaviour
{
    private GameObject _cube;
    private float _scaleValue;
    private Camera _mainCamera;

    public event Action<GameObject,float> CubeSelected;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        int leftMouseButton = 0;

        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                _cube = hit.transform.gameObject;
                _scaleValue = hit.transform.localScale.x;

                CubeSelected.Invoke(_cube,_scaleValue);
            }
        }
    }
}
