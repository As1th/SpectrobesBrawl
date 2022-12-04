using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrisDamage : MonoBehaviour
{
    public GameObject attackParticle;
    public bool hitOnce = false;
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

        if (transform.root.gameObject.GetComponent<GrisController>().isAttacking)
        {

            if (other.gameObject.layer == 8 && other.gameObject.transform.root.GetComponent<SpectrobeController>().iframe == false && !hitOnce)
            {
                hitOnce = true;
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpectrobeController>().Hit(new Vector3(hitDir.x, 12, hitDir.z) * Time.deltaTime, 650, dmg:10);
                //other.gameObject.transform.root.GetComponent<SpikanControl>().iframe = true;
                Instantiate(attackParticle, this.gameObject.transform.position+(-transform.right*5), Quaternion.identity);
            }
        }
    }
}
