using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    Grid grid;
    GameObject player;
    bool done = false;
    List<Node> nodes;
    
    public GameObject krawlVortex;
    public GameObject powerup;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<UpdateGrid>().AStar;
        player = GetComponent<GameManager>().player;
    }

    // Update is called once per frame
    void Update()
    {
       if(!done)
        {
            nodes = grid.GetLargestWhiteArea();
            SpawnPlayer();
            SpawnKrawlVortexes();

            SpawnPowerups();


            done = true;
        } 
    }

    private void LateUpdate()
    {
        if(done && !player.GetComponent<Animator>().enabled) 
        {
             player.GetComponent<Animator>().enabled = true;
             player.GetComponent<SpectrobeController>().enabled = true;
        }
    }

    public void SpawnPlayer()
    {
     

        player.transform.position = grid.GetMostCentralNode(nodes).worldPosition;

        
    }

    public void SpawnKrawlVortexes()
    {

        List<Node> points = grid.GetMaxSeparationFlushBorderNodes(nodes,  4);
        //print(points.Count);
        foreach (Node node in points)
        {
           GetComponent<GameManager>().spawnLoci.Add( Instantiate(krawlVortex, node.worldPosition, Quaternion.identity));
        }

    }

    public void SpawnPowerups()
    {
        List<Node> points = grid.GetEvenlySpacedNodes(nodes, player.transform.position, 1000, 500,7);
        //print(points.Count);
        foreach (Node node in points)
        {
            var p = Instantiate(powerup, node.worldPosition, Quaternion.identity);
            p.GetComponent<PowerupSpawner>().gm = this.gameObject.GetComponent<GameManager>();
           
            GetComponent<GameManager>().powerupLoci.Add( p );

        }
    }
}
