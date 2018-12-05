using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //variavel da velocidade de movimento do personagem
    public float walkForce;
    public float runForce;
    public float maxWalkSpeed;
    public float slowDownAmount;
    public float fakeFriction;
    public float H;

    //variavel do Rigidbody2D, Transform e Animator do personagem
    Rigidbody2D heroR;
    Transform heroT;
    Animator anim;

    //variavel para a forca e velocidade do pulo
    public float jumpVelocity;

    //variavel para controlar a gravidade do pulo
    public float fallMultiplier;
    public float lowJumpMultiplier;

    //Outro jeito de verificar se o personagem está no chão para liberar o pulo, opção 6 (professor willian)
    public Transform check;
    public LayerMask whatIsGround;
    public float ray = 0.1f;
    public bool grounded;

    //ghost jump
    public bool ghostJump;
    public float ghostJumpDelay;

    //variavel para ativar o comando de pulo no FixedUpdate
    bool activeJump;

    //variavel para virar o personagem
    public bool face = false;
    public WaterGun wgScript;

    public Image healthIcon;
    public Text healthCount;

    //PlayerStats
    public float health = 6f;
    public float maxHealth;

    //invunerable
    public bool invunerable = false;
    private bool aboutToDie = false;

    //Hero and gun spriterenderer
    SpriteRenderer sprite;
    SpriteRenderer wgSprite;

    //deathGun
    public Transform gunPoint;
    public Transform deathGunLeft;
    public Transform deathGunRight;
    private bool deathGunSummoned = false;

    //shooters
    public GameObject Shooter1;
    public GameObject Shooter2;
    public GameObject Shooter3;
    public GameObject Shooter4;
    public GameObject Shooter5;
    public GameObject Shooter6;



    void Awake()
    {
        //comando para o script pegar o Transform, Rigidbody2D e Animator do personagem
        heroT = GetComponent<Transform>();
        heroR = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        gunPoint = transform.Find("GunPoint");
        wgSprite = GameObject.Find("WaterGun").GetComponent<SpriteRenderer>();
        wgScript = GameObject.Find("WaterGun").GetComponent<WaterGun>();
        healthIcon = GameObject.Find("HealthIcon").GetComponent<Image>();
        healthCount = GameObject.Find("HealthCount").GetComponent<Text>();
        check = GameObject.Find("BottomCheck").GetComponent<Transform>();

    }

    //Opção 4 pulo (video board to bit games)
    /*void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().size;  
    }*/

    //Opção 5 pulo (video board to bit games)
    /*void Awake()
    {
        playerSize = GetComponent<CapsuleCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }*/

    // Use this for initialization
    void Start()
    {

        string temp = health.ToString();
        //healthCount.text = temp;
        maxHealth = health;

    }

    // Update is called once per frame
    void Update()
    {

        if (check == null)
        {
            check = GameObject.Find("BottomCheck").GetComponent<Transform>();
            //print("BottomCheck Nulo");
        }

        H = Input.GetAxis("Horizontal");

        grounded = Physics2D.OverlapCircle(check.position, ray, whatIsGround);

        TextLimits();

        if (GameManager.instance.alive == true)
        {

            /*if (H != 0 && Input.GetKey(KeyCode.Q))
            {
                //comando para movimento do personagem
                transform.Translate(new Vector3(H * runForce * Time.deltaTime, 0, 0));

            }
            else if (H != 0 && !Input.GetKey(KeyCode.Q))
            {
                //comando para movimento do personagem
                transform.Translate(new Vector3(H * walkForce * Time.deltaTime, 0, 0));

            }*/

            /*if (Input.GetKey(KeyCode.G))
            {
                invunerable = !invunerable;
            }*/

            //comando para virar o personagem
            if (H > 0 && face)
            {
                flip();

            }
            if (H < 0 && !face)
            {
                flip();
            }

            /*if (wgScript.heroToCrosshair > 0 && face)
            {
                flip();

            }
            if (wgScript.heroToCrosshair < 0 && !face)
            {
                flip();
            }*/



            //opção 6 para liberar pulo (professor willian)


            //comando para o pulo, opção 4 e 5 (video board to bits games) e opção 6 (Prof willian)
            if (Input.GetKeyDown(KeyCode.Space) && ghostJump)
            {
                activeJump = true;
                //comando para o som do pulo usando o audiomanager
                AudioManager.instance.jumpingFX();
            }

            //Animation
            if (grounded)
            {
                anim.SetBool("Falling", false);
                anim.SetBool("Jump", false);
                ghostJump = true;

                if (heroR.velocity.x >= 0.06f || heroR.velocity.x <= -0.06f)
                {
                    anim.SetBool("Walking", true);
                    anim.SetBool("Idle", false);
                }
                else
                {
                    anim.SetBool("Walking", false);
                    anim.SetBool("Idle", true);
                }
            }
            if (!grounded)
            {
                StartCoroutine(DelayNotGrounded());
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);

                if (heroR.velocity.y <= 0)
                {
                    anim.SetBool("Jump", false);
                    anim.SetBool("Falling", true);

                }
                else
                {
                    anim.SetBool("Jump", true);
                    anim.SetBool("Falling", false);
                }
            }

        }

        else if (GameManager.instance.alive == false)
        {
            heroR.velocity = new Vector2 (0,heroR.velocity.y);
            anim.SetBool("Dead", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Falling", false);

            if (deathGunSummoned == false)
            {
                if (face)
                {
                    Instantiate(deathGunLeft, gunPoint.position, gunPoint.rotation);
                }
                if (!face)
                {
                    Instantiate(deathGunRight, gunPoint.position, gunPoint.rotation);
                }
                
                deathGunSummoned = true;
            }
        }

        /*if (wgScript.heroToCrosshair < 0)
        {
            sprite.flipX = true;
        }*/

        if (health == 1)
        {
            aboutToDie = true;
        }
    }




    void FixedUpdate()
    {
        if (GameManager.instance.alive == true)
        {
            Walk();

            if (activeJump)
            {
                //pulo, opção 2, nao muda muita coisa da opção 1, mas com esse codigo o pulo está usando a física da Unity, ao invés de controla-lo manualmente como na opção 1
                heroR.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

                //joga a variavel ativaPulo pra falso, pra que o personagem nao fique pulando loucamente
                activeJump = false;
            }

            //opção 4 pulo, quando tem um gap no chão, o raycast não pega e o personagem não pula, parece ser a pior opção (video board to bit games)
            /*else
            {
                Vector2 rayStart = (Vector2)transform.position + Vector2.down * playerSize.y * 0.5f; //usa-se o 0.5f pq o transform.position está no meio do player e queremos fazer uma linha de checagem em baixo do player
                grounded = Physics2D.Raycast(rayStart, Vector2.down, groundedSkin, mask);
            }*/

            //opção 5 pulo, soluciona o problema da opção 4 (video board to bit games)
            /*else
            {
                Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f; //usa-se o 0.5f pq o transform.position está no meio do player e queremos fazer uma linha de checagem em baixo do player
                grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
            }*/



            //comando para melhorar o pulo, deixar mais suave, opção B FixedUpdate (video board to bit games), em alguns jogos esse codigo pode bugar
            //quando o personagem estiver caindo a velocidade de queda dele aumenta
            if (!ghostJump)
            {
                if (heroR.velocity.y < 0)
                {
                    heroR.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
                else if (heroR.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
                {
                    heroR.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
                }
            }

            //quanto mais apertar o botão de pulo, mais alto o personagem pula
            //o else if fala que se o personagem estiver subindo e o jogador não estiver pressionando o botão de pulo, é aplicado uma força pra baixo, fazendo com que o pulo fique mais fraco



            /*comando para melhorar o pulo, deixar mais suave, opção C (video board to bit games atualizado), nao muda muita coisa da opção B, mas com esse codigo o pulo está usando a física da Unity, 
            ao invés de controla-lo manualmente como na opção B*/
            //Tem que testar qual funciona melhor com o jogo que estiver fazendo
            //quando o personagem estiver caindo a velocidade de queda dele aumenta

            /*if (birdR.velocity.y < 0)
            {
                birdR.gravityScale = multiplicadorQueda;
            }

            //quanto mais apertar o botão de pulo, mais alto o personagem pula
            //o else if fala que se o personagem estiver subindo e o jogador não estiver pressionando o botão de pulo, é aplicado uma força pra baixo, fazendo com que o pulo fique mais fraco
            else if (birdR.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                birdR.gravityScale = multiplicadorPuloBaixo;
            }

            //quando não estiver fazendo nem um nem outro comando, a gravidade do personagem volta pra gravidade normal
            else
            {
                birdR.gravityScale = 1f;
            }*/
        }
    }


    //metodo para virar o personagem
    void flip()
    {
        face = !face;

        Vector3 scala = heroT.localScale;
        scala.x *= -1;
        heroT.localScale = scala;
    }

    public void Hurt()
    {
        if (!invunerable)
        {
            if (aboutToDie == false)
            {
                StartCoroutine(InvunerableTime());
            }

            health -= 1f;
            healthIcon.fillAmount -= 1 / maxHealth;
            string temp = health.ToString();
            healthCount.text = temp;
            AudioManager.instance.gettingHurtFX();

        }

        if (health == 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public IEnumerator Knockback (float knocbackDuration, float kbpwrx, float kbpwry, Vector3 knockbackDirection)
    {
        heroR.velocity = new Vector2(0, 0);
        float timer = 0;
        while (knocbackDuration > timer)
        {
            timer += Time.deltaTime;
            heroR.AddForce(new Vector3(knockbackDirection.x * kbpwrx * transform.localScale.x, knockbackDirection.y * kbpwry, transform.position.z));
        }
        yield return 0;
    }

    public IEnumerator InvunerableTime ()
    {
        invunerable = true;

        for (float i = 0f; i < 0.7f; i += 0.1f)
        {
            sprite.enabled = false;
            wgSprite.enabled = false;
            yield return new WaitForSeconds(0.09f);
            sprite.enabled = true;
            wgSprite.enabled = true;
            yield return new WaitForSeconds(0.09f);
        }

        invunerable = false;
    }

    void TextLimits()
    {
        if (health >= 6)
        {
            health = 6f;
            healthCount.text = health.ToString();
        }
        if (health <= 0)
        {
            health = 0f;
            healthCount.text = health.ToString();
        }
    }

    IEnumerator DelayNotGrounded()
    {
        yield return new WaitForSeconds(ghostJumpDelay);
        ghostJump = false;

        yield return null;

    }

    void Walk()
    {
        print(heroR.velocity.ToString());

        //fake fritcion easing the x speed of the player
        Vector2 easeVelocity = heroR.velocity;
        easeVelocity.y = heroR.velocity.y;
        easeVelocity.x *= fakeFriction;        
        if (grounded)
        {
            heroR.velocity = easeVelocity;
        }

        //player movement
        if (H > 0)
        {
            heroR.AddForce(new Vector2(walkForce, 0f), ForceMode2D.Force);
        }

        else if (H < 0)
        {
            heroR.AddForce(new Vector2(-walkForce, 0f), ForceMode2D.Force);
        }
        
        //player slowly stop
        else if (H < 1 || H > -1)
        {
            if (heroR.velocity.x > 0)
            {
                heroR.velocity = new Vector2(heroR.velocity.x - (slowDownAmount *Time.deltaTime), heroR.velocity.y);
            }
            if (heroR.velocity.x < 0)
            {
                heroR.velocity = new Vector2(heroR.velocity.x - (-slowDownAmount * Time.deltaTime), heroR.velocity.y);
            }
        }


        //player maxspeed
        if (heroR.velocity.x > maxWalkSpeed)
        {
            heroR.velocity = new Vector2 (maxWalkSpeed, heroR.velocity.y);
        }
        else if (heroR.velocity.x < -maxWalkSpeed)
        {
            heroR.velocity = new Vector2 (-maxWalkSpeed, heroR.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trigger1")
        {
            StartCoroutine(ShooterDelay(Shooter1, Shooter2));
        }
        if (other.gameObject.tag == "Trigger2")
        {
            StartCoroutine(ShooterDelay(Shooter2, Shooter3));
        }
        if (other.gameObject.tag == "Trigger3")
        {
            StartCoroutine(ShooterDelay(Shooter3, Shooter4));
        }
        if (other.gameObject.tag == "Trigger4")
        {
            StartCoroutine(ShooterDelay(Shooter4, Shooter5));
        }
        if (other.gameObject.tag == "Trigger5")
        {
            StartCoroutine(ShooterDelay(Shooter5, Shooter6));
        }
        if (other.gameObject.tag == "Trigger6")
        {
            Shooter6.SetActive(false);
        }
    }

    IEnumerator ShooterDelay (GameObject shooterA, GameObject shooterB)
    {
        shooterB.SetActive(true);

        yield return new WaitForSeconds(5f);
        shooterA.SetActive(false);
    }
}


