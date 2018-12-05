using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGun : MonoBehaviour {

    //gun rotation
    public Transform crosshair;
    public int angleAdjustment = 0;
    public float H;
    public bool facingRight = true;
    public float zAngle;


    //gun rotation flip
    public SpriteRenderer wgSprite;
    public Transform heroT;
    public float heroToCrosshair;

    //shooting
    public float fireRate = 0;
    //public float waterDamage = 10;
    //public LayerMask whatToHit;
    public Transform firePoint;
    float timeToFire = 0;
    public Transform bulletPreFabRight;
    public Transform bulletPreFabLeft;

    //float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    //UI
    public Image waterBar;
    public Text waterCount;
    public float realValue = 100f;
    public float maxValue = 100f;
    public float waterCost = 1f;

    //gunknockback and animation
    public Rigidbody2D heroR;
    public Animator anim;
    public float gunForce;
    //public CameraShake cameraShake;
    public bool shooting = false;

    void Awake()
    {
        heroR = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        waterBar = GameObject.Find("WaterBar").GetComponent<Image>();
        waterCount = GameObject.Find("WaterCount").GetComponent<Text>();
        wgSprite = GetComponent<SpriteRenderer>();
        heroT = GameObject.Find("Player").GetComponent<Transform>();
        firePoint = transform.Find("FirePoint");
    }

    // Use this for initialization
    void Start () {

        //UI
        string temp = realValue.ToString();
        waterCount.text = temp;

    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.instance.alive == true)
        {
            H = Input.GetAxis("Horizontal");

            //difference between heroX position and crosshairX position to check if the crosshair is on the left or right of the player
            heroToCrosshair = crosshair.position.x - heroT.position.x;

            if (anim == null)
            {
                anim = GameObject.Find("WaterGun").GetComponent<Animator>();
                //print("Animator Nulo");
            }

            //Gun pointing option 1
            if (H > 0)
            {
                angleAdjustment = 0;
                facingRight = true;

            }
            else if (H < 0)
            {
                angleAdjustment = 180;
                facingRight = false;
            }

            //Gun pointing option 2
            /*if (heroToCrosshair > 0)
            {
                angleAdjustment = 0;
                facingRight = true;

            }
            else if (heroToCrosshair < 0)
            {
                angleAdjustment = 180;
                facingRight = false;
            }*/

            //Code to rotate the gun and make it follow the crosshair
            Vector2 direction = crosshair.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + angleAdjustment);
            zAngle = angle;


            //Shooting
            if (fireRate == 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    shoot();
                }
            }

            else
            {
                if (Input.GetButton("Fire1") && Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    shoot();
                    //StartCoroutine(cameraShake.Shake(0.3f, 1f));
                }

                else if (!Input.GetButton("Fire1"))
                {
                    anim.SetBool("Shooting", false);
                }
            }

            /*Gun pointing option 1: if the crosshair is pointing in the opposite direction that the player is looking, this code makes the gun flips in Y. This way the gun is always up. This way the gun 
            stays on top of the player when the player is looking right and the gun is pointing left*/
            /*Gun pointing option 2: if you want the gun to always look at the same direction as the players, so the player walk backwards when walking right and pointing the gun left for example, just 
            delete the code below. This way look cooler, but its needed to make an animation of the player walking backwards*/
            if (facingRight)
            {
                if (heroToCrosshair < 0)
                {
                    wgSprite.flipY = true;
                }
                else
                {
                    wgSprite.flipY = false;
                }
            }

            else if (!facingRight)
            {
                if (heroToCrosshair < 0)
                {
                    wgSprite.flipY = false;
                }
                else
                {
                    wgSprite.flipY = true;
                }
            }
        }
        else if (GameManager.instance.alive == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (shooting == true)
        {
            ShootKnockback();
        }
    }


    void shoot ()
    {

        if (realValue > 0)
        {
            shooting = true;
            anim.SetBool("Shooting", true);
            //waterSprayAim();
            AudioManager.instance.waterFXshoot();
            waterBarLess();

            if (facingRight)
            {
                WaterSprayRight();

                //this is used to limit the number of bullets spawn, in our case its not needed
                /*if (Time.time >= timeToSpawnEffect)
                {
                    Effect();
                    timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
                }*/

                //Debug.DrawLine(firePointPosition, (crosshairPosition - firePointPosition) * 1000, Color.cyan);

                /*if (hit.collider != null)
                {
                    Debug.DrawLine(firePointPosition, (crosshairPosition - firePointPosition) * 100, Color.red);
                    Debug.Log("hit" + hit.collider.name + waterDamage + "damage.");
                }*/
            }

            if (!facingRight)
            {
                WaterSprayLeft();

                //this is used to limit the number of bullets spawn, in our case its not needed
                /*if (Time.time >= timeToSpawnEffect)
                {
                    Effect();
                    timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
                }*/

                //Debug.DrawLine(firePointPosition, (crosshairPosition - firePointPosition) * 1000, Color.cyan);

                /*if (hit.collider != null)
                {
                    Debug.DrawLine(firePointPosition, (crosshairPosition - firePointPosition) * 100, Color.red);
                    Debug.Log("hit" + hit.collider.name + waterDamage + "damage.");
                }*/
            }
        }

        else if (realValue < 0)
        {
            anim.SetBool("Shooting", false);
        }
    }

    void ShootKnockback ()
    {
        if (zAngle > 120 && zAngle < 150 || zAngle < 60 && zAngle > 30 || zAngle < -120 && zAngle > -150 || zAngle > -60 && zAngle < -30)
        {
            gunForce = 0.35f;

            if (heroToCrosshair < 0)
            {
                heroR.AddForce(Vector2.right * gunForce, ForceMode2D.Impulse);
            }
            else
            {
                heroR.AddForce(Vector2.left * gunForce, ForceMode2D.Impulse);
            }
        }
        else if (zAngle > 150 && zAngle < 180 || zAngle > -180 && zAngle < -150 || zAngle < 30 && zAngle > -30)
        {
            gunForce = 0.45f;

            if (heroToCrosshair < 0)
            {
                heroR.AddForce(Vector2.right * gunForce, ForceMode2D.Impulse);
            }
            else
            {
                heroR.AddForce(Vector2.left * gunForce, ForceMode2D.Impulse);
            }
        }
        else if (zAngle > -120 && zAngle < -60)
        {
            heroR.AddForce(Vector2.up * gunForce, ForceMode2D.Impulse);
            gunForce = 0.2f;
        }
        else
        {
            gunForce = 0;
        }

        shooting = false;
    }

    void WaterSprayRight()
    {
        Instantiate(bulletPreFabRight, firePoint.position, firePoint.rotation);
    }

    void WaterSprayLeft()
    {
        Instantiate(bulletPreFabLeft, firePoint.position, firePoint.rotation);
    }

    //UI
    void waterBarLess()
    {
        realValue -= waterCost;
        waterBar.fillAmount = realValue / 100;
        string temp = realValue.ToString("F0");
        waterCount.text = temp;
    }

    /*void waterSprayAim ()
    {
        Vector2 crosshairPosition = crosshair.position;
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, crosshairPosition - firePointPosition, 100, whatToHit);
    }*/
}
