using UnityEngine;

public class Destroyer : MonoBehaviour
{

    [SerializeField] private CubeSelector _cubeSelector;

    private void OnEnable()
    {
        _cubeSelector.CubeSelected += CubeDestroy;
    }

    private void OnDisable()
    {
        _cubeSelector.CubeSelected -= CubeDestroy;
    }

    private void CubeDestroy(GameObject currentCube, float scaleValue)
    {
        Destroy(currentCube);
    }
}
