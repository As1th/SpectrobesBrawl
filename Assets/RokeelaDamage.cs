using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RokeelaDamage : MonoBehaviour
{
    public GameObject attackParticle;
    
    public bool hitOnce = false;
    public float hitHeight=1600;
    public float knockback = 670;
    public float dmg=10;
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

        if (transform.root.gameObject.GetComponent<RokeelaController>().isAttacking)
        {

            if (other.gameObject.layer == 8 && other.gameObject.transform.root.GetComponent<SpikanControl>().iframe == false && !hitOnce)
            {
                hitOnce = true;
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpikanControl>().Hit(new Vector3(hitDir.x, 16, hitDir.z) * Time.deltaTime, knockback, dmg:dmg);
                //other.gameObject.transform.root.GetComponent<SpikanControl>().iframe = true;
                Instantiate(attackParticle, this.gameObject.transform.position+(-transform.right*5), Quaternion.identity);
            }
        }
    }
}
