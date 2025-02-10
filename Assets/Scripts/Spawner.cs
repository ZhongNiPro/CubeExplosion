using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Cube _cubePrefab;

    private void OnEnable()
    {
        _cube.Destroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _cube.Destroyed -= OnCubeDestroyed;
    }

    private void OnCubeDestroyed(Cube destroyedÑube)
    {
        destroyedÑube.Destroyed -= OnCubeDestroyed;

        int minCount = 2;
        int maxCount = 6;
        int scaleRate = 2;
        float correctionRate = 0.5f;

        int randomCount = Random.Range(minCount, maxCount + 1);
        float scaleValue = destroyedÑube.transform.localScale.x;

        if (scaleValue >= Random.value)
        {
            scaleValue /= scaleRate;

            for (int i = 0; i < randomCount; i++)
            {
                float randomX = Random.value + destroyedÑube.transform.position.x - correctionRate;
                float randomY = Random.value + destroyedÑube.transform.position.y;
                float randomZ = Random.value + destroyedÑube.transform.position.z - correctionRate;

                Vector3 position = new Vector3(randomX, randomY, randomZ);

                Cube cube = Instantiate(_cubePrefab, position, Quaternion.identity);

                cube.Destroyed += OnCubeDestroyed;

                cube.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                cube.ChangeColor();
            }
        }
        else
        {
            destroyedÑube.Explode();
        }
    }
}
