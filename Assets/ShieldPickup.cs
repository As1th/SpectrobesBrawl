using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
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




        if (other.gameObject.layer == 11)
        {




            other.gameObject.transform.root.GetComponent<SpectrobeController>().shield = true;
            


            Destroy(this.gameObject);
        }

    }
}
