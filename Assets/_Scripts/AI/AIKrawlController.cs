using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIKrawlController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public PathfindingD pathfindingD;
    public bool useDijkstra;
    public List<Node> path = new List<Node>();
    public float moveSpeed;
    public CharacterController controller;
    public bool grouped = false;
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
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();

        if (useDijkstra)
        {
            pathfindingD = GameObject.Find("Dijkstra").GetComponent<PathfindingD>();
        }
        else
        {
            pathfinding = GameObject.Find("A*").GetComponent<Pathfinding>();
        }
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
        if (useDijkstra)
        {
           
            path = pathfindingD.FindPath(this.transform.position, player.transform.position);

        } else
        {
            path = pathfinding.FindPath(this.transform.position, player.transform.position);
        }
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


       
        foreach (GameObject k in GetComponent<Krawl>().gm.currentKrawl) {
         if(k != this.gameObject)
            {
                if (Vector3.Distance(k.transform.position, transform.position) < 70)
                {
                    if(!k.GetComponent<AIKrawlController>().grouped)
                    grouped = true;
                    break;
                } else
                {
                    grouped = false;
                }
            }

        }

        if (grouped)
        {
            controller.SimpleMove(transform.right * moveSpeed);
          
        }
        else
        {
            controller.SimpleMove(((target - transform.position).normalized) * moveSpeed);
        }
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
