using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreebagRangeChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            transform.root.gameObject.GetComponent<CreebagController>().inRange = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.root.CompareTag("Player"))
        {
            print("sdsd");
            transform.root.gameObject.GetComponent<CreebagController>().inRange = false;
        }
    }
}
