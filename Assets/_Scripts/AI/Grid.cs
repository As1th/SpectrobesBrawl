using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour 
{
	public LayerMask unwalkableMask;
    public LayerMask powerupMask;
    public LayerMask playerMask;
    public LayerMask krawlMask;
    public Vector2 gridWorldSize;
    public enum tileStates
    {
        free,
        unwalkable,
        player,
        powerup,
        krawl
    }

    public float nodeRadius;
	public Node[,] nodeGrid;
	public float nodeDiameter;
	public int gridSizeX, gridSizeY;
	
	//public List<Node> path;

	void Awake()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid();
	}

	public void CreateGrid() // creates the grid of nodes
	{
		nodeGrid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        bool walkable;
        tileStates currentState;
        for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                currentState = tileStates.free;
                walkable = true;

                if (Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: powerupMask))
                {
                    currentState = tileStates.powerup;
                }
                if (Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: krawlMask))
                {
                    currentState = tileStates.krawl;
                }
                if (Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: playerMask))
				{
                    currentState = tileStates.player;
					
                }
                if ((Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: unwalkableMask)))
                {
					currentState = tileStates.unwalkable;
					walkable = false;
				}
				
               
				nodeGrid[x, y] = new Node(walkable, worldPoint, x, y, currentState);
			}
		}
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) // selects the closest node using a given world position
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return nodeGrid[x, y];
	}

	
}
