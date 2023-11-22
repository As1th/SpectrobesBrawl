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
    public bool defender;
    public List<Node> path = new List<Node>();
    public float moveSpeed;
    public CharacterController controller;
    public bool grouped = false;
    public GameObject player;
    Vector3 target;
    public NPCStates currentState = NPCStates.Chase;
    public float attackRange;
    public float guardChaseRange;
     float attackCooldown;
    public float groupingDistance;
    Krawl krawl;
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
        krawl = GetComponent<Krawl>();
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
                Guard();
                break;
            case NPCStates.Attack:
                Attack();
                break;
            case NPCStates.Retreat:
                Retreat();
                break;
            default:
                Chase(); 
                break;
        }
    }
    private void Retreat()
    {
        if (!krawl.gm.player.GetComponent<SpectrobeController>().ultimate)
        {
            currentState = NPCStates.Chase;
        }
        GameObject furthestVortexFromPlayer = krawl.gm.spawnLoci[0];

        foreach (GameObject vortex in krawl.gm.spawnLoci)
        {
            if (Vector3.Distance(vortex.transform.position, player.transform.position) > Vector3.Distance(furthestVortexFromPlayer.transform.position, player.transform.position))
            {
                furthestVortexFromPlayer = vortex;
            }
        }

        if (useDijkstra)
        {

            path = pathfindingD.FindPath(this.transform.position, furthestVortexFromPlayer.transform.position);

        }
        else
        {
            path = pathfinding.FindPath(this.transform.position, furthestVortexFromPlayer.transform.position);
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
        foreach (GameObject k in krawl.gm.currentKrawl)
        {
            if (k != this.gameObject)
            {
                if (Vector3.Distance(k.transform.position, transform.position) < groupingDistance)
                {
                    if (!k.GetComponent<AIKrawlController>().grouped)
                        grouped = true;
                    break;
                }
                else
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
    private void Chase()
    {

        if (krawl.gm.player.GetComponent<SpectrobeController>().ultimate)
        {
            currentState = NPCStates.Retreat;
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            currentState = NPCStates.Attack;
        }
        if (defender)
        {
            GameObject closestVortex = krawl.gm.spawnLoci[0];

            foreach (GameObject vortex in krawl.gm.spawnLoci)
            {
                if (Vector3.Distance(vortex.transform.position, transform.position) < Vector3.Distance(closestVortex.transform.position, transform.position))
                {
                    closestVortex = vortex;
                }
            }
           
                if (Vector3.Distance(closestVortex.transform.position, transform.position) > guardChaseRange)
                {
                    currentState = NPCStates.Guard;

            }
            
            
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


       
        foreach (GameObject k in krawl.gm.currentKrawl) {
         if(k != this.gameObject)
            {
                if (Vector3.Distance(k.transform.position, transform.position) < groupingDistance)
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

    private void Guard()
    {
        if (krawl.gm.player.GetComponent<SpectrobeController>().ultimate)
        {
            currentState = NPCStates.Retreat;
        }
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            currentState = NPCStates.Attack;
        }
        GameObject closestVortex = krawl.gm.spawnLoci[0];
       
        foreach (GameObject vortex in krawl.gm.spawnLoci)
        {
            if(Vector3.Distance(vortex.transform.position, transform.position) < Vector3.Distance(closestVortex.transform.position,transform.position))
            {
                  closestVortex = vortex;
            }
        }
        if (Vector3.Distance(closestVortex.transform.position, player.transform.position) <= guardChaseRange)
        {
            currentState = NPCStates.Chase;
        }
        if (Vector3.Distance(closestVortex.transform.position, transform.position) < 35)
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
            GetComponent<Animator>().SetTrigger("Idle");
            return;
        }
        if (useDijkstra)
        {

        path = pathfindingD.FindPath(this.transform.position, closestVortex.transform.position);

        }
        else
        {
            path = pathfinding.FindPath(this.transform.position, closestVortex.transform.position);
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
        foreach (GameObject k in krawl.gm.currentKrawl)
        {
            if (k != this.gameObject)
            {
                if (Vector3.Distance(k.transform.position, transform.position) < groupingDistance)
                {
                    if (!k.GetComponent<AIKrawlController>().grouped)
                        grouped = true;
                    break;
                }
                else
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
        if (krawl.gm.player.GetComponent<SpectrobeController>().ultimate)
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
        if (krawl.staggerCountdown <= 0)
        {
            krawl.deathCheck();
            currentState = NPCStates.Chase;

            
        }
    }
}
