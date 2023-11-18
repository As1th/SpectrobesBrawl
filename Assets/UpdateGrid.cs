using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateGrid : MonoBehaviour
{
    public Grid AStar;
    GameManager gm;
    public SpawnPoints spawnObjects;
    // Start is called before the first frame update
    void Start()
    {
        gm = this.GetComponent<GameManager>();
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
                switch (n.currentState)  
                {
                    case Grid.tileStates.player:
                        Gizmos.color = Color.green;
                        break;
                    case Grid.tileStates.krawl:
                        Gizmos.color = Color.magenta;
                        break;
                    case Grid.tileStates.powerup:
                        Gizmos.color = Color.cyan;
                        break;


                }


                Gizmos.DrawCube(n.worldPosition, Vector3.one * (AStar.nodeDiameter - .1f));
            }
            foreach (GameObject krawl in gm.currentKrawl)
            {
                AIKrawlController seekerMove = krawl.GetComponent<AIKrawlController>();

                if (seekerMove != null && seekerMove.path != null && seekerMove.path.Count > 0)
                {
                    DrawPathGizmo(seekerMove.path, 20f); // Adjust the thickness as needed
                }
            }

        }
    }


    void DrawPathGizmo(List<Node> path, float thickness)
    {
        Gizmos.color = Color.black;

        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 start = path[i].worldPosition;
            Vector3 end = path[i + 1].worldPosition;

            // Calculate the direction and length of the line segment
            Vector3 direction = end - start;
            float length = direction.magnitude;

            // Normalize the direction
            direction.Normalize();

            // Calculate orthogonal direction for the thickness
            Vector3 orthogonalDirection = new Vector3(-direction.z, 0, direction.x);

            // Calculate half thickness
            float halfThickness = thickness * 0.5f;

            // Calculate points for the four corners of the rectangle
            Vector3 topLeft = start + orthogonalDirection * halfThickness;
            Vector3 topRight = start - orthogonalDirection * halfThickness;
            Vector3 bottomLeft = end + orthogonalDirection * halfThickness;
            Vector3 bottomRight = end - orthogonalDirection * halfThickness;

            // Draw the rectangle by connecting the corners with Gizmos.DrawLine
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}
