using UnityEngine;

public class AniLaser : MonoBehaviour {
    Vector2 size;
    float width;
    private SpriteRenderer spriteRendererComponent;

    private void Awake()
    {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update () {
        Vector2 size = spriteRendererComponent.size;
        width = size.x;
        if (width > -2.7)
        {
            width -= Time.deltaTime*10;
        }
        else
        {
            width -= Time.deltaTime*2;
        }
        size = new Vector2(width, size.y);
        GetComponent<SpriteRenderer>().size = size;
    }
}
