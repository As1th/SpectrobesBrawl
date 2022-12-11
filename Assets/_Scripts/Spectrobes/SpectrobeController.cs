using UnityEngine;

public class SpectrobeController : MonoBehaviour
{

    // Start is called before the first frame update
    private CharacterController controller;
    private Animator animator;
    public float speed;
    public bool isAttacking;
    float turnSmoothVelocity;
    float dashcooldown = 0;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float attackCoolDown = 0f;
    public GameObject CHParts;
    public float forwardCHCharge;
    public GameObject normalAttackCollider;
    Vector3 velocity;
    public bool stagger;
    public bool iframe = false;
    public bool permaGround;
    public Collider hurtbox;
    //public float health = 20;
    public GameObject cloud;
    public GameObject scripts;
    public GameObject EvolvedForm;
    public ParticleSystem evolvePart;
    public ParticleSystem groundFire;
    public GameObject CHPartsEvolved;
    public AudioSource dashSound;
    public AudioSource swingSound;
    public AudioSource chSound;
    public bool evolved=false;
    public bool touch;
    
    public GameObject spawnPoint;
    public ParticleSystem CHGlowParticles;

    void Start()
    {
        cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hurtbox = normalAttackCollider.GetComponent<Collider>();
      
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
        //hitNormal = hit.normal;
        if (hit.gameObject.layer == 10)
        {
            if (IsBAboveA(A: hit.gameObject.transform, B: transform, Ra: hit.gameObject.GetComponent<CharacterController>().radius, Rb: 9.5f))
            {
                touch = true;
            }
           
        }
    }

