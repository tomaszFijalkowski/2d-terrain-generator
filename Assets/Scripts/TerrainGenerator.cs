using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Texture")]
    [SerializeField] private int quality = 3;
    [SerializeField] private int sideLength;

    [Header("Perlin Noise")]
    [SerializeField] private int octaves = 1;
    [SerializeField] private float persistence = 1f;
    [SerializeField] private float lacunarity = 1f;
    [SerializeField] private int seed;
    
    [Header("Island")]
    [Range(0f, 100f)]
    [SerializeField] private float exponent = 4;
    [Range(0f, 10000f)]
    [SerializeField] private float multiplier = 16;

    [Header("Terrain")]
    [Range(0f, 0.66f)]
    [SerializeField] private float seaLevel;
    [Range(0f, 16f)]
    [SerializeField] private float roughness;
    [Range(-0.66f, 0.33f)]
    [SerializeField] private float elevationIndex;
    [Range(-0.66f, 0.66f)]
    [SerializeField] private float humidityIndex;
    
    private new Renderer renderer;
    private ColorMap colorMap;

    // Slider parameter methods
    public void AdjustQuality(float value)
    {
        quality = (int) value;
    }
    
    public void AdjustSeaLevel(float value)
    {
        seaLevel = value;
        GenerateTerrain(0);
    }
    
    public void AdjustRoughness(float value)
    {
        roughness = value;
        GenerateTerrain(0);
    }
    
    public void AdjustElevation(float value)
    {
        elevationIndex = value;
        GenerateTerrain(0);
    }
    
    public void AdjustHumidity(float value)
    {
        humidityIndex = value;
        GenerateTerrain(0);
    }

    public void TextureFocus()
    {
        GenerateTerrain(quality);
    }

    public void GenerateNewSeed()
    {
        seed = Random.Range(-9999, 9999);
        GenerateTerrain(quality);
    }
    
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        colorMap = new ColorMap();
        GenerateNewSeed();
    }
    
    private void GenerateTerrain(int qualityParam)
    {
        sideLength = (int) (Screen.height / 8f * Mathf.Pow(2, qualityParam));
        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        var texture = new Texture2D(sideLength, sideLength);

        for (var x = 0; x < sideLength; x++)
        {
            for (var y = 0; y < sideLength; y++)
            {
                var color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y)
    {
        var xCoord = (float) x / sideLength;
        var yCoord = (float) y / sideLength;

        var elevation = CalculatePerlinNoise(xCoord, yCoord, seed);
        var humidity = CalculatePerlinNoise(xCoord, yCoord, seed + 1);
        
        elevation += elevationIndex;
        humidity += humidityIndex;
        
        ShapeIntoAnIsland(x, y, ref elevation);

        return colorMap[elevation, humidity, seaLevel];
    }

    private float CalculatePerlinNoise(float xCoord, float yCoord, int offset)
    {
        var noise = 0f;
        var frequency = 1f;
        var amplitude = 1f;
        var totalAmplitude = 0f;

        for (var i = 0; i < octaves; i++)
        {
            // Safety break to avoid unexpected results
            if (frequency > 1E+5) break;
            
            noise += Mathf.PerlinNoise(xCoord * (i == 0 ? roughness : frequency) + offset,
                                       yCoord * (i == 0 ? roughness : frequency) + offset) * amplitude;
            totalAmplitude += amplitude;
            amplitude *= persistence;
            frequency *= lacunarity;
        }

        // Normalise noise to 0..1 range
        noise /= totalAmplitude;

        return noise;
    }

    private void ShapeIntoAnIsland(int x, int y, ref float elevation)
    {
        // Euclidean distance to the center where distance = 0..1
        var distanceToCenter = (float) Math.Sqrt(
                                   Math.Pow(x - sideLength / 2, 2) +
                                   Math.Pow(y - sideLength / 2, 2)) / sideLength;

        // Shape into an island
        elevation = elevation - Mathf.Pow(distanceToCenter, exponent) * multiplier;
    }
}