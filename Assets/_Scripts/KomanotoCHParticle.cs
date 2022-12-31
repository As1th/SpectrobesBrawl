using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomanotoCHParticle : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public GameObject attackParticle;
    
    public float damage;
    public GameObject scripts;
  
    public bool playEffectOnCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        scripts = GameObject.Find("Scripts");
    }

    // Update is called once per frame
    void Update()
    {
        //controller.SimpleMove(transform.forward * 83);

       
 


       
        //GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        // transform.position = transform.position + transform.forward;

    }



    private void OnTriggerEnter(Collider other)
    {

      
        if (other.gameObject.layer == 6)
        {
           
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 1, hitDir.z) * Time.deltaTime, 150, dmg: damage, false);
                if (!playEffectOnCollision)
                {
                    Instantiate(attackParticle, this.gameObject.transform.position + (transform.forward * 10), Quaternion.identity);
                }
           
            

        }

        if (playEffectOnCollision)
            {
                Instantiate(attackParticle, this.gameObject.transform.position + (transform.forward * 10), Quaternion.identity);

            }
        if (other.gameObject.layer == 3 || gameObject.layer == 16)
        {
            Destroy(this.gameObject);
        }
      
            Destroy(this.gameObject);
        
        
    }
}
