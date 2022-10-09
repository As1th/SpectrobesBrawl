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
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (!isAttacking)
        {


            if (Input.GetButtonDown("Attack"))
            {
                if (attackCoolDown == 0)
                {

                    animator.SetTrigger("Attack");
                    isAttacking = true;
                }

            }
            else if (Input.GetButtonDown("Attack2"))
            {
                if (attackCoolDown == 0)
                {

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
                if (direction.magnitude >= 0.1f)
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


    public void FixedUpdate()
    {

        
    }


    public void spawnCHParts()
    {
        Instantiate(CHParts, tailball.transform.position, transform.rotation);
       
        Instantiate(CHParts, tailball.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y+35, transform.eulerAngles.z));
        Instantiate(CHParts, tailball.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 35, transform.eulerAngles.z));
    }

    public void startIdle()
    {
        attackCoolDown = 20;
        animator.SetTrigger("Idle");
        isAttacking = false;
    }

    

   
    

}
