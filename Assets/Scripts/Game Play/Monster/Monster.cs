using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MiniWave miniWave;
    [SerializeField] private MonsterData _monsterData;
    private Rigidbody2D rb;
    [SerializeField] private float curHP;

    public HealthBar healthBar;
    public Vector2 target;
    public int pathIndex = 0;
    public int IDInWave;
    private int spiritStoneAmount;
    private int damage;
    private float notTakeDamageTime = 0f;
    private float timeToHideHealthBar = 2f;

    public void InitMonster(MonsterData data)
    {
        _monsterData = data;
        curHP = data.maxHP;
        spiritStoneAmount = data.spiritStoneAmount;
        damage = data.damage;
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHP(data.maxHP);
    }
    private void Start()
    {
        target = miniWave.pathway.wayPoints[0];
    }

    private void Update()
    {
        HireHealthBar();
        MoveToNextIndex();
        if (pathIndex == miniWave.pathway.wayPoints.Count)
        {
            this.PostEvent(EventID.On_Monster_Escaped, damage);
            miniWave.listMonsters.Remove(this);
            miniWave.CheckIfAllEnermyDead();
            Destroy(gameObject);
        }
    }

    private void HireHealthBar()
    {
        if (notTakeDamageTime < timeToHideHealthBar) return;
        notTakeDamageTime += Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        rb.velocity = direction * _monsterData.speed;
    }

    private void MoveToNextIndex()
    {
        if(Vector2.Distance(target, transform.position) > 0.1) return;
        pathIndex++;
       
    }

    public void TakeDamage(float amount)
    {
        healthBar.gameObject.SetActive(true);
        notTakeDamageTime = 0f;
        curHP -= amount;
        if (curHP <= 0)
        {
            OnMonsterDie();
        }
        healthBar.SetHP(curHP);
    }

    private void OnMonsterDie()
    {
        miniWave.listMonsters.Remove(this);
        miniWave.CheckIfAllEnermyDead();
        this.PostEvent(EventID.On_Monster_Killed,spiritStoneAmount);
        Destroy(gameObject);
    }
}
