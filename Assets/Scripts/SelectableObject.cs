using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectableObject : MonoBehaviour
{
    public GameObject selectionIndicator;
    private void Start()
    {
        Unselect();
    }
    public virtual void OnHover(float modifier)
    {
        transform.localScale = Vector3.one * modifier; 
    }
    public virtual void UnHover(float modifier)
    {
        transform.localScale = Vector3.one * modifier; 
    }
    public virtual void Select()
    {
        selectionIndicator.SetActive(true);
    }
    public virtual void Unselect()
    {
        selectionIndicator.SetActive(false);
    }
    public virtual void SetDestination(Vector3 pointOfDestination)
    {

    }
}
