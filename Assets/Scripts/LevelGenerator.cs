using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class LevelGenerator : MonoBehaviour {
    public int GridSize;
    public List<GameObject> Blocks;
    public List<GameObject> GeneratedBlocks;
    public float Scale;
    private float[,] noiseMap;
    public NavMeshSurface surface;
    public GameObject level;
    public List<GameObject> Trees;
    public List<GameObject> GeneratedTrees;


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
                GenerateBlock(x, y);

            }
        }
        surface.BuildNavMesh();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void GenerateBlock(int x, int y)
    {
        GameObject block = null;
        //Retrieve the noise value for this specific key, key combination    
        float noise = noiseMap[x, y];
        float temp = 0;
        //Converts the noise into workable values for blockbased terrain generation
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
            //Spawns trees on this specific level only
            if (Random.Range(0, 100) >= 90)
            {
                GameObject tree = Instantiate(Trees[Random.Range(0, 3)]);
                tree.transform.position = new Vector3(x * 1, temp + 0.45f, y * 1);
                tree.transform.Rotate(0, Random.Range(0, 360), 0);
                tree.transform.parent = level.transform;
                GeneratedTrees.Add(tree);
            }
        }
        else if (noise > 0.6 && noise <= 1)
        {
            temp = 3;
        }

        block = Instantiate(Blocks[(int)temp]);
        block.transform.parent = level.transform;
        block.transform.position = new Vector3(x * 1, temp, y * 1);
        block.name = y + "-" + x + "-" + temp;
        GeneratedBlocks.Add(block);

    }

    //Generates a noise map used for terrain height generation
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
