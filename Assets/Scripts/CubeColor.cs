using UnityEngine;

public class CubeColor : MonoBehaviour
{
    public void ChangeColor(Cube cube)
    {
        cube.Renderer.material.color = Random.ColorHSV();
    }
}
