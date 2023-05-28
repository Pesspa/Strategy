using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    idle,
    walkToUnit,
    walkToBuillding,
    attack
}
public class Enemy : MonoBehaviour
{
    private float _reloadAttack = 1f;
    private float _timer = 0f;

    public Healthbar healthbar;
    public float maxhealth;
    public float health;
    public Building targetBuillding;
    public Unit targetUnit;
    public EnemyState CurrentEnemystate;
    public float distanceToFollow;
    public float distanceToAttack;
    public NavMeshAgent enemyBot;
    void Start()
    {
        //        if (!gameObject.GetComponent<NavMeshAgent>()) 
        //        {
        //            gameObject.AddComponent<NavMeshAgent>();
        //        } 
        //        SetState(CurrentEnemystate = EnemyState.idle);
        SearchOfNearestBuillding();
        CurrentEnemystate = EnemyState.walkToBuillding;
    }
    void Update()
    {
        if(targetUnit != null)
        {
            SetState(EnemyState.walkToUnit);
        }
        if(CurrentEnemystate == EnemyState.idle)
        {
            SearchOfNearestUnit();
        }
        else if(CurrentEnemystate == EnemyState.walkToUnit)
        {
            SearchOfNearestUnit();
            if(targetUnit != null)
            {
                enemyBot.SetDestination(targetUnit.transform.position);
                if (Vector3.Distance(targetUnit.transform.position, transform.position) > distanceToFollow)
                {
                    SetState(CurrentEnemystate = EnemyState.walkToBuillding);
                }
                if (Vector3.Distance(targetUnit.transform.position, transform.position) < distanceToAttack)
                {
                    SetState(CurrentEnemystate = EnemyState.attack);
                }
            }
        }
        else if(CurrentEnemystate == EnemyState.walkToBuillding)
        {
            if(targetBuillding != null)
            {
                if (Vector3.Distance(transform.position, targetBuillding.transform.position) > distanceToAttack)
                {
                    SetState(CurrentEnemystate = EnemyState.walkToBuillding);
                }
            }
//            SetState(CurrentEnemystate = EnemyState.walkToBuillding);
//            enemyBot.SetDestination(targetUnit.transform.position);
            if (targetBuillding != null)
            {
                if (Vector3.Distance(transform.position, targetBuillding.transform.position) < distanceToAttack)
                {
                    SetState(CurrentEnemystate = EnemyState.attack);
                }
            }
        }
        else if(CurrentEnemystate == EnemyState.attack)
        {

        }
        if(CurrentEnemystate == EnemyState.attack)
        {
            if (targetUnit != null || targetBuillding != null)
            {
                if(targetUnit != null)
                {
                    if (Vector3.Distance(targetUnit.transform.position, transform.position) > distanceToAttack)
                    {
                        SetState(CurrentEnemystate = EnemyState.walkToUnit);
                    }
                    _timer += Time.deltaTime;
                    if (_timer >= _reloadAttack)
                    {
                        targetUnit.TakeDamage(1);
                        _timer = 0;
                    }
                }
                if(targetBuillding != null)
                {
                    _timer += Time.deltaTime;
                    if(_timer >= _reloadAttack)
                    {
                        targetBuillding.TakeDamage(1);
                        _timer = 0;
                    }
                }

            }
            else
            {
                SetState(CurrentEnemystate = EnemyState.walkToBuillding);
            }
        }
    }
    public void SetState(EnemyState enemyState)
    {
        CurrentEnemystate = enemyState;
        if (CurrentEnemystate == EnemyState.idle)
        {

        }
        else if (CurrentEnemystate == EnemyState.walkToUnit)
        {
            SearchOfNearestUnit();
            enemyBot.SetDestination(targetUnit.transform.position);
        }
        else if (CurrentEnemystate == EnemyState.walkToBuillding)
        {
            SearchOfNearestBuillding();
            enemyBot.SetDestination(targetBuillding.transform.position);
            SearchOfNearestUnit();
        }
        else if (CurrentEnemystate == EnemyState.attack)
        {
            //_timer = 0;
        }
    }
    public void SearchOfNearestBuillding()
    {
        Building nearestBuillding = null;
        Building[] allBuilldings = FindObjectsOfType<Building>();
        float minDistance = Mathf.Infinity;
        for(int i = 0; i < allBuilldings.Length; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, allBuilldings[i].transform.position);
            if(currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearestBuillding = allBuilldings[i];
            }
        }
        targetBuillding = nearestBuillding;
    }
    public void SearchOfNearestUnit()
    {
        Unit nearestUnit = null;
        float minDistance = Mathf.Infinity;
        Unit[] allUnits = FindObjectsOfType<Unit>();
        for(int i =0; i < allUnits.Length; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, allUnits[i].transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearestUnit = allUnits[i];
            }
        }
        if (minDistance < distanceToFollow)
        {
            targetUnit = nearestUnit;
        }
    }
    public void TakeDamage(float damage)
    {
        healthbar.SetHealth(health, maxhealth);
        health -= damage;
        if(health < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
