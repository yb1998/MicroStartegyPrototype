﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Base,
    Greenhouse,
    Mine,
    SolarPanel
}

public enum ResourceType
{
    Food,
    Metal,
    Oxygen,
    Energy
}

public class Building : MonoBehaviour
{
    public BuildingType type;

    [Header("Production")]
    public bool doesProduceResource;
    public ResourceType productionResource;
    public int productionQuantity;

    [Header("Maintenance")]
    public bool hasMaintenanceCost;
    public ResourceType MaintenanceResource;
    public int maintenanceQuantity;


}
