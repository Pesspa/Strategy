using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public enum UnitState
    {
        idle,
        walkToEnemy,
        attack
    }
    public float damage;
    public Enemy targetEnemy;
    public UnitState currentUnitState;
    public float distanceToFollow;
    public float distanceToAttack;

    public AudioSource soundOfDeath;
    public NavMeshAgent bot;
    public int unitPrice;
    public float health = 5;
    public float maxHealth = 5;

    public GameObject healthBarPrefab;
    public Healthbar healthBar;
    private float _attackPerriod = 1f;
    private float _timer;
    private void Start()
    {
        maxHealth = health;
//        GameObject healthBar = Instantiate(healthBarPrefab);
//        this.healthBar = healthBar.GetComponent<Healthbar>();
//        this.healthBar.Setup(transform);
        bot = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(currentUnitState == UnitState.idle)
        {
            SearchOfNearestEnemy();
            if (targetEnemy)
            {
                if (Vector3.Distance(transform.position, targetEnemy.transform.position) < distanceToFollow)
                {
                    currentUnitState = UnitState.walkToEnemy;
                }
            }
        } 
        else if(currentUnitState == UnitState.walkToEnemy)
        {
            SearchOfNearestEnemy();
            if (targetEnemy)
            {
                bot.SetDestination(targetEnemy.transform.position);
                if (Vector3.Distance(transform.position, targetEnemy.transform.position) < distanceToAttack)
                {
                    currentUnitState = UnitState.attack;
                }
            }
        } 
        else if(currentUnitState == UnitState.attack)
        {
            if(targetEnemy != null)
            {
                _timer += Time.deltaTime;
                if (_timer >= _attackPerriod)
                {
                    targetEnemy.TakeDamage(damage);
                    _timer = 0;
                }
            }
            else
            {
                currentUnitState = UnitState.idle;
            }
        }
    }
    public void SearchOfNearestEnemy()
    {
        float currentDistance;
        Enemy nearestEnemy = null;
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        float minDistance = Mathf.Infinity;
        for(int i = 0; i < allEnemies.Length; i++)
        {
            if (Vector3.Distance(transform.position,allEnemies[i].transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, allEnemies[i].transform.position);
                nearestEnemy = allEnemies[i];
            }
            targetEnemy = nearestEnemy;
        }
    }
    public override void SetDestination(Vector3 point)
    {
        if(bot != null)
        {
            base.SetDestination(point);
            bot.SetDestination(point);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health, maxHealth);
        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        soundOfDeath.Play();
        Destroy(gameObject);
    }
}
