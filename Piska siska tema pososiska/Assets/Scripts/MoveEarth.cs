using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEarth : MonoBehaviour
{
    public float speed = 2f;
    public GameObject[] planets;

    private GameObject planet;
    private static int oldNumberPlanet;
    void Start()
    {
        planet = this.gameObject;
        if(!planet.transform.name.Contains("Clone"))
            spawn();
    }
    
    void Update()
    {
        planet.transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
        if (planet.transform.position.z < -150f)
        {
            spawn();
        }
    }

    private void spawn()
    {
        int variant = Random.Range(0, 2);
        float x = -40f;
        if (variant == 0)
            x = Random.Range(-50f, -35f);
        else if (variant == 1)
            x = Random.Range(30f, 45f);

        int numberPlanet = Random.Range(0, planets.Length);
        while (numberPlanet == oldNumberPlanet)
            numberPlanet = Random.Range(0, planets.Length);

        GameObject obj = Instantiate(planets[Random.Range(0, planets.Length)], new Vector3(x, Random.Range(-100f, -30f), 300f), Quaternion.identity);

        oldNumberPlanet = numberPlanet;
        Destroy(planet);
    }
}
