using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health;
    public GameObject deathEffect;
    public float speed;
    public float distanceAttack;

    protected float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    
    public bool isMoving = false;
    public bool face = false;

    public Rigidbody2D rb2d;
    public Transform player;
    public Transform monsterT;
    public SpriteRenderer sprite;

    public float scalex;
    public float scaley;
    public float maxHealth;

    //public CameraShake cameraShake;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        monsterT = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        //cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();

        timeBtwShots = startTimeBtwShots;
    }

    public float PlayerDistance ()
    {
        return Vector2.Distance(player.position, transform.position);

    }

    /*public IEnumerator Flip ()
    {
        yield return new WaitForSeconds(0.1f);
        sprite.flipX = !sprite.flipX;
        speed *= -1;
    }*/
    protected void Flip ()
    {
        face = !face;

        Vector3 scala = monsterT.localScale;
        scala.x *= -1;
        monsterT.localScale = scala;
        speed *= -1;
    }

}
