using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject[] LittleEnemy;
    public GameObject[] MediumEnemy;
    public GameObject[] BigEnemy;
    public GameObject Bullet;
    public GameObject Gun;
    
    private float speed;
    private float health;
    
    private Random rnd;
    private int chanceSpawnEnemy;
    private TypeEnemy randomTypeEnemy;
    private PhotonView photonView;

    private bool localGame;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Local"))
            localGame = true;
        else
            localGame = false;

        photonView = GetComponent<PhotonView>();
        
        spawn();
    }

    private void spawn()
    {
        chanceSpawnEnemy = Random.Range(0, 100);

        if (chanceSpawnEnemy > 85)
        {
            initializeParametrsEnemy(10f, 3f);
            spawnModelShip(BigEnemy, 2);
            randomTypeEnemy = TypeEnemy.BIG;
        }
        else if (chanceSpawnEnemy > 55 && chanceSpawnEnemy <= 85)
        {
            initializeParametrsEnemy(15f, 1.5f);
            spawnModelShip(MediumEnemy, 3);
            randomTypeEnemy = TypeEnemy.MEDIUM;
        }
        else if(chanceSpawnEnemy <= 55)
        {
            initializeParametrsEnemy(25f, 0.5f);
            spawnModelShip(LittleEnemy, 3);
            randomTypeEnemy = TypeEnemy.LITTLE;
        }
    }
    private void initializeParametrsEnemy(float _speed, float _health)
    {
        speed = _speed;
        health = _health;
    }

    private void spawnModelShip(GameObject[] ship, int countType)
    {
        GameObject model = ship[Random.Range(0, countType)];
        model = Instantiate(model, this.transform);
        model.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        
        InvokeRepeating(nameof(fire), 0.1f, 3f);
        //model.transform.position = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        move();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
    }

    private void move()
    {
        transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
    }

    private void fire()
    {
        GameObject b = Instantiate(Bullet, Gun.transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().direction = -1f;
    }

    public void getDamage(float _damage)
    {
        health -= _damage;
        print("Got HIT");
        if (health <= 0)
            destroyShip();
    }

    private void destroyShip()
    {
        GameController.score += 1;
        if(localGame)
            Destroy(gameObject);
        else if (photonView != null)
        {
            if (photonView.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}

enum TypeEnemy
{
    LITTLE,
    MEDIUM,
    BIG
};
