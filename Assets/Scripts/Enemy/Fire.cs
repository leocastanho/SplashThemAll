using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : EnemyController {

    // Use this for initialization
    void Start()
    {
        health = 10;
        maxHealth = health;
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(this.gameObject);
            AudioManager.instance.enemyDieFX();
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), Quaternion.identity);
            TimeManager.instance.StartCoroutine(TimeManager.instance.CallSlowMotionEnemies());
        }

    }

    public void TakeDamage (float damage)
    {
        health -= damage;
        AudioManager.instance.hittingFX();
        //code to get smaller on hit
        Vector3 scale = transform.localScale;
        scale.x -= (scalex / (maxHealth * 6));
        scale.y -= (scaley / (maxHealth * 6));
        transform.localScale = scale;
    }

}
