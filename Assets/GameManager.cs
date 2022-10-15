using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject KrawlSpawner;
    public GameObject SpawnLociArray;
    public List<GameObject> spawnLoci = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
     
        for (int i = 0; i < SpawnLociArray.transform.childCount; i++)
        {
            
            spawnLoci.Add(SpawnLociArray.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void spawnKrawl()
    {
        int random = Random.Range(0, spawnLoci.Count-1);
        Instantiate(KrawlSpawner, spawnLoci[random].transform.position, Quaternion.Euler(90,0,0));
    }
}
