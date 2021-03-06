using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    public AudioClip piu;
    public AudioClip dead;
    public GameObject gameOverScreen;
    public ParticleSystem blow;
    private PhotonView _photonView;
    private bool localGame;

    private Vector3 clickPos;
    private Camera mainCam;
    public float speed = 0.1f;
    private float maxSpeed = 0.5f;
    private float horizontalDir = 0f;
    private int playerLevel = 0;
    private float verticalDir = 0f;
    public int health = 5;
    public GameObject bulletPrefab;
    public GameObject healthGO;
    public Transform cannon;

    public static float bulletDamage = 10f;

    private float direction = 1f;

    private bool immortality = false;

    public GameObject[] playerShips;


    public Button levelUpButton;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Local"))
            localGame = true;
        else
            localGame = false;


        _photonView = GetComponent<PhotonView>();
        mainCam = Camera.main;
        InvokeRepeating(nameof(Fire), 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_photonView != null)
            if (!_photonView.IsMine && !localGame)
                return;

        //Movement Input
        if (Input.GetMouseButton(0))
        {
            clickPos = mainCam.ScreenToViewportPoint(Input.mousePosition);
            if (clickPos.x > 0.6f)
            {
                horizontalDir = clickPos.x - 0.5f;
            }
            else if (clickPos.x < 0.6f)
            {
                horizontalDir = clickPos.x - 0.5f;
            }
            else
            {
                verticalDir = 0f;
            }

            if (clickPos.y > 0.6f)
            {
                verticalDir = clickPos.y - 0.5f;
            }
            else if (clickPos.y < 0.6f)
            {
                verticalDir = clickPos.y - 0.5f;
            }
            else
            {
                verticalDir = 0f;
            }
        }
        else
        {
            clickPos = new Vector3(0.5f, 0.5f, 0);
            horizontalDir = 0f;
            verticalDir = 0f;
        }

        //Movement calculation
        float xMovement = horizontalDir * speed;
        float yMovement = verticalDir * speed * 2;
        //print(new Vector2(xMovement, yMovement));

        //Movement apply
        gameObject.transform.position += new Vector3(xMovement, 0, yMovement);

        //Rotation calculation
        float rotoation = xMovement * 90;

        //Rotation apply
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotoation));

        Vector3 playerPos = gameObject.transform.position;

        //Borders

        //horizontal
        if (gameObject.transform.position.x > 22)
        {
            gameObject.transform.position = new Vector3(22, 0, playerPos.z);
        }
        else if (gameObject.transform.position.x < -22)
        {
            gameObject.transform.position = new Vector3(-22, 0, playerPos.z);
        }

        //vertical
        if (gameObject.transform.position.z > 49)
        {
            gameObject.transform.position = new Vector3(playerPos.x, 0, 49);
        }
        else if (gameObject.transform.position.z < -49)
        {
            gameObject.transform.position = new Vector3(playerPos.x, 0, -49);
        }

        if (health <= 0)
        {
            Dead();
        }
    }

    void Fire()
    {
        GameObject bullet = this.gameObject;
        
        if (localGame)
            bullet = Instantiate(bulletPrefab, cannon.transform.position, Quaternion.identity);
        else
            bullet = PhotonNetwork.Instantiate(bulletPrefab.name, cannon.transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().direction = direction;
        
        GetComponent<AudioSource>().PlayOneShot(piu);
    }

    public void GetDamage(float damage)
    {
        if (!immortality)
        {
            StartCoroutine(ImmortalityCorutine());
            if(localGame)
                healthGO.transform.GetChild((health - 1)).gameObject.SetActive(false);
            health -= 1;
            print("Hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetDamage(1f);
            other.GetComponent<Enemy>().getDamage(1f);
        }
    }

    IEnumerator ImmortalityCorutine()
    {
        immortality = true;
        yield return new WaitForSeconds(1f);
        immortality = false;
    }


    public void UpgradeShip()
    {
        if (playerLevel <= 3)
        {
            health = 5;

            for (int i = 0; i < healthGO.transform.childCount; i++)
            {
                healthGO.transform.GetChild(i).gameObject.SetActive(true);
            }
            
            GameController.score = 0;
            print("Upgrade");
            playerLevel += 1;

            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            Instantiate(playerShips[playerLevel], transform.position, Quaternion.identity, transform);

            levelUpButton.interactable = false;
            if (playerLevel >= 3)
            {
                levelUpButton.gameObject.SetActive(false);
            }
            InvokeRepeating(nameof(Fire), 1f, 1f);
        }
        
    }

    void Dead()
    {
        //Instantiate(blow, transform.position, Quaternion.identity);
        //gameOverScreen.SetActive(true);
        if (localGame)
        {
            Instantiate(blow, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameOverScreen.GetComponent<AudioSource>().PlayOneShot(dead);
            gameOverScreen.SetActive(true);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect(); 
            SceneManager.LoadScene(0);
        }
        
    }
}