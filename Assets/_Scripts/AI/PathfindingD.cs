using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class PathfindingD : MonoBehaviour
{

    // public Transform seeker, target; // references to the seeker and target GameObjects
    public Grid grid; // reference to the grid
    public GameManager gm;
    void Awake()
    {


    }

    void LateUpdate()
    {
        // target = gm.player.transform;

        //  FindPath(seeker.position, target.position);
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        var timer = System.Diagnostics.Stopwatch.StartNew();
        //grid = GetComponent<Grid>();
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        Node currentNode = null;

        while (openSet.Count > 0)
        {
            currentNode = openSet.First();
            foreach (Node n in openSet)
            {
                if (n.gCost < currentNode.gCost)
                {
                    currentNode = n;
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, currentNode);
            }

            List<Node> currentNeighbours = GetNeighbours(currentNode);
            foreach (Node neighbour in currentNeighbours)
            {
                if (closedSet.Contains(neighbour) || !neighbour.walkable)
                {
                    continue;
                }

                int gCost = currentNode.gCost + GetDistance(currentNode, neighbour);

                if (gCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = gCost;
                    neighbour.hCost = 0; // In Dijkstra's, hCost is not needed
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        timer.Stop();
        long nanosecondsPerTick = (1000L * 1000L * 1000L) / System.Diagnostics.Stopwatch.Frequency;
        long numberOfTicks = timer.ElapsedTicks;
        long nanoseconds = numberOfTicks * nanosecondsPerTick;
        Debug.Log(string.Format("Dijkstra's Search from {0} to {1} took {2} nanoseconds to complete.", startPos.ToString(), targetPos.ToString(), nanoseconds.ToString()));
        return RetracePath(startNode, currentNode);
    }
    public List<Node> RetracePath(Node startNode, Node endNode) // retraces the path by using the parent property stored in each node, saves this path in a list and passes it to the grid class to be handled
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
        //path = path;

    }

    public List<Node> GetNeighbours(Node node) // returns a list of all the nearest neighbours of a given node
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < grid.gridSizeX && checkY >= 0 && checkY < grid.gridSizeY)
                {
                    neighbours.Add(grid.nodeGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    int GetDistance(Node nodeA, Node nodeB) // returns the distance between two given nodes
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
