using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Grid : MonoBehaviour 
{
	public LayerMask unwalkableMask;
    public LayerMask powerupMask;
    public LayerMask playerMask;
    public LayerMask krawlMask;
    public LayerMask vortexMask;

    public Vector2 gridWorldSize;
    
    public enum tileStates
    {
        free,
        unwalkable,
        player,
        powerup,
        krawl,
        vortex
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
               
                if (Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: vortexMask))
                {
                    currentState = tileStates.vortex;

                }
                if (Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: krawlMask))
                {
                    currentState = tileStates.krawl;
                }
                if ((Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: unwalkableMask)))
                {
					currentState = tileStates.unwalkable;
					walkable = false;
				}
                if (Physics.CheckCapsule(worldPoint, new Vector3(worldPoint.x, worldPoint.y - 350, worldPoint.z), nodeRadius, layerMask: playerMask))
                {
                    currentState = tileStates.player;
                    walkable = true;
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

    public Node GetMostCentralNode(List<Node> nodes)
    {
        if (nodes.Count == 0)
        {
            Debug.LogError("Node list is empty.");
            return null;
        }

        // Calculate the average position
        Vector3 averagePosition = Vector3.zero;
        foreach (Node node in nodes)
        {
            averagePosition += node.worldPosition;
        }
        averagePosition /= nodes.Count;

        // Find the node closest to the average position
        Node mostCentralNode = nodes[0];
        float closestDistance = Vector3.Distance(averagePosition, mostCentralNode.worldPosition);

        for (int i = 1; i < nodes.Count; i++)
        {
            float distance = Vector3.Distance(averagePosition, nodes[i].worldPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                mostCentralNode = nodes[i];
            }
        }

        return mostCentralNode;
    }

    public List<Node> GetEvenlySpacedNodes(List<Node> nodes, Vector3 center, float radius, float initialMinRadius, int numberOfNodes)
    {
        List<Node> evenlySpacedNodes = new List<Node>();

        if (numberOfNodes <= 0)
        {
            Debug.LogWarning("Number of nodes must be greater than zero.");
            return evenlySpacedNodes;
        }

        float minRadius = initialMinRadius;

        while (minRadius <= radius)
        {
            // Filter nodes within the specified radius and outside the minimum radius
            List<Node> nodesWithinRadius = nodes.FindAll(node =>
            {
                float distance = Vector3.Distance(node.worldPosition, center);
                return distance <= radius && distance >= minRadius;
            });

            if (nodesWithinRadius.Count >= numberOfNodes)
            {
                // Distribute nodes evenly in a circular pattern
                float angleIncrement = 360f / numberOfNodes;

                for (int i = 0; i < numberOfNodes; i++)
                {
                    float targetAngle = (angleIncrement * i) % 360f;
                    float closestAngle = Mathf.Infinity;
                    Node closestNode = null;

                    foreach (Node node in nodesWithinRadius)
                    {
                        float distanceToCenter = Vector3.Distance(node.worldPosition, center);

                        // Skip nodes within the minimum radius
                        if (distanceToCenter < minRadius)
                        {
                            continue;
                        }

                        float angle = Mathf.Atan2(node.worldPosition.z - center.z, node.worldPosition.x - center.x) * Mathf.Rad2Deg;

                        // Ensure the angle is in the [0, 360) range
                        angle = (angle + 360f) % 360f;

                        float angleDifference = Mathf.Abs(targetAngle - angle);

                        if (angleDifference < closestAngle)
                        {
                            closestAngle = angleDifference;
                            closestNode = node;
                        }
                    }

                    if (closestNode != null)
                    {
                        evenlySpacedNodes.Add(closestNode);
                        nodesWithinRadius.Remove(closestNode);
                    }
                }

                if (evenlySpacedNodes.Count >= numberOfNodes)
                {
                    break; // Stop if enough nodes are found
                }
            }

            // Reduce the minimum radius and try again
            minRadius += 1f; // Adjust this value based on your needs
        }

        return evenlySpacedNodes;
    }

    public List<Node> GetMaxSeparationFlushBorderNodes(List<Node> largestWhiteArea, int numberOfNodes)
    {
        List<Node> evenlySpacedNodes = new List<Node>();

        if (numberOfNodes <= 0)
        {
            Debug.LogWarning("Number of nodes must be greater than zero.");
            return evenlySpacedNodes;
        }

        // If there are not enough nodes in the largestWhiteArea, return an empty list
        if (largestWhiteArea.Count < numberOfNodes)
        {
            Debug.LogWarning("Not enough nodes in the largestWhiteArea.");
            return evenlySpacedNodes;
        }

        // Sort nodes based on their coordinates to find the outermost border
        largestWhiteArea.Sort((a, b) =>
        {
            if (a.gridX == b.gridX)
                return a.gridY.CompareTo(b.gridY);
            return a.gridX.CompareTo(b.gridX);
        });

        int n = largestWhiteArea.Count;

        // Calculate the maximum possible separation
        int maxSeparation = Mathf.FloorToInt(n / (float)numberOfNodes);

        // Calculate the remaining space after placing the nodes
        int remainingSpace = n % numberOfNodes;

        // Initialize with the maximum possible separation
        int separation = maxSeparation;

        // Iterate until a valid set of nodes is found
        while (separation > 0)
        {
            evenlySpacedNodes.Clear();

            // Traverse the outermost border and select evenly spaced nodes with separation
            for (int i = 0; i < n; i += separation)
            {
                evenlySpacedNodes.Add(largestWhiteArea[i]);
            }

            // Distribute the remaining space by adding one more node at a time
            for (int i = 0; i < remainingSpace; i++)
            {
                evenlySpacedNodes.Add(largestWhiteArea[i * (maxSeparation + 1)]);
            }

            // If the number of selected nodes is equal to or greater than the required number, break the loop
            if (evenlySpacedNodes.Count >= numberOfNodes)
            {
                break;
            }

            // Decrease the separation and try again
            separation--;
        }

        // Trim the result list to the desired number of nodes
        evenlySpacedNodes = evenlySpacedNodes.Take(numberOfNodes).ToList();

        return evenlySpacedNodes;
    }

}
