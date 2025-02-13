using UnityEngine;

public class FirstSpawn : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private void Start()
    {
        float offsetX = 3f;
        float offsetY = 0.5f;

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = new((i - 1) * offsetX, offsetY, 0);
            Instantiate(_cubePrefab, position, Quaternion.identity);
        }
    }
}
