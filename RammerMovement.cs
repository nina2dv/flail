using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammerMovement : MonoBehaviour
{
    private GameObject target;
    float distance;
    public float stoplength;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        distance = Vector3.Distance(gameObject.transform.position, target.transform.position) - stoplength;

        speed = distance;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            Destroy(gameObject);
        }

    }
}
