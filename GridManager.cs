using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Transform[] Plants;
    public MeshRenderer FloorMesh;
    public int Width;
    public int Height;
    public float Footprint;
    public float HeightOffset;
    public int PlantCount;

    public Vector3 InitPoint;
    public List<Vector3> GridPoints;
    public MeadowManager meadow;

    void Awake()
    {
        GridPoints = BuildGrid();
        InitPlant(PlantCount);
        meadow = GameObject.Find("MeadowManager").GetComponent<MeadowManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Vector3> BuildGrid()
    {
        List<Vector3> grid = new List<Vector3>();

        for (int w = 0; w < Width; w++)
        {
            for (int h = 0; h < Height; h++)
            {
                grid.Add(InitPoint + new Vector3(w * Footprint, HeightOffset, h * Footprint));
            }
        }

        return grid;
    }

    private void SeedRandomPlant()
    {
        int n = Random.Range(0, GridPoints.Count);
        Vector3 pos = GridPoints[n];
        Transform plant = Plants[Random.Range(0, Plants.Length)];
        Instantiate(plant, pos, Quaternion.identity);
        GridPoints.RemoveAt(n);
    }

    public void SeedTargettedPlant(Vector3 targetted)
    {
        int i = 0;
        int ClosestIndex = 0;
        float CurrentDistance = 10f;

        foreach (Vector3 GridPoint in GridPoints)
        {
            float current = Vector3.Distance(GridPoint, targetted);
            if (current < CurrentDistance)
            {
                CurrentDistance = current;
                ClosestIndex = i;
            }
            i++;
        }

        Transform plant = Plants[Random.Range(0, Plants.Length)];
        Transform newPlant = Instantiate(plant, GridPoints[ClosestIndex], Quaternion.identity);
        meadow.LivingPlants.Add(newPlant.gameObject);
        GridPoints.RemoveAt(ClosestIndex);
    }

    public void NewPoint(Vector3 point)
    {
        GridPoints.Add(point);
    }

    private void InitPlant(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SeedRandomPlant();
        }
    }

}
