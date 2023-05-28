using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public float cellsize = 1f;
    public Camera raycastCamera;
    public Dictionary<Vector2Int, Building> builldingDictionary = new Dictionary<Vector2Int, Building>();

    private Plane _plane;

    public Building currentBuilding;
    private void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
        builldingDictionary.Clear();
    }
    private void Update()
    {
        if(currentBuilding == null)
        {
            return;
        }
        Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 getPoint = ray.GetPoint(distance);
        int x = Mathf.RoundToInt(getPoint.x);
        int z = Mathf.RoundToInt(getPoint.z);
        

        currentBuilding.transform.position = new Vector3(x,0,z) * cellsize;

        if (Input.GetMouseButtonDown(0))
        {
            //builldingDictionary.Add(new Vector2Int(x,z), currentBuilding);
            instalBuilding(x, z, currentBuilding);
            currentBuilding = null;
        }
    }
    public void CreateBuillding(GameObject builldingPrefab)
    {
        GameObject newBuillding = Instantiate(builldingPrefab);
        currentBuilding = newBuillding.GetComponent<Building>();
    }
    void instalBuilding(int xPosition, int zPosition, Building building)
    {
        for(int x = 0; x < building.xSize; x++)
        {
            for(int z = 0; z < building.zSize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                builldingDictionary.Add(coordinate, currentBuilding);
            }
        }
    }
}
