using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViblovaDamage : MonoBehaviour
{
    public GameObject attackParticle;
    public bool hitOnce = false;
    public float damage;
    public float dmgMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        dmgMultiplier = Random.Range(1f, 1.6f);
        float newMultiplier = Random.Range(1f, 1.6f);
        if (newMultiplier > dmgMultiplier)
        {
            dmgMultiplier = newMultiplier;
        }
        damage *= dmgMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

    }


    
    private void OnTriggerEnter(Collider other)
    {

       
            

            if (other.gameObject.layer == 8 && other.gameObject.transform.root.GetComponent<SpectrobeController>().iframe == false && !hitOnce)
            {
                hitOnce = true;
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpectrobeController>().Hit(new Vector3(hitDir.x, 1, hitDir.z) * Time.deltaTime, 500, dmg:damage);
                //other.gameObject.transform.root.GetComponent<SpikanControl>().iframe = true;
                Instantiate(attackParticle, this.gameObject.transform.position+(-transform.right*5), Quaternion.identity);
            }
        
    }
}
