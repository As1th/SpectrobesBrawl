using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikanCH : MonoBehaviour
{
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // GetComponent<ImpactReceiverInfinite>().AddImpact(transform.forward, 250);
      
    }

    // Update is called once per frame
    void Update()
    {
        controller.SimpleMove(transform.forward * 83);

        float speedForward = Vector3.Dot(controller.velocity, transform.forward);
        float speedDown = Vector3.Dot(controller.velocity, -transform.up);


        
      
        if (speedForward < 80 && speedDown == 0)
        {
            Destroy(this.gameObject);
        }

       
        //GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        // transform.position = transform.position + transform.forward;

    }


    
}