    public static bool IsBAboveA(Transform A, Transform B, float Ra, float Rb)
    {
        Vector3 Vab = (B.position - A.position) - Vector3.Dot(B.position - A.position, A.up) * A.up;
        if (Vab.magnitude < (Ra + Rb))
        {
            if (Vector3.Dot(B.position - A.position, A.up) > 0)
            {
                return true; // object B is above object A
            }
        }
        return false; // object B is not above object A
    }
    // Update is called once per frame
    void Update()
    {
        if (touch)
        {
            gameObject.GetComponent<CharacterController>().SimpleMove(transform.forward * 100);
        }
        touch = false;
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 40.81f * Time.deltaTime);
        }
        if (permaGround)
        {
            float verticalVelosity = 0;
            if (controller.isGrounded)
            {
                verticalVelosity -= 0;
            }
            else
            {
                verticalVelosity -= 1;
            }


            controller.Move(new Vector3(0, verticalVelosity, 0));
        }
        
        if (!isAttacking && Time.timeScale != 0)
        {

           
            if (Input.GetButtonDown("Attack"))
            {
                if (attackCoolDown == 0 && !stagger) //used to be && controller.isGrounded instead of && !stagger
                {
                   
                    animator.SetTrigger("Attack");
                    isAttacking = true;
                    if (groundFire != null)
                    {
                        groundFire.Play();                   
                    }
                    if (swingSound!=null)
                    {
                        swingSound.Play();
                    }
                }

            }
            else if (Input.GetButtonDown("Attack2"))
            {
                if (attackCoolDown == 0 && !stagger && scripts.GetComponent<GameManager>().ch >= 50) //used to be && controller.isGrounded instead of && !stagger
                {

                    chSound.Play();
                    if (CHGlowParticles != null)
                    {
                        CHGlowParticles.Play();
                    }
                    if (CHGlowParticles != null)
                    {
                        CHGlowParticles.Play();
                    }
                   
                    scripts.GetComponent<GameManager>().ch = 0;
                    iframe = true;
                    permaGround = true;
                    animator.SetTrigger("Attack2");
                    if (forwardCHCharge > 0)
                    {
                        GetComponent<ImpactReceiver>().AddImpact(transform.forward, forwardCHCharge);
                    }
                    isAttacking = true;

                }

            }
            else if (Input.GetButtonDown("Evolve"))
            {
                if (attackCoolDown == 0 && !stagger && !evolved && scripts.GetComponent<GameManager>().ev >= 400) //used to be && controller.isGrounded instead of && !stagger
                {
                    var eff = Instantiate(evolvePart, new Vector3(transform.position.x, transform.position.y + 18f, transform.position.z), Quaternion.identity);
                    scripts.GetComponent<GameManager>().ev = 0;
                    scripts.GetComponent<GameManager>().healthStore = scripts.GetComponent<GameManager>().health;
                    var var = Instantiate(EvolvedForm, transform.position, transform.rotation);
                    eff.transform.parent = var.transform;
                    Camera.main.transform.parent = var.transform;
                    var.GetComponent<SpectrobeController>().scripts = scripts;
                    var.GetComponent<SpectrobeController>().evolved = true;
                    foreach (GameObject k in scripts.GetComponent<GameManager>().currentKrawl)
                    {
                        k.GetComponent<Krawl>().player = var;
                    }
                    Destroy(this.gameObject);
                }

            }
            else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                if (scripts.GetComponent<Menu>().introScene)
                {
                    Camera.main.transform.parent.GetComponent<Rotate>().enabled = false;
                    Camera.main.transform.parent.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.parent.transform.rotation, transform.rotation, 5);//Quaternion.Lerp(camAnchor.transform.rotation, transform.rotation, 10000);
                }

                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                Vector3 moveDir = new Vector3(0, 0, 0);
                if (direction.magnitude >= 0.1f && !stagger)
                {
                    float targetAngle;
                    if (vertical >= 0)
                    {
                        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                        transform.rotation = Quaternion.Euler(0f, angle, 0f);
                        animator.SetFloat("WalkCycleMultiplier", 1);
                    }
                    else {
                        animator.SetFloat("WalkCycleMultiplier", -1);
                        if (Mathf.Approximately(horizontal, 0))
                        {
                            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                        } else
                        {
                            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                            transform.rotation = Quaternion.Euler(0f, angle, 0f);
                        }

                        // transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    }


                    moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }

                animator.SetBool("IsRunning", true);
                if (Input.GetButtonDown("Dash"))
                {
                    if (dashcooldown == 0 && attackCoolDown == 0)
                    {
                        dashSound.Play();
                        iframe = true;
                        animator.SetTrigger("ForwardDash");
                        isAttacking = true;
                        GetComponent<ImpactReceiver>().AddImpact(moveDir, 500);
                        dashcooldown = 36;

                    }

                }
            }
            else
            {
                if (scripts.GetComponent<Menu>().introScene)
                {
                    Camera.main.transform.parent.GetComponent<Rotate>().enabled = true;
                }
                animator.SetBool("IsRunning", false);
            }

        }


     



    }

    public void FixedUpdate()
    {
        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }


        if (dashcooldown > 0)
        {
            dashcooldown--;
        }
    }
    public void deathCheck()
    {
        if (scripts.GetComponent<GameManager>().health <= 0)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
            Camera.main.gameObject.transform.parent = null;
            Camera.main.gameObject.GetComponent<Rotate>().enabled = true;
            scripts.GetComponent<GameManager>().lost = true;
            scripts.GetComponent<GameManager>().defeatSequence();
            this.gameObject.SetActive(false);
            
        }
    }
    public void Hit(Vector3 dir, float force, float dmg)
    {
        scripts.GetComponent<GameManager>().ch += 5;
        if (!evolved)
        {
            scripts.GetComponent<GameManager>().ev += 5;
        }
            deactivateHurtBox();
        stagger = true;
        GetComponent<ImpactReceiver>().AddImpact(dir, force);
        // transform.rotation = Quaternion.LookRotation(new Vector3(-dir.x, 0, -dir.z));
        scripts.GetComponent<GameManager>().health -= dmg;
        animator.SetTrigger("Hit");

    }
    public void spawnCHPartsKomadoros()
    {

        Instantiate(CHParts, spawnPoint.transform.position, transform.rotation);
       // Instantiate(CHParts, spawnPoint.transform.Find("SpawnPoint2").transform.position, transform.rotation);
    }


    public void spawnCHPartsSpikan()
    {
       
        Instantiate(CHParts, normalAttackCollider.transform.position, transform.rotation);
        Instantiate(CHParts, normalAttackCollider.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y+35, transform.eulerAngles.z));
        Instantiate(CHParts, normalAttackCollider.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 35, transform.eulerAngles.z));
    }

    public void spawnCHPartsKomanoto()
    {

        Instantiate(CHParts, spawnPoint.transform.position, transform.rotation);
        Instantiate(CHParts, spawnPoint.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 35, transform.eulerAngles.z));
        Instantiate(CHParts, spawnPoint.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 35, transform.eulerAngles.z));
    }


    public void spawnSpikanorCH()
    {
        Instantiate(CHPartsEvolved, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));

       
    }
    public void startIdle()
    {
        deactivateHurtBox();
        permaGround = false;
        iframe = false;
        stagger = false;
        attackCoolDown = 11;
        deathCheck();
        animator.SetTrigger("Idle");
        isAttacking = false;
    }



    public void activateHurtBox()
    {
        hurtbox.enabled = true;
    }

    public void deactivateHurtBox()
    {
        hurtbox.enabled = false;
    }


}
