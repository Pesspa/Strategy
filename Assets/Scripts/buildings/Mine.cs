using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    public int stonk;
    public Resource resource;
    private float _timer = 0;
    private float _timeToPay = 3f;
    private void Start()
    {
        resource = FindObjectOfType<Resource>();
    }
    void Update()
    {
        healthbar.SetHealth(health, maxHealth);
        if(health <= 0)
        {
            Die(gameObject);
        }

        _timer += Time.deltaTime;
        if(_timer >= _timeToPay)
        {
            AddMoney();
            _timer = 0;
        }
    }
    public void AddMoney()
    {
        resource.gold += stonk;
    }
}
