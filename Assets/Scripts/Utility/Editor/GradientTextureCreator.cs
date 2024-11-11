using UnityEngine;

public class GradientTextureCreator : ScriptableObject
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private Vector2Int size = new(128, 4);

    public Texture2D GenerateTexture()
    {
        Texture2D texture = new(size.x, size.y);
        for (int h = 0; h < texture.height; h++)
        {
            for (int w = 0; w < texture.width; w++)
            {
                texture.SetPixel(w, h, gradient.Evaluate((float)w / texture.width));
            }
        }

        texture.Apply();

        return texture;
    }
}