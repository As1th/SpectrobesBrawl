using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryPickup : MonoBehaviour
{
    int random;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0,5);

        switch (random)
        {
            case 0:
                GetComponent<HealthPickup>().enabled = true;
                break;
            case 1:
                GetComponent<ShieldPickup>().enabled = true;
                break;
            case 2:
                GetComponent<CHPickup>().enabled = true;
                break;
            case 3:
                GetComponent<CritPickup>().enabled = true;
                break;
            case 4:
                GetComponent<UltPickup>().enabled = true;
                break;
            default:

                GetComponent<HealthPickup>().enabled = true;
                break;
        }
        this.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

  
}
