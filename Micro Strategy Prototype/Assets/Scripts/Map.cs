using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public List<Building> buildings = new List<Building>();
    public List<Building> buildingPrefabs = new List<Building>();

    public float tileSize;

    public static Map instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EnableUsableTiles();
    }

    public void EnableUsableTiles()
    {
        foreach(Building building in buildings)
        {
            Tile[] tilesToEnable = {GetTileAtPosition(building.transform.position + new Vector3(0, tileSize, 0)),
                            GetTileAtPosition(building.transform.position + new Vector3(0, -tileSize, 0)),
                            GetTileAtPosition(building.transform.position + new Vector3(tileSize, 0, 0)),
                            GetTileAtPosition(building.transform.position + new Vector3(-tileSize, 0, 0))};

            foreach (Tile tile in tilesToEnable)
            {
                tile?.ToggleHighlight(true);
            }
        }
    }

    public void DisableUsableTiles()
    {
        foreach(Tile tile in tiles)
        {
            tile.ToggleHighlight(false);
        }
    }

    public void CreateNewBuilding(BuildingType buildingType, Vector3 position)
    {
        Building prefabToSpawn = buildingPrefabs.Find(x => x.type == buildingType);
        GameObject buildingObj = Instantiate(prefabToSpawn.gameObject, position, Quaternion.identity);
        buildings.Add(buildingObj.GetComponent<Building>());

        GetTileAtPosition(position).hasBuilding = true;

        DisableUsableTiles();

        GameManager.instance.OnCreatedNewBuilding(prefabToSpawn);
    }

    Tile GetTileAtPosition (Vector3 pos)
    {
        return tiles.Find(x => x.CanBeHighlighted(pos));
    }
}
