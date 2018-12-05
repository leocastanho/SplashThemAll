using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaterSprayRight : MonoBehaviour {

    //shooting in line
    /*public float moveSpeed = 6f;
      public float lifeTime = 0.75f;*/

    //shooting in arc
    public Rigidbody2D sprayR;
    public float force = 100f;
    public WaterGun wgScript;
    private bool shot = false;

    //muzzleFlash
    private Sprite defaultSprite;
    public Sprite muzzleFlash;
    public int framesToFlash = 3;
    private SpriteRenderer spriteRend;


    //effects
    public GameObject destroyEffect;
    public GameObject hitEnemyEffect;

    //things that the bullet colides
    public LayerMask whatIsSolid;

    //damage of the bullet
    public float damage;




    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRend.sprite;

        sprayR = GetComponent<Rigidbody2D>();

        wgScript = GameObject.Find("WaterGun").GetComponent<WaterGun>();

        StartCoroutine(FlashMuzzleFlash());
    }

    // Update is called once per frame
    void Update () {

        //shooting in arc
        float x = force * Mathf.Cos(wgScript.zAngle * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(wgScript.zAngle * Mathf.Deg2Rad);
        if (!shot)
        {
            sprayR.AddForce(new Vector2(x, y),ForceMode2D.Impulse);
            shot = true;
        }
        
        //Shooting in line
        /*transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Invoke("DestroyProjectile", lifeTime);*/

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Fire"))
            {
                hitInfo.collider.GetComponent<Fire>().TakeDamage(damage);
                DestroyProjectileEnemy();
            }
            else if (hitInfo.collider.CompareTag("FireBlob"))
            {
                hitInfo.collider.GetComponent<FireBlob>().TakeDamage(damage);
                DestroyProjectileEnemy();
            }
            else if (hitInfo.collider.CompareTag("Boss"))
            {
                hitInfo.collider.GetComponent<Boss>().TakeDamage(damage);
                DestroyProjectileEnemy();
            }
            else if (hitInfo.collider.CompareTag("Projectile"))
            {
                hitInfo.collider.GetComponent<Projectile>().TakeDamage(damage);
                DestroyProjectileEnemy();
            }
            else if (hitInfo.collider.CompareTag("BigProjectile"))
            {
                hitInfo.collider.GetComponent<BigProjectile>().TakeDamage(damage);
                DestroyProjectileEnemy();
            }
            else
            {
                DestroyProjectile();
            }

        }

    }

    IEnumerator FlashMuzzleFlash()
    {
        spriteRend.sprite = muzzleFlash;

        for (int i = 0; i < framesToFlash; i++)
        {
            yield return 0;
        }

        spriteRend.sprite = defaultSprite;
    }

    private void DestroyProjectile ()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void DestroyProjectileEnemy()
    {
        Instantiate(hitEnemyEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Invoke("DestroyProjectile",0f);
        }
        if (other.gameObject.tag == "Fire")
        {
            Invoke("DestroyProjectile", 0.2f);
        }
        if (other.gameObject.tag == "FireBlob")
        {
            Invoke("DestroyProjectile", 0.2f);
        }
        if (other.gameObject.tag == "Boss")
        {
            Invoke("DestroyProjectile", 0.2f);
        }
    }*/
}
