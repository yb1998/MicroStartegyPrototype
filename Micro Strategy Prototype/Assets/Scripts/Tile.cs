using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject highlight;
    public bool hasBuilding;

    public void ToggleHighlight(bool toggle)
    {
        highlight.SetActive(toggle);
    }

    //can this tile be highlighted based on a given position
    public bool CanBeHighlighted (Vector3 potentialPosition)
    {
        return transform.position == potentialPosition && !hasBuilding;
    }

    void OnMouseDown()
    {
        if(GameManager.instance.placingBuilding && !hasBuilding)
            Map.instance.CreateNewBuilding(GameManager.instance.curSelectedBuilding, transform.position);
    } 
}
