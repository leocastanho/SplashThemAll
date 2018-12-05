using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigProjectile : EnemyController {

    
    //public float speed;

    //private Transform player;
    private Vector2 target;
    //public SpriteRenderer sprite;


    // Use this for initialization
    void Start () {

        player = GameObject.Find("Player").GetComponent<Transform>();

        target = new Vector2(player.position.x, player.position.y);

        sprite = GetComponent<SpriteRenderer>();

        maxHealth = health;
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            StartCoroutine(TimeDestroyProjectile());
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            AudioManager.instance.enemyDieFX();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        AudioManager.instance.hittingFX();
        Vector3 scale = transform.localScale;
        scale.x -= (scalex / (maxHealth * 8));
        scale.y -= (scaley / (maxHealth * 8));
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
        {
            /*if (other.gameObject.tag == "Bullet" && health >= 0)
            {
                health -= 1;
                AudioManager.instance.hittingFX();
            }*/

            if (other.CompareTag("Player"))
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
    }

    IEnumerator TimeDestroyProjectile()
    {
        yield return new WaitForSeconds(0.1f);

        /*for (float i = 0f; i < 0.9f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }*/

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}


