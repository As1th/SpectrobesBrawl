using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAirProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public GameObject attackParticle;
    public GameObject reverseAttackParticle;
    public bool dead;
    public GameObject scripts;
    public AudioSource blocksound;
    public GameObject defensePoof;
    public bool playEffectOnCollision = false;
    public float mainDamage;
    public float deflectedDamage;
    public float dmgMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        blocksound = GameObject.Find("BlockSound").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        scripts = GameObject.Find("Scripts");
        dmgMultiplier = Random.Range(0.7f, 1.1f);
        mainDamage *= dmgMultiplier;
        deflectedDamage *= dmgMultiplier;
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
            blocksound.Play();
            this.gameObject.transform.Rotate(new Vector3(1, 0, 0), -180);
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 7000);
            dead = true;
            Instantiate(defensePoof, transform.position, Quaternion.identity);
            scripts.GetComponent<GameManager>().ch += 1;
            if (!other.gameObject.transform.root.GetComponent<SpectrobeController>().evolved)
            {
                scripts.GetComponent<GameManager>().ev += 1;
            }
            this.gameObject.layer = 1;
            return;

        }
        else if (other.gameObject.layer == 8 && !dead)
        {
            if (other.gameObject.transform.root.GetComponent<SpectrobeController>().iframe == false)
            {
                Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
                other.gameObject.transform.root.GetComponent<SpectrobeController>().Hit(new Vector3(hitDir.x, 7, hitDir.z) * Time.deltaTime, 100, dmg: deflectedDamage);
                if (!playEffectOnCollision)
                {
                    Instantiate(attackParticle, this.gameObject.transform.position + (transform.forward * 10), Quaternion.identity);
                }
            }

            if (other.gameObject.transform.root.GetComponent<SpectrobeController>().iframe == true)
            { return; }
        }
        if (other.gameObject.layer == 6 && dead)
        {
            Instantiate(reverseAttackParticle, this.gameObject.transform.position + (transform.forward * 10), Quaternion.identity);
            Vector3 hitDir = (other.gameObject.transform.root.position - transform.root.position);
            other.gameObject.transform.root.GetComponent<Krawl>().Hit(new Vector3(hitDir.x, 7, hitDir.z) * Time.deltaTime, force:300, dmg: mainDamage, true);
            scripts.GetComponent<GameManager>().ch += 5;
            if (!(other.gameObject.transform.root.GetComponent<Krawl>().player.GetComponent<SpectrobeController>().evolved))
            {
                scripts.GetComponent<GameManager>().ev += 5;
            }
            Destroy(this.gameObject);
        }
        if (!dead)
        {
            if(playEffectOnCollision)
            {
                Instantiate(attackParticle, this.gameObject.transform.position + (transform.forward * 10), Quaternion.identity);

            }
            Destroy(this.gameObject);
        }
    }
}
