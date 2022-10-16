using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject KrawlSpawner;
    public GameObject SpawnLociArray;
    public List<GameObject> spawnLoci = new List<GameObject>();
    public float score = 0;
    public float ch = 0;
    public float ev = 0;
    public bool lost = false;
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
        if (ch > 50)
        { ch = 50; }
    }

    public void spawnKrawl()
    {
        int random = Random.Range(0, spawnLoci.Count-1);
        Instantiate(KrawlSpawner, spawnLoci[random].transform.position, Quaternion.Euler(90,0,0));
    }
}
