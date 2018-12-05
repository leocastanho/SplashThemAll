using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour{

    private Player player;
    private bool aboutToDie = false;

    public int[] knockbackPowerX;
    public int[] knockbackPowerY;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.health == 1)
        {
            aboutToDie = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            if (!player.invunerable)
            {
                player.Hurt();
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[0], knockbackPowerY[0], transform.position));
                }

            }
        }
        if (other.CompareTag("FireBlob"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[1], knockbackPowerY[1], transform.position));
                }
                player.Hurt();

            }
        }
        if (other.CompareTag("Boss"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[2], knockbackPowerY[2], transform.position));
                }
                player.Hurt();

            }
        }
        if (other.CompareTag("Projectile"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[3], knockbackPowerY[3], transform.position));
                }
                player.Hurt();

            }
        }
        if (other.CompareTag("BigProjectile"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[3], knockbackPowerY[3], transform.position));
                }
                player.Hurt();

            }
        }
    }

    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            if (!player.invunerable)
            {
                player.Hurt();
                player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[0], knockbackPowerY[0], transform.position));
            }
        }
        if (other.CompareTag("FireBlob"))
        {
            if (!player.invunerable)
            {
                player.Hurt();
                player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[1], knockbackPowerY[1], transform.position));
            }
        }
        if (other.CompareTag("Boss"))
        {
            if (!player.invunerable)
            {
                player.Hurt();
                player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[2], knockbackPowerY[2], transform.position));
            }
        }
    }*/

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[0], knockbackPowerY[0], transform.position));
                }
                player.Hurt();

            }
        }
        if (other.CompareTag("FireBlob"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[1], knockbackPowerY[1], transform.position));
                }
                player.Hurt();

            }
        }
        if (other.CompareTag("Boss"))
        {
            if (!player.invunerable)
            {
                if (aboutToDie == false)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[2], knockbackPowerY[2], transform.position));
                }
                player.Hurt();
            }
        }
        if (other.CompareTag("Projectile"))
        {
            if (!player.invunerable)
            {
                if (player.health == 100)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[3], knockbackPowerY[3], transform.position));
                }
                player.Hurt();
            }
        }
        if (other.CompareTag("BigProjectile"))
        {
            if (!player.invunerable)
            {
                if (player.health == 100)
                {
                    player.StartCoroutine(player.Knockback(0.02f, knockbackPowerX[3], knockbackPowerY[3], transform.position));
                }
                player.Hurt();
            }
        }
    }

    void enemiesHit ()
    {

    }
}
