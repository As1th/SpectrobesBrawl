using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krawl : MonoBehaviour
{
    public bool iframe = false;
    public bool stagger = false;
    public float iframeCountdown;
    public float staggerCountdown = 0f;
    private CharacterController controller;
    private Animator animator;
    private ImpactReceiver impact;
    public GameObject player;
    public GameObject cloud;
    //public float speed = 27f;
    public float health = 50f;
    public GameObject scripts;
    public bool touch;
    public GameManager gm;
    SpectrobeController playerSpectrobeController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpectrobeController = player.GetComponent<SpectrobeController>();
        scripts = playerSpectrobeController.scripts;
        gm = scripts.GetComponent<GameManager>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        impact = GetComponent<ImpactReceiver>();
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //hitNormal = hit.normal;
        if (hit.gameObject.layer == 10 || hit.gameObject.layer == 10)
        {
            if (IsBAboveA(A: hit.gameObject.transform, B: transform, Ra:hit.gameObject.GetComponent<CharacterController>().radius, Rb:GetComponent<CharacterController>().radius))
            {
                touch = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (iframeCountdown > 0)
        {
            iframeCountdown--;
        }
        else
        {
            iframe = false;
        }
        if (staggerCountdown > 0)
        {
            staggerCountdown--;
        }
        else
        {
            stagger = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (touch)
        {
           controller.SimpleMove(transform.right * 80);
        }
        touch = false;
    }
    public static bool IsBAboveA(Transform A, Transform B, float Ra, float Rb)
    {
        Vector3 Vab = (B.position - A.position) - Vector3.Dot(B.position - A.position, A.up) * A.up;
        if (Vab.magnitude < (Ra + Rb))
        {
            if (Vector3.Dot(B.position - A.position, A.up) > 0)
            {
                return true; // object B is above object A
            }
        }
        return false; // object B is not above object A
    }
    public void deathCheck()
    {
        if (health <= 0)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
            gm.AddScore();
            //  gm.ch += 10;
            if (!playerSpectrobeController.evolved)
            {
                gm.ev += 10;
            }
           gm.currentKrawl.Remove(this.gameObject);
           gm.spawnKrawl();
            foreach (GameObject v in gm.spawnLoci)
            {
               if(v.GetComponent<VortexController>().defendSpawn ==  this.gameObject)
                {
                    v.GetComponent<VortexController>().defendSpawn = null;
                }
            }
            Destroy(this.gameObject);
            


           
        }
    }

    public void Hit(Vector3 dir, float force, float dmg, bool giveCHXP)
    {
        iframeCountdown = 65;
            if (!playerSpectrobeController.evolved)
            {
                gm.ev += 10;
            }
       
     
            if (giveCHXP)
            {
                gm.ch += 10;
            }


        this.gameObject.GetComponent<AIKrawlController>().currentState = AIKrawlController.NPCStates.Stagger;
            stagger = true;
        staggerCountdown = 65;
        impact.AddImpact(dir, force);
            transform.rotation = Quaternion.LookRotation(new Vector3(-dir.x, 0, -dir.z));
            health -= dmg;
            animator.SetTrigger("Hit");
    }
}
