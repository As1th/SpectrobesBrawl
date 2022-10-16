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
    GameObject player;
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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void deactivateHurtBox()
    {
        hurtbox.enabled = false;
    }

    public void startIdle()
    {
        weapon.GetComponent<SwarHeadbuttDamage>().hitOnce = false;
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
