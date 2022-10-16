using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHParticleDamage : MonoBehaviour
{
    public GameObject attackParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {




        if (other.gameObject.layer == 6)
        {
            Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
            other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 8, hitDir.z) * Time.deltaTime, 200, dmg:10, false);

            Instantiate(attackParticle, transform.position, Quaternion.identity);
            Destroy(transform.root.gameObject);
        }
        
    }
}
