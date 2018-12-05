using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : EnemyController {


    // Use this for initialization
    void Start () {

        sprite = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.left * Time.deltaTime * speed);

        StartCoroutine(TimeDestroyProjectile());

       
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator TimeDestroyProjectile()
    {
        yield return new WaitForSeconds(5f);

        for (float i = 0f; i < 0.7f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.01f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.01f);
        }

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}


