using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikanCH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ImpactReceiverInfinite>().AddImpact(transform.forward, 250);
    }

    // Update is called once per frame
    void Update()
    {
      
        Vector3 vel = (GetComponent<CharacterController>().velocity);
        Vector3 inv = transform.InverseTransformVector(vel);
        //GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        // transform.position = transform.position + transform.forward;
        print(inv);
        if (inv.z < 0.1f) {
           // Destroy(this.gameObject);
        }
        //as well as this one
       

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
