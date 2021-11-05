using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject[] Meteors;
    public float TimeSpawn = 2f;

    private bool localGame;
    private GameObject enem_obj;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Local"))
            localGame = true;
        else
            localGame = false;
        
        InvokeRepeating(nameof(spawn), 0.5f, TimeSpawn);
    }

    private void spawn()
    {
        float x = transform.position.x + Random.Range(-5f, 5f), y = transform.position.y, z = transform.position.z;
        if (localGame)
            enem_obj = Instantiate(Enemy, new Vector3(x, y, z), Quaternion.identity);
        else
            enem_obj = PhotonNetwork.Instantiate(Enemy.name, new Vector3(x, y, z), Quaternion.identity);
    }
}
