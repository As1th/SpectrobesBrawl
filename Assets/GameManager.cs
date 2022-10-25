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
    public float health;
    public bool lost = false;
    public UIBarScript chBar;
    public UIBarScript healthBar;
    public UIBarScript evBar;
    // Start is called before the first frame update
    void Start()
    {
        health = 275;
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

        if (health < 0)
        {
            health = 0;
        }

        if (ev > 200)
        {
            ev = 200;
        }
        evBar.UpdateValue((int)ev, 200);
        chBar.UpdateValue((int)ch,50);
        healthBar.UpdateValue((int)health,275);
    }

    public void spawnKrawl()
    {
        int random = Random.Range(0, spawnLoci.Count-1);
        Instantiate(KrawlSpawner, spawnLoci[random].transform.position, Quaternion.Euler(90,0,0));
    }
}
