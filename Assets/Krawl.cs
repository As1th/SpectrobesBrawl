using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krawl : MonoBehaviour
{
    public bool iframe = false;
    public bool stagger = false;
    private CharacterController controller;
    private Animator animator;
    private ImpactReceiver impact;
    public GameObject player;
    public GameObject cloud;
    //public float speed = 27f;
    public float health = 50f;
    public GameObject scripts;
    public bool touch;
    // Start is called before the first frame update
    void Start()
    {
        scripts = GameObject.Find("Scripts");
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        impact = GetComponent<ImpactReceiver>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    // Update is called once per frame
    void Update()
    {
        if (touch)
        {
            gameObject.GetComponent<CharacterController>().SimpleMove(transform.right * 80);
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
            scripts.GetComponent<GameManager>().AddScore();
            //  scripts.GetComponent<GameManager>().ch += 10;
            if (!player.GetComponent<SpikanControl>().evolved)
            {
                scripts.GetComponent<GameManager>().ev += 10;
            }
           scripts.GetComponent<GameManager>().currentKrawl.Remove(this.gameObject);
           scripts.GetComponent<GameManager>().spawnKrawl();

            Destroy(this.gameObject);
            


           
        }
    }

    public void Hit(Vector3 dir, float force, float dmg, bool giveCHXP)
    {
            if (!player.GetComponent<SpikanControl>().evolved)
            {
                scripts.GetComponent<GameManager>().ev += 10;
            }
            if (giveCHXP)
            {
                scripts.GetComponent<GameManager>().ch += 10;
            }
        

        
            stagger = true;
            impact.AddImpact(dir, force);
            transform.rotation = Quaternion.LookRotation(new Vector3(-dir.x, 0, -dir.z));
            health -= dmg;
            animator.SetTrigger("Hit");
    }
}
