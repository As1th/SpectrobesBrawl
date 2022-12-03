using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFireDamage : MonoBehaviour
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

        if (transform.root.gameObject.GetComponent<KomanotoControl>().isAttacking)
        {
            
            if (other.gameObject.layer == 6 && other.gameObject.transform.root.GetComponent<Krawl>().iframe == false)
            {
                GetComponent<AudioSource>().Play();
                Instantiate(attackParticle, other.gameObject.transform.position, Quaternion.identity);
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 17, hitDir.z)*Time.deltaTime, 850, dmg:20, true);
                other.gameObject.transform.root.GetComponent<Krawl>().iframe = true;
              
            }
        }
    }
}
