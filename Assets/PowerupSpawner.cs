using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameManager gm;
    public bool doneFirst;
    public GameObject currentPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!doneFirst)
        {
            int random = Random.Range(0, gm.PowerupsList.Count);
            var p = Instantiate(gm.PowerupsList[random], transform.position, Quaternion.identity);
            gm.currentPowerups.Add(p);
            currentPower = p;
            doneFirst = true;
        } else
        {
            if (gm.currentPowerups.Count <= 4 && currentPower == null)
            {
                if (Mathf.Abs(gm.player.transform.position.x - transform.position.x) > 10 && Mathf.Abs(gm.player.transform.position.z - transform.position.z) > 10)
                {
                    int random = Random.Range(0, gm.PowerupsList.Count);
                    var p = Instantiate(gm.PowerupsList[random], transform.position, Quaternion.identity);
                    gm.currentPowerups.Add(p);
                    currentPower = p;
                }
            }

        }
    }
}
