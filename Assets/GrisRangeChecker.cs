using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrisRangeChecker : MonoBehaviour
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
            transform.root.gameObject.GetComponent<GrisController>().inRange = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.root.CompareTag("Player"))
        {
            transform.root.gameObject.GetComponent<GrisController>().inRange = false;
        }
    }
}
