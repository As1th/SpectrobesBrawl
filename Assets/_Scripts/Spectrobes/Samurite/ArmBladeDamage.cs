using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBladeDamage : MonoBehaviour
{
    public float damage;
    public GameObject attackParticle;
    SpectrobeController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.root.gameObject.GetComponent<SpectrobeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (controller.isAttacking)
        {
            
            if (other.gameObject.layer == 6 && other.gameObject.transform.root.GetComponent<Krawl>().iframe == false)
            {
                GetComponent<AudioSource>().Play();
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 14, hitDir.z)*Time.deltaTime, 900, dmg:damage, true);
                other.gameObject.transform.root.GetComponent<Krawl>().iframe = true;
                Instantiate(attackParticle, other.gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
