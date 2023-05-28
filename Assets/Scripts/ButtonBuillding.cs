using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuillding : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public GameObject SelectableBullding;
    public int price;
    public void TryToBuy()
    {
        price = SelectableBullding.GetComponent<Building>().price;
        if(FindObjectOfType<Resource>().gold >= price)
        {
            FindObjectOfType<Resource>().gold -= price;
            BuildingPlacer.CreateBuillding(SelectableBullding);
        }
    }
}