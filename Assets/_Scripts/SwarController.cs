using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private ImpactReceiver impact;
    private Krawl krawl;
    float gravity;
    public float speed=27f;
    public bool isAttacking = false;
  
    public bool inRange = false;
    public float attackCoolDown = 0f;
    public Collider hurtbox;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        impact = GetComponent<ImpactReceiver>();
        krawl = GetComponent<Krawl>();
        animator.SetBool("IsRunning", true);
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 90.81f * Time.deltaTime);
        }

        if (!krawl.stagger && !isAttacking && krawl.player.activeSelf == true)
        {
            transform.LookAt(krawl.player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if (!inRange)
            {
                animator.SetBool("IsRunning", true);
                Vector3 dir = (krawl.player.transform.position - transform.position).normalized;
                controller.Move(new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        if (krawl.player.activeSelf == false)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Idle");
        }

        if (!krawl.iframe)
        {
            if (!krawl.stagger && !isAttacking && inRange && attackCoolDown==0)
            {
                isAttacking = true;
                transform.LookAt(krawl.player.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                animator.SetTrigger("Attack");
            }

        }


        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }
        */
    }
   
    public void activateHurtBox()
    {
        weapon.GetComponent<SwarHeadbuttDamage>().hitOnce = false;
        hurtbox.enabled = true;
    }

    public void deactivateHurtBox()
    {
        hurtbox.enabled = false;
       
    }

    public void startIdle()
    {
        weapon.GetComponent<SwarHeadbuttDamage>().hitOnce = false;
        deactivateHurtBox();
        animator.SetTrigger("Idle");
        /*
        attackCoolDown = 10;
        //attackCoolDown = 30;
       
        krawl.iframe = false;
        krawl.stagger = false;
        isAttacking = false;
        krawl.deathCheck();
        //inRange = false;
      //  isAttacking = false;
        */
    }
}
