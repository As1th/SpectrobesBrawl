using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    Grid grid;
    GameObject player;
    bool done = false;
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

            SpawnPlayer();
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
        List<Node> nodes = grid.GetLargestWhiteArea();
       
        player.transform.position = nodes[5].worldPosition;
       
        done = true;
    }
}
