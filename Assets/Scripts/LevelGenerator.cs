using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class LevelGenerator : MonoBehaviour {
    public int GridSize;
    public List<GameObject> Tiles;
    public List<GameObject> GeneratedTiles;
    public float Scale;
    private float[,] noiseMap;
    public NavMeshSurface surface;
    public GameObject player;
    public GameObject level;

    // Use this for initialization
    void Start () {
        if(GridSize <= 0)
        {
            GridSize = 25;
        }
        noiseMap = GenerateNoiseMap(GridSize, GridSize, Scale);
        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < (GridSize); x++)
            {
                GameObject tile = GenerateTile(x, y);
                GeneratedTiles.Add(tile);

            }
        }
        surface.BuildNavMesh();
        Vector3 pos = new Vector3(0, 0, 0);
        Instantiate(player, pos, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

    }
    public GameObject GenerateTile(int x, int y)
    {
        GameObject tile = null;
            
        float noise = noiseMap[x, y];
        float temp = 0;
        if (noise <= 0.2)
        {
            temp = 0;
        }
        else if(noise > 0.2 && noise <= 0.4) 
        {
            temp = 1;
        }
        else if (noise > 0.4 && noise <= 0.6)
        {
            temp = 2;
        }
        else if (noise > 0.6 && noise <= 1)
        {
            temp = 3;
        }

        tile = Instantiate(Tiles[(int)temp]);
        tile.transform.parent = level.transform;
        tile.transform.position = new Vector3(x * 1, temp, y * 1);
        tile.name = y + "-" + x + "-" + temp;
        return tile;

    }

    public List<GameObject> SelectTiles(GameObject origin, int range)
    {
        List<GameObject> selectedTiles = new List<GameObject>();

        string[] elements = origin.name.Split('-');

        int minX = int.Parse(elements[0]) - range;
        int maxX = int.Parse(elements[0]) + range;
        int minZ = int.Parse(elements[1]) - range;
        int maxZ = int.Parse(elements[1]) + range;

        foreach (GameObject tile in GeneratedTiles)
        {
            elements = tile.name.Split('-');
            int x = int.Parse(elements[0]);
            int z = int.Parse(elements[1]);

            if (x >= minX && x <= maxX)
            {
                if (z >= minZ && z <= maxZ)
                {
                    if (origin != tile)
                    {
                        selectedTiles.Add(tile);
                    }
                }
            }
        }

        return selectedTiles;
    }


    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float sampleX = x * scale;
                float sampleY = y * scale;
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }

}
