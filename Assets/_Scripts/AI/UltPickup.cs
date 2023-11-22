using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {




        if (other.gameObject.layer == 11 && this.enabled)
        {




            other.gameObject.transform.root.GetComponent<SpectrobeController>().ultimate = true;
            other.gameObject.transform.root.GetComponent<SpectrobeController>().ultimateCountdown = 1500;

            other.gameObject.transform.root.GetComponent<SpectrobeController>().gm.currentPowerups.Remove(this.gameObject);
            foreach (GameObject p in other.gameObject.transform.root.GetComponent<SpectrobeController>().gm.powerupLoci)
            {
                if (p.GetComponent<PowerupSpawner>().currentPower == this.gameObject)
                {
                    p.GetComponent<PowerupSpawner>().currentPower = null;
                }


            }
            Destroy(this.gameObject);
        }
    }
}
