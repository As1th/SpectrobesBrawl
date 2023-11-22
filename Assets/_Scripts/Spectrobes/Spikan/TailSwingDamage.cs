using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSwingDamage : MonoBehaviour
{
    public float damage;
    public float dmgMultiplier;
    public GameObject attackParticle;
    SpectrobeController controller;
    public bool guaranteedCriticalHit;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.root.gameObject.GetComponent<SpectrobeController>();
        dmgMultiplier = Random.Range(0.8f, 1.2f);
        damage *= dmgMultiplier;
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
                float tempDamage = damage;
                if (guaranteedCriticalHit)
                {
                    tempDamage *= 2;
                    guaranteedCriticalHit = false;
                }
                else
                {
                    int random = Random.Range(0, 10);
                    if (random == 9)
                    {
                        tempDamage *= 2;
                    }
                }
                GetComponent<AudioSource>().Play();
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 17, hitDir.z)*Time.deltaTime, 850, dmg: tempDamage, true);
                other.gameObject.transform.root.GetComponent<Krawl>().iframe = true;
                Instantiate(attackParticle, transform.position, Quaternion.identity);
            }
        }
    }
}
