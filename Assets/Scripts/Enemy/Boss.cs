using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyController {

    public GameObject bossBlock;
    public Image healthBossBar;
    public SpriteRenderer spriteBoss;
    public Animator bossAnim;
    public GameObject bigProjectile;
    private int rand;
    private int rand2;
    private bool speedSTG2 = false;
    private bool speedSTG3 = false;


    public float distance;

    private void Awake()
    {
        bossBlock = GameObject.Find("BossBlock");
        healthBossBar = GameObject.Find("HealthBoss").GetComponent<Image>();
        spriteBoss = GetComponent<SpriteRenderer>();
        bossAnim = GetComponent<Animator>();
        monsterT = GetComponent<Transform>();
    }

    void Start () {

        maxHealth = health;
        scalex = transform.localScale.x;
        scaley = transform.localScale.y;
      
    }
	
	void Update () {
        rand = Random.Range(0,4);
        rand2 = Random.Range(0, 5);

        distance = PlayerDistance();
        isMoving = (distance <= distanceAttack);


        if (isMoving)
        {
            if ((player.position.x > transform.position.x && face) || (player.position.x < transform.position.x && !face))
            {
                Flip();
            }
        }

        if (health <= maxHealth * 2 / 3 && health > maxHealth / 3)
        {
            if (!speedSTG2)
            {
                startTimeBtwShots = 1.8f;
                if (player.position.x > transform.position.x)
                {
                    speed = 2f;
                }
                else
                {
                    speed = -2f;
                }
                speedSTG2 = true;
            }

            bossAnim.SetBool("Angry", true);
            if (timeBtwShots <= 0)
            {
                timeBtwShots = startTimeBtwShots;
                if (rand2 == 0)
                {
                    //spawn projectile on enemys position with no rotation
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    //if we dont use this command, the enemy will shot 60 shots per second! -> if we dont use it. It can make a cool effect of a whip, but we wont use on this project                    
                }
                if (rand2 == 1)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
                if (rand2 == 2)
                {
                    Instantiate(bigProjectile, transform.position, Quaternion.identity);
                }
                if (rand2 == 3)
                {
                    Instantiate(bigProjectile, transform.position, Quaternion.identity);
                }
                if (rand2 == 4)
                {
                    Instantiate(bigProjectile, transform.position, Quaternion.identity);
                }

            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        else if (health <= maxHealth / 3)
        {
            if (!speedSTG3)
            {
                if (player.position.x > transform.position.x)
                {
                    speed = -2.5f;
                }
                else
                {
                    speed = 2.5f;
                }
                speedSTG3 = true;
            }
            startTimeBtwShots = 1.5f;
            if (timeBtwShots <= 0)
            {
                timeBtwShots = startTimeBtwShots;
                if (rand == 0)
                {
                    //spawn projectile on enemys position with no rotation
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    //if we dont use this command, the enemy will shot 60 shots per second! -> if we dont use it. It can make a cool effect of a whip, but we wont use on this project                    
                }
                if (rand == 1)
                {
                    Instantiate(bigProjectile, transform.position, Quaternion.identity);
                }
                if (rand == 2)
                {
                    Instantiate(bigProjectile, transform.position, Quaternion.identity);
                }
                if (rand == 3)
                {
                    Instantiate(bigProjectile, transform.position, Quaternion.identity);
                }

            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            if (timeBtwShots <= 0)
            {
                if (rand == 0)
                {
                    //spawn projectile on enemys position with no rotation
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    //if we dont use this command, the enemy will shot 60 shots per second! -> if we dont use it. It can make a cool effect of a whip, but we wont use on this project                    
                    timeBtwShots = startTimeBtwShots;
                }
                if (rand == 1)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = 0.05f;
                }
                if (rand == 2)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = 0.05f;
                }
                if (rand == 3)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = 0.05f;
                }

            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        if (health <= 0)
        {
            Destroy(this.gameObject, 0);
            AudioManager.instance.bossDieFX();
            AudioManager.instance.StartCoroutine(AudioManager.instance.bossDeathMusicDelay());
            AudioManager.instance.bossAlive = false;
            bossBlock.SetActive(false);
            Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), Quaternion.identity);
            TimeManager.instance.StartCoroutine(TimeManager.instance.CallSlowMotionBoss());
            UIManager.instance.StartCoroutine(UIManager.instance.CamTrasitionDelay());
        }

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBossBar.fillAmount -= 1/(maxHealth/damage);
        AudioManager.instance.hittingFX();
        //code to get smaller on hit
        Vector3 scale = transform.localScale;
        scale.x -= (scalex / (maxHealth*8));
        scale.y -= (scaley / (maxHealth*8));
        transform.localScale = scale;
    }
}
