using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float addHealth;
    float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        multiplier = Random.Range(0.9f, 1.2f);
     
        addHealth *= multiplier;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {




        if (other.gameObject.layer == 11)
        {

            if (other.gameObject.transform.root.GetComponent<SpectrobeController>().gm.health < 150)
            { other.gameObject.transform.root.GetComponent<SpectrobeController>().gm.health += (addHealth*1.1f); }
            else
            {
                other.gameObject.transform.root.GetComponent<SpectrobeController>().gm.health += addHealth;
            }

            other.gameObject.transform.root.GetComponent<SpectrobeController>().gm.currentPowerups.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

    }
}
