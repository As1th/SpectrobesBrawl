using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static AIKrawlController;

public class VortexController : MonoBehaviour
{
    public float health;
    public float detectRange;
    public GameObject cloud;
    public GameManager gm;
    public GameObject defendSpawn;
    public VortexStates currentState = VortexStates.Wait;
    public enum VortexStates
    {
        Wait,
        Spawn
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Scripts").GetComponent<GameManager>();
    }
    void Update()
    {
        SwitchState();
        deathCheck();
    }
    private void SwitchState()
    {
        switch (currentState)
        {
            case VortexStates.Wait:
                Wait();
                break;
            case VortexStates.Spawn:
                Spawn();
                break;
            
            default:
                Wait();
                break;
        }
    }
    private void deathCheck()
    {
        if (health <= 0)
        {
            gm.spawnLoci.Remove(this.gameObject);
            Destroy(this.gameObject);

            Instantiate(cloud, transform.position, Quaternion.identity);
            gm.AddScore();
            //  gm.ch += 10;
            if (!gm.player.GetComponent<SpectrobeController>().evolved)
            {
                gm.ev += 10;
            }
            gm.winCheck();
        }
    }
    private void Wait()
    {
       
            if (Vector3.Distance(gm.player.transform.position, transform.position) <= detectRange)
            {
                currentState = VortexStates.Spawn;
            }
        
       

    }

    private void Spawn()
    {
        if (Vector3.Distance(gm.player.transform.position, transform.position) > detectRange)
        {
            currentState = VortexStates.Wait;
        }
        if (defendSpawn == null)
        {
            defendSpawn = Instantiate(gm.KrawlList[gm.maxKrawl], transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1)
        {
            health -= other.gameObject.GetComponent<CHParticleDamage>().damage;
            Destroy(other.gameObject);

        }
        else if (other.gameObject.layer == 9)
        {
            health -= other.gameObject.GetComponent<TailSwingDamage>().damage;
        }
    }

}
