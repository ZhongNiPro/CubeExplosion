using UnityEngine;

public class Raycaster : MonoBehaviour
{
    private Camera _mainCamera;

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
                if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                {
                    cube.Interact();
                    Debug.Log("cube");
                }
            }
        }
    }
}
