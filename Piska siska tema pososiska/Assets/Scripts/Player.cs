using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 clickPos;
    private Camera mainCam;
    public float speed = 0.1f;
    private float maxSpeed = 0.5f;
    private float horizontalDir = 0f;
    private int playerLevel = 0;
    private float verticalDir = 0f;
    public float health = 5f;
    public GameObject bulletPrefab;

    public Transform cannon;

    public static float bulletDamage;
    
    private float direction = 1f;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        InvokeRepeating(nameof(Fire), 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        gameObject.transform.position += new Vector3(xMovement,0, yMovement);

        //Rotation calculation
        float rotoation = xMovement * 90;
        
        //Rotation apply
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotoation));
        
        Vector3 playerPos = gameObject.transform.position;
        
        //Borders
        
        //horizontal
        if (gameObject.transform.position.x > 23)
        {
            gameObject.transform.position = new Vector3(23, 0, playerPos.z);
        }
        else if (gameObject.transform.position.x < -23)
        {
            gameObject.transform.position = new Vector3(-23, 0, playerPos.z);
        }
        //vertical
        if (gameObject.transform.position.z > 50)
        {
            gameObject.transform.position = new Vector3(playerPos.x, 0, 50);
        }
        else if (gameObject.transform.position.z < -50)
        {
            gameObject.transform.position = new Vector3(playerPos.x, 0, -50);
        }
        
        
    }

    void Fire()
    {
        var bullet = Instantiate(bulletPrefab, cannon.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = direction;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        print("Hit");
    }
}
