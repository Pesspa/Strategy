using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableObject
{
    public AudioSource audio;
    public int health;
    public int maxHealth;
    public int price;
    public int xSize = 3;
    public int zSize = 3;
    public Renderer renderer;
    public Color startColor;
    public Healthbar healthbar;
    private void Awake()
    {
        maxHealth = health;
        healthbar.SetHealth(health, maxHealth);
        startColor = renderer.material.color;
    }
    private void Update()
    {
        healthbar.SetHealth(health, maxHealth);
    }
    private void OnDrawGizmos()
    {
        float cellSize = FindObjectOfType<BuildingPlacer>().cellsize;

        for(int i = 0; i < zSize; i++)
        {
            for(int j = 0; j < xSize; j++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(i, 0, j), new Vector3(1f, 0f, 1f) * cellSize);
            }
        }
    }
    public void DisplayAcceptableposition()
    {
        renderer.material.color = startColor;
    }
    public void DisplayUnacceptableposition()
    {
        renderer.material.color = Color.red;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health, damage);
        if(health <= 0)
        {
            audio.Play();
            Invoke(nameof(Die), 1f);
        }
    }
    public void Die(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
