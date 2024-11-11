using UnityEngine;
using static RandomExtention;

[RequireComponent(typeof(SpriteRenderer))]
public class GlitchEffect : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Random.ColorHSV();

        spriteRenderer.flipX = randomBool;
        spriteRenderer.flipY = randomBool;
    }
    
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }
}