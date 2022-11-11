using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZepiDamage : MonoBehaviour
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

        if (transform.root.gameObject.GetComponent<ZepiController>().isAttacking)
        {

            if (other.gameObject.layer == 8 && other.gameObject.transform.root.GetComponent<SpikanControl>().iframe == false && !hitOnce)
            {
                hitOnce = true;
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpikanControl>().Hit(new Vector3(hitDir.x, 2, hitDir.z) * Time.deltaTime, 400, dmg:3);
                //other.gameObject.transform.root.GetComponent<SpikanControl>().iframe = true;
                Instantiate(attackParticle, this.gameObject.transform.position+(-transform.right*5), Quaternion.identity);
            }
        }
    }
}
