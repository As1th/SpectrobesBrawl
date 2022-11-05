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
    // Start is called before the first frame update
    void Start()
    {
        scripts = GameObject.Find("Scripts");
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        impact = GetComponent<ImpactReceiver>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
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
