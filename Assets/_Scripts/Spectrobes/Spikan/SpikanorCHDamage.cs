using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikanorCHDamage : MonoBehaviour
{
    public GameObject attackParticle;
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




        if (other.gameObject.layer == 6)
        {
            Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
            other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 9, hitDir.z) * Time.deltaTime, 700, dmg: 999, false);

            Instantiate(attackParticle, new Vector3(transform.position.x, transform.position.y+15, transform.position.z), Quaternion.identity);
          
        }

    }
}
