using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekerMoveTets : MonoBehaviour
{
    public Pathfinding pathfinding;
    List<Node> path = new List<Node>();
    public float moveSpeed;
    public CharacterController controller;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        path = pathfinding.FindPath(this.transform.position, player.transform.position);
        var target = new Vector3 (path[0].worldPosition.x, this.transform.position.y, path[0].worldPosition.z);
         
        transform.LookAt(target);
        controller.SimpleMove(((target - transform.position).normalized)*moveSpeed);
    }



    void OnDrawGizmos() // draw path in debug view
    {
      // Gizmos.DrawWireCube(transform.position, new Vector3(pathfinding.grid.gridWorldSize.x, 1, pathfinding.grid.gridWorldSize.y));

        if (pathfinding.grid.nodeGrid != null)
        {
            foreach (Node n in pathfinding.grid.nodeGrid)
            {
                //Gizmos.color = (n.walkable) ? Color.clear : Color.clear;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(n.worldPosition, Vector3.one * (pathfinding.grid.nodeDiameter - .1f));
            }
        }
    }


}
