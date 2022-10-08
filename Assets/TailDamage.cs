using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailDamage : MonoBehaviour
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
        if (transform.root.gameObject.GetComponent<SpikanControl>().isAttacking)
        {
            if (other.gameObject.layer == 6 && other.gameObject.transform.root.GetComponent<Krawl>().iframe == false)
            {
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 20, hitDir.z)*Time.deltaTime, 815, 815);
                other.gameObject.transform.root.GetComponent<Krawl>().iframe = true;
            }
        }
    }
}
