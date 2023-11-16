using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateGrid : MonoBehaviour
{
    public Grid AStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AStar != null)
        {
            AStar.CreateGrid();
        }
    }

    void OnDrawGizmos() // draw path in debug view
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(AStar.gridWorldSize.x, 1, AStar.gridWorldSize.y));

        if (AStar.nodeGrid != null)
        {
            foreach (Node n in AStar.nodeGrid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                //if (path != null)
                //    if (path.Contains(n))
                //        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (AStar.nodeDiameter - .1f));
            }
        }
    }
}
