using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekerMoveTets : MonoBehaviour
{
    public Pathfinding pathfinding;
    public List<Node> path = new List<Node>();
    public float moveSpeed;
    public CharacterController controller;
    public GameObject player;
    Vector3 target;    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        path = pathfinding.FindPath(this.transform.position, player.transform.position);
        if (path.Count > 0)
        {
            target = new Vector3(path[0].worldPosition.x, this.transform.position.y, path[0].worldPosition.z);
        }
         
        transform.LookAt(target);
        controller.SimpleMove(((target - transform.position).normalized)*moveSpeed);
    }








    }
