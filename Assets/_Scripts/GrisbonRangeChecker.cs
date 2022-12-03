using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrisbonRangeChecker : MonoBehaviour
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
            transform.root.gameObject.GetComponent<GrisbonController>().inRange = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.root.CompareTag("Player"))
        {
            transform.root.gameObject.GetComponent<GrisbonController>().inRange = false;
        }
    }
}
