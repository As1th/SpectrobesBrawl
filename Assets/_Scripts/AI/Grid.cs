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

    public List<Node> GetLargestWhiteArea()
    {
        List<Node> largestWhiteArea = new List<Node>();
        List<Node> currentWhiteArea = new List<Node>();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Node currentNode = nodeGrid[x, y];

                if (currentNode.currentState != tileStates.unwalkable && !currentNode.visited)
                {
                    currentWhiteArea.Clear();
                    DepthFirstSearch(currentNode, currentWhiteArea);

                    if (currentWhiteArea.Count > largestWhiteArea.Count)
                    {
                        largestWhiteArea.Clear();
                        largestWhiteArea.AddRange(currentWhiteArea);
                    }
                }
            }
        }

        // Mark all nodes as not visited for future use
        ResetVisitedNodes();

        return largestWhiteArea;
    }

    private void DepthFirstSearch(Node startNode, List<Node> whiteArea)
    {
        Stack<Node> stack = new Stack<Node>();
        stack.Push(startNode);

        while (stack.Count > 0)
        {
            Node currentNode = stack.Pop();

            if (!currentNode.visited && currentNode.currentState != tileStates.unwalkable)
            {
                currentNode.visited = true;
                whiteArea.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    stack.Push(neighbor);
                }
            }
        }
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(nodeGrid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    private void ResetVisitedNodes()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                nodeGrid[x, y].visited = false;
            }
        }
    }
}
