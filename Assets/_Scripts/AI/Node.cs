using UnityEngine;
using System.Collections;

public class Node
{
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;
	public Grid.tileStates currentState;
	public int gCost; // gCost is distance from starting node
	public int hCost; // hCost is distance from end node
	public Node parent;
    public bool visited;
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, Grid.tileStates currentState)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        this.currentState = currentState;
    }

    public int fCost // fCost is gCost + hCost
	{
		get
		{
			return gCost + hCost;
		}
	}
}
