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
    GameObject player;
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
        
    }

    

    public void startIdle()
    {
        //attackCoolDown = 30;
        animator.SetTrigger("Idle");
        krawl.iframe = false;
      //  isAttacking = false;
    }
}
