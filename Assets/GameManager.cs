using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject KrawlSpawner;
    public GameObject SpawnLociArray;
    public List<GameObject> spawnLoci = new List<GameObject>();
    public float score = 0;
    public float ch = 0;
    public float ev = 0;
    public float health;
    public float healthStore;
    public bool lost = false;
    public UIBarScript chBar;
    public UIBarScript healthBar;
    public UIBarScript evBar;
    public GameObject pointCounter;
    public GameObject[] KrawlList;
    public List<GameObject> currentKrawl = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        health = 275;
        for (int i = 0; i < SpawnLociArray.transform.childCount; i++)
        {
            
            spawnLoci.Add(SpawnLociArray.transform.GetChild(i).gameObject);
        }

        spawnKrawl();
        spawnKrawl();
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
        pointCounter.GetComponent<Text>().text = score.ToString();
    }

    public void spawnKrawl()
    {
      int i = 0;
        if (score <= 50)
        {
            if (currentKrawl.Count < 2)
            {
                i = Random.Range(0, 2);
                summon(i);
            }
        }
        else if (score <= 80)
        {
            i = Random.Range(0, 3);
            summon(i);
            if (currentKrawl.Count < 3)
            {
                i = Random.Range(0, 3);
                summon(i);
            }
        }
        else if (score <= 130)
        {
            if (currentKrawl.Count < 2)
            {
                i = Random.Range(1, 3);
                summon(i);
            }
        }
        else if (score <= 160)
        {
            i = Random.Range(0, 3);
            summon(i);
            if (currentKrawl.Count < 3)
            {
                i = Random.Range(0, 3);
                summon(i);
            }
        }
        else if (score <= 230)
        {
            if (currentKrawl.Count < 2)
            {
                i = Random.Range(3, 5);
                summon(i);
            }
        }
        else if (score <= 290)
        {
            i = Random.Range(2, 5);
            summon(i);
            if (currentKrawl.Count < 3)
            {
                i = Random.Range(2, 5);
                summon(i);
            }
        }
        else if (score <= 370)
        {
            if (currentKrawl.Count < 2)
            {
                i = Random.Range(4, 8);
                summon(i);
            }
        }
        else if (score <= 450)
        {
            i = Random.Range(3, 8);
            summon(i);
            if (currentKrawl.Count < 3)
            {
                i = Random.Range(3, 8);
                summon(i);
            }
        }
    }

    public void summon(int i)
    {
        int random = Random.Range(0, spawnLoci.Count);
        var var = Instantiate(KrawlSpawner, spawnLoci[random].transform.position, Quaternion.Euler(90, 0, 0));
        var.GetComponent<KrawlSpawner>().krawl = KrawlList[i];
        
    }


}
