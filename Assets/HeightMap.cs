using UnityEngine;

public class terrain : MonoBehaviour
{
    // Start is called before the first frame update

    public int width = 256;
    public int height = 256;

    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetxIncreaser = 0f;
    public float offsetyIncreaser = 0f;

    public float scale = 20f;
    public float scale2 = 1f;
    public float parallaxHeight = 0.1f;
    public float scaleIncreaser = 0f;

    void Update()
    {
        Renderer obj = GetComponent<Renderer>();
        // obj.material.EnableKeyword("_HEIGHTMAP");
            // obj.material.EnableKeyword("_PARRALAXMAP");
                // obj.material.SetTexture("_Parallax", 4);
            // obj.material.SetInt("_Parallax", parralaxHeight);
        // obj.material.SetTexture("_ParallaxMap", GenerateTexture());
        obj.material.EnableKeyword("_NORMALMAP");
        obj.material.EnableKeyword("_HEIGHTMAP");
        obj.material.EnableKeyword("_HEIGHTSCALE");
        obj.material.SetTexture("_BumpMap", GenerateTexture(width, height, offsetX, offsetY, scale));
        obj.material.SetFloat("_Parallax", parallaxHeight);
        obj.material.SetTexture("_ParallaxMap", GenerateTexture(width, height, offsetX + 1000, offsetY + 1000, scale2));
        offsetX+=offsetxIncreaser;
        offsetY+=offsetyIncreaser;
        scale+=scaleIncreaser;
    }

    Texture2D GenerateTexture(int width, int height, float offsetX, float offsetY, float scale) {
        Texture2D texture = new Texture2D(width, height);

        //GENERATE A PERLIN NOISE MAP

        for(int x = 0; x < width; x ++) {
            for(int y = 0; y < height; y++) {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return(texture);
    }

    Color CalculateColor(int x, int y) {
        float xCord = (float)x / width * scale + offsetX;
        float yCord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCord, yCord);
        return new Color(sample, sample, sample);
    }
}
