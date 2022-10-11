using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarHeadbuttDamage : MonoBehaviour
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

        if (transform.root.gameObject.GetComponent<SwarController>().isAttacking)
        {

            if (other.gameObject.layer == 8 && other.gameObject.transform.root.GetComponent<SpikanControl>().iframe == false)
            {

                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpikanControl>().Hit(new Vector3(hitDir.x, 12, hitDir.z) * Time.deltaTime, 800, 800);
                other.gameObject.transform.root.GetComponent<SpikanControl>().iframe = true;
                Instantiate(attackParticle, this.gameObject.transform.position+(-transform.right*5), Quaternion.identity);
            }
        }
    }
}
