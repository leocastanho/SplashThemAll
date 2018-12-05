using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlob : EnemyController {

    private bool movingRight = true;
    public Transform groundDetection;
    public float distance;
    public float distance2;
    public LayerMask whatIsGround;


    // Use this for initialization
    void Start () {
        health = 20;
        maxHealth = health;
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
        speed = Random.Range(2f, 3f);
    }
	
	// Update is called once per frame
	void Update () {


        this.transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundDetector = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, whatIsGround);
        Debug.DrawRay(groundDetection.position, Vector2.down * distance, Color.red);

        if (groundDetector.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;

            }
        }

        if (movingRight)
        {
            RaycastHit2D groundDetector2 = Physics2D.Raycast(groundDetection.position, Vector2.right, distance2, whatIsGround);
            Debug.DrawRay(groundDetection.position, Vector2.right * distance2, Color.red);
            if (groundDetector2.collider == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
        }
        else
        {
            RaycastHit2D groundDetector3 = Physics2D.Raycast(groundDetection.position, Vector2.left, distance2, whatIsGround);
            Debug.DrawRay(groundDetection.position, Vector2.left * distance2, Color.red);
            if (groundDetector3.collider == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        //Another way to make the raycast in both directions
        /*RaycastHit2D groundDetector2 = Physics2D.Raycast(new Vector2 (groundDetection.position.x -0.5f, groundDetection.position.y), Vector2.right, distance, whatIsGround);
        Debug.DrawRay(new Vector2(groundDetection.position.x - 0.5f, groundDetection.position.y), Vector2.right * distance, Color.red);*/

        

        if (health <= 0)
        {
            Destroy(this.gameObject, 0);
            AudioManager.instance.enemyDieFX();
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), Quaternion.identity);
            TimeManager.instance.StartCoroutine(TimeManager.instance.CallSlowMotionEnemies());
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        AudioManager.instance.hittingFX();
        //code to get smaller on hit
        Vector3 scale = transform.localScale;
        scale.x -= (scalex / (maxHealth * 8));
        scale.y -= (scaley / (maxHealth * 8));
        transform.localScale = scale;
    }
}
