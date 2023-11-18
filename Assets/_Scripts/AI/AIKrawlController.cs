using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIKrawlController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public List<Node> path = new List<Node>();
    public float moveSpeed;
    public CharacterController controller;
    public GameObject player;
    Vector3 target;
    public NPCStates currentState = NPCStates.Chase;
    public float attackRange;
    public float attackCooldown;

    public enum NPCStates
    {
        Chase,
        Guard,
        Attack,
        Retreat,
        Stagger
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        pathfinding = GameObject.Find("A*").GetComponent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchState();

        if (attackCooldown > 0)
        {
            attackCooldown--;
        }
    }


    private void SwitchState()
    {
        switch (currentState)
        {
            case NPCStates.Stagger:
                Stagger();
                break;
            case NPCStates.Chase:
                Chase();
                break;
            case NPCStates.Guard:
                Chase();
                break;
            case NPCStates.Attack:
                Attack();
                break;
            case NPCStates.Retreat:
                Chase();
                break;
            default:
                Chase(); 
                break;
        }
    }

    private void Chase()
    {
        if (GetComponent<Krawl>().gm.player.GetComponent<SpectrobeController>().evolved)
        {
            currentState = NPCStates.Retreat;
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            currentState = NPCStates.Attack;
        }
        
        path = pathfinding.FindPath(this.transform.position, player.transform.position);
        if (path.Count > 0)
        {
            target = new Vector3(path[0].worldPosition.x, this.transform.position.y, path[0].worldPosition.z);
        }
        if (!GetComponent<Animator>().GetBool("IsRunning"))
        {
            
           GetComponent<Animator>().SetTrigger("Idle");
        }
        GetComponent<Animator>().SetBool("IsRunning", true);
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        controller.SimpleMove(((target - transform.position).normalized) * moveSpeed);

    }

    private void Attack()
    {
        if (GetComponent<Krawl>().gm.player.GetComponent<SpectrobeController>().evolved)
        {
            currentState = NPCStates.Retreat;
        }
        if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
        {
            currentState = NPCStates.Chase;
        }

      

        transform.LookAt(player.transform);

        //  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (attackCooldown == 0)
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
            GetComponent<Animator>().SetTrigger("Attack");
            attackCooldown = 100;
        }
        else {
           
        }

    }

    private void Stagger()
    {
        GetComponent<Animator>().SetBool("IsRunning", false);
        if (GetComponent<Krawl>().staggerCountdown <= 0)
        {
            GetComponent<Krawl>().deathCheck();
            currentState = NPCStates.Chase;

            
        }
    }
}
