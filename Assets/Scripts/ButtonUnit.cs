using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ButtonUnit : MonoBehaviour
{
    public int price;
    public GameObject prefabUnit;
    public Transform spawnUnit;
    public Resource resource;
    public void CreateUnit()
    {
        price = prefabUnit.GetComponent<Unit>().unitPrice;
        if (resource.gold >= price)
        {
            resource.gold -= price; 
            Vector3 position = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            GameObject newUnit = Instantiate(prefabUnit, spawnUnit.position + position, spawnUnit.rotation);
            NavMeshAgent botKnight = newUnit.GetComponent<NavMeshAgent>();
        }     
    }
}
