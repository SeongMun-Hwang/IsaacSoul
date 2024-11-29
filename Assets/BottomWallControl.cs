using UnityEngine;

public class BottomWallControl : MonoBehaviour
{
    public Material wallMaterial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color color = wallMaterial.color;
        color.a = 0.3f;
        wallMaterial.color = color;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Color color = wallMaterial.color;
        color.a = 1f;
        wallMaterial.color = color;
    }
}
