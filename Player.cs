using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float moveSpeed;
    private float tempSpeed;

    public Rigidbody2D rb2d;

    public GameObject pulley;

    private Vector2 moveInput;
    public float dashSpeed;
    public float dashTime;
    public float dashPower;
    bool isDashing = false;


    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject retryGUI;

    // Start is called before the first frame update
    void Start()
    {
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
            }
            // Dashing();
        }
        // dashCooldown -= Time.deltaTime;

        UpdatePulley();
        // LookAtMouse();


    }
    void UpdateDirection()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle * -90, Vector3.forward);
        /* Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
        */
    }

    private IEnumerator Dash()
    {
  
   
            isDashing = true;
            tempSpeed = moveSpeed;
            moveSpeed *= dashPower;
            
            yield return new WaitForSeconds(dashTime);

            moveSpeed = tempSpeed;
            isDashing = false;
     
    }

    void UpdatePulley()
    {
        if (Input.GetKeyDown("1"))
        {
            pulley.GetComponent<Crank>().Rotate(1);
        }
        if (Input.GetKeyDown("2"))
        {
            pulley.GetComponent<Crank>().Rotate(-1);
        }
    }
    /* private void Dashing()
    {
        if (dashCooldown <= 0)
        {
            dash = true;
        }
        else
        {
            // dashCooldown--;
        }
        //temp = rb2d.velocity;

        rb2d.velocity = Vector2.zero;

        if (dash && Input.GetKey(KeyCode.Space))
        {
            Vector2 mouseDirection = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2)).normalized;
            rb2d.AddForce(mouseDirection * dashSpeed * Time.fixedDeltaTime);
            dash = false;
            dashCooldown = 2;

            //rb2d.velocity = temp;
        }
    }
    */




    void LookAtMouse()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy")||col.gameObject.CompareTag("Projectile"))
        {
            Time.timeScale = 0;
            retryGUI.SetActive(true);
        }
    }
}