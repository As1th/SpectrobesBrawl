using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteelaController : MonoBehaviour
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
    public GameObject projectiles;
    public GameObject spawnPoint;
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
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 90.81f * Time.deltaTime);
        }

        if (!krawl.stagger && !isAttacking && GetComponent<Krawl>().player.activeSelf == true)
        {
            transform.LookAt(GetComponent<Krawl>().player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if (!inRange)
            {
                animator.SetBool("IsRunning", true);
                Vector3 dir = (GetComponent<Krawl>().player.transform.position - transform.position).normalized;
                controller.Move(new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime);
            }
            else {
                animator.SetBool("IsRunning", false);
            }
        }
        if (GetComponent<Krawl>().player.activeSelf == false)
        {
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Idle");
        }

        if (!krawl.iframe)
        {
            if (!krawl.stagger && !isAttacking && inRange && attackCoolDown==0)
            {
                isAttacking = true;
                transform.LookAt(GetComponent<Krawl>().player.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                animator.SetTrigger("Attack");
            }

        }


        if (attackCoolDown > 0)
        {
            attackCoolDown--;
        }

    }

    public void spawnProjectiles()
    {
     
        
        var var = Instantiate(projectiles, spawnPoint.transform.position, transform.rotation);
        var.transform.LookAt(new Vector3(GetComponent<Krawl>().player.transform.position.x , GetComponent<Krawl>().player.transform.position.y+10f, GetComponent<Krawl>().player.transform.position.z));
        
    }

    public void startIdle()
    {
       
        attackCoolDown = 100;
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
