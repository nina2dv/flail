using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private GameObject target;
    float distance;
    public float speed;

    private float timeBtwShots;
    public float startTimeBtwShots;




    private Transform player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player");
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
    

        distance = Vector3.Distance(gameObject.transform.position, target.transform.position) - 6.5f;

        speed = distance;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
     

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Destroy(gameObject);
        }

    }

}