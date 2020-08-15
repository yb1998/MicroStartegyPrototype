using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int curTurn;
    public bool placingBuilding;
    public BuildingType curSelectedBuilding;

    [Header("Current Resources")]
    public int curFood;
    public int curMetal;
    public int curOxygen;
    public int curEnergy;

    [Header("Round Resource Increas ")]
    public int foodPerTurn;
    public int metalPerTurn;
    public int oxygenPerTurn;
    public int energyPerTurn;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //updating resource UI
        UI.instance.UpdateResourceText();
    }

    public void EndTurn()
    {
        //give resources
        curFood += foodPerTurn;
        curMetal += metalPerTurn;
        curOxygen += oxygenPerTurn;
        curEnergy += energyPerTurn;

        //update the resource UI
        UI.instance.UpdateResourceText();

        curTurn++;

        //enable the building buttons
        UI.instance.ToggleBuildingButtons(true);

        //enable usable tiles
        Map.instance.EnableUsableTiles();
    }

    public void SetPlacingBuilding(BuildingType buildingType)
    {
        placingBuilding = true;
        curSelectedBuilding = buildingType;    
    }

    public void OnCreatedNewBuilding (Building building)
    {
        //adds produced goods to resource flow
        if(building.doesProduceResource)
        {
            switch(building.productionResource)
            {
                case ResourceType.Food:
                    foodPerTurn += building.productionQuantity;
                    break;

                case ResourceType.Metal:
                    metalPerTurn += building.productionQuantity;
                    break;

                case ResourceType.Oxygen:
                    oxygenPerTurn += building.productionQuantity;
                    break;

                case ResourceType.Energy:
                    energyPerTurn += building.productionQuantity;
                    break;
            }
        }

        //adds maintenance Costs to resource flow
        if (building.hasMaintenanceCost)
        {
            switch(building.MaintenanceResource)
            {
                case ResourceType.Food:
                    foodPerTurn -= building.maintenanceQuantity;
                    break;

                case ResourceType.Metal:
                    metalPerTurn -= building.maintenanceQuantity;
                    break;

                case ResourceType.Oxygen:
                    oxygenPerTurn -= building.maintenanceQuantity;
                    break;

                case ResourceType.Energy:
                    energyPerTurn -= building.maintenanceQuantity;
                    break;
            }
        }

        placingBuilding = false;
        
        //update Resource UI
        UI.instance.UpdateResourceText();
    }
}
