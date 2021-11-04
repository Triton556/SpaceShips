using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject[] LittleEnemy;
    public GameObject[] MediumEnemy;
    public GameObject[] BigEnemy;
    public GameObject Bullet;
    public GameObject Gun;
    
    private float speed;
    private float damage;
    private float health;
    
    private Random rnd;
    private int chanceSpawnEnemy;
    private TypeEnemy randomTypeEnemy;
    private void Start()
    {
        spawn();
    }

    private void spawn()
    {
        chanceSpawnEnemy = Random.Range(0, 100);

        if (chanceSpawnEnemy > 85)
        {
            initializeParametrsEnemy(0.5f, 3f, 4f);
            spawnModelShip(BigEnemy, 2);
            randomTypeEnemy = TypeEnemy.BIG;
        }
        else if (chanceSpawnEnemy > 55 && chanceSpawnEnemy <= 85)
        {
            initializeParametrsEnemy(0.7f, 1.5f, 2f);
            spawnModelShip(MediumEnemy, 3);
            randomTypeEnemy = TypeEnemy.MEDIUM;
        }
        else if(chanceSpawnEnemy <= 55)
        {
            initializeParametrsEnemy(1f, 0.5f, 1f);
            spawnModelShip(LittleEnemy, 3);
            randomTypeEnemy = TypeEnemy.LITTLE;
        }
        
        Debug.Log(randomTypeEnemy);
    }
    private void initializeParametrsEnemy(float _speed, float _damage, float _health)
    {
        speed = _speed;
        damage = _damage;
        health = _health;
    }

    private void spawnModelShip(GameObject[] ship, int countType)
    {
        GameObject model = ship[Random.Range(0, countType)];
        model = Instantiate(model, this.transform);
        model.transform.position = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        //move();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }

    private void move()
    {
        transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
    }

    private void fire(float dir=-1f)
    {
        GameObject b = Instantiate(Bullet, Gun.transform.position, Quaternion.identity);
        //b.GetComponent<Bullet>().direction = dir;
    }

    private void getDamage(float _damage)
    {
        health -= damage;
        if (health <= 0)
            destroyShip();
    }

    private void destroyShip()
    {
        Destroy(gameObject);
    }
}

enum TypeEnemy
{
    LITTLE,
    MEDIUM,
    BIG
};
