using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAirProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public GameObject attackParticle;
    public bool dead;
    public GameObject scripts;
    public GameObject defensePoof;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
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
        if (other.gameObject.layer == 9 && !dead)
        {
            this.gameObject.transform.Rotate(new Vector3(1, 0, 0), -145);
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 7000);
            dead = true;
            Instantiate(defensePoof, transform.position, Quaternion.identity);
            scripts.GetComponent<GameManager>().ch += 5;
            scripts.GetComponent<GameManager>().ev += 5;
            return;

        }
        else if (other.gameObject.layer == 8 && !dead)
        {
            if (other.gameObject.transform.root.GetComponent<SpikanControl>().iframe == false)
            {
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpikanControl>().Hit(new Vector3(hitDir.x, 7, hitDir.z) * Time.deltaTime, 300, dmg: 5);
                Instantiate(attackParticle, this.gameObject.transform.position + (transform.forward * 10), Quaternion.identity);
            }

            if (other.gameObject.transform.root.GetComponent<SpikanControl>().iframe == true)
            { return; }
        }
        if (!dead)
        {
            Destroy(this.gameObject);
        }
    }
}