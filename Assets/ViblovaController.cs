using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViblovaController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private ImpactReceiver impact;
    private Krawl krawl;
    float gravity;
    public float speed=27f;
    public bool isAttacking = false;
    GameObject player;
    public bool inRange = false;
    public float attackCoolDown = 0f;
    public Collider hurtbox;
    public Collider hurtbox2;
    public GameObject weapon;
    public GameObject weapon2;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        impact = GetComponent<ImpactReceiver>();
        krawl = GetComponent<Krawl>();
        animator.SetBool("IsRunning", true);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 90.81f * Time.deltaTime);
        }

        if (!krawl.stagger && !isAttacking && player.activeSelf == true)
        {
            transform.LookAt(player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if (!inRange)
            {
                animator.SetBool("IsRunning", true);
                Vector3 dir = (player.transform.position - transform.position).normalized;
                controller.Move(new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        if (player.activeSelf == false)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Idle");
        }

        if (!krawl.iframe)
        {
            if (!krawl.stagger && !isAttacking && inRange && attackCoolDown==0)
            {
                isAttacking = true;
                transform.LookAt(player.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                animator.SetTrigger("Attack");
            }

        }


        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }

    }

    public void activateHurtBox()
    {
        hurtbox.enabled = true;
        hurtbox2.enabled = true;
    }

    public void deactivateHurtBox()
    {
        hurtbox.enabled = false;
        hurtbox2.enabled = false;
    }

    public void startIdle()
    {
        weapon.GetComponent<ViblovaDamage>().hitOnce = false;
        weapon2.GetComponent<ViblovaDamage>().hitOnce = false;
        deactivateHurtBox();
        attackCoolDown = 10;
        //attackCoolDown = 30;
        animator.SetTrigger("Idle");
        krawl.iframe = false;
        krawl.stagger = false;
        isAttacking = false;
        krawl.deathCheck();
        //inRange = false;
      //  isAttacking = false;
    }
}
