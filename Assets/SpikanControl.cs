using UnityEngine;

public class SpikanControl : MonoBehaviour
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
    public GameObject tailball;
    Vector3 velocity;
    public bool stagger;
    public bool iframe = false;
    public bool permaGround;
    public Collider hurtbox;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        hurtbox = tailball.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (!isAttacking)
        {


            if (Input.GetButtonDown("Attack"))
            {
                if (attackCoolDown == 0 && controller.isGrounded)
                {
                  
                    animator.SetTrigger("Attack");
                    isAttacking = true;
                }

            }
            else if (Input.GetButtonDown("Attack2"))
            {
                if (attackCoolDown == 0 && controller.isGrounded)
                {
                    iframe = true;
                    permaGround = true;
                    animator.SetTrigger("Attack2");
                    isAttacking = true;
                }

            }
            else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                Vector3 moveDir = new Vector3(0, 0, 0);
                if (direction.magnitude >= 0.1f && !stagger)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }

                animator.SetBool("IsRunning", true);
                if (Input.GetButtonDown("Dash"))
                {
                    if (dashcooldown == 0 && attackCoolDown == 0)
                    {

                        iframe = true;
                        animator.SetTrigger("ForwardDash");
                        isAttacking = true;
                        GetComponent<ImpactReceiver>().AddImpact(moveDir, 485);
                        dashcooldown = 38;

                    }

                }
            }
            else
            {

                animator.SetBool("IsRunning", false);
            }

        }


        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }


        if (dashcooldown > 0)
        {
            dashcooldown--;
        }



    }

    public void Hit(Vector3 dir, float force, float dmg)
    {
        deactivateHurtBox();
        stagger = true;
        GetComponent<ImpactReceiver>().AddImpact(dir, force);
       // transform.rotation = Quaternion.LookRotation(new Vector3(-dir.x, 0, -dir.z));
        animator.SetTrigger("Hit");

    }


    public void spawnCHParts()
    {
        Instantiate(CHParts, tailball.transform.position, transform.rotation);
       
        Instantiate(CHParts, tailball.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y+35, transform.eulerAngles.z));
        Instantiate(CHParts, tailball.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 35, transform.eulerAngles.z));
    }

    public void startIdle()
    {
        deactivateHurtBox();
        permaGround = false;
        iframe = false;
        stagger = false;
        attackCoolDown = 18;
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
