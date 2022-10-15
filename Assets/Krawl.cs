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
    GameObject player;
    public GameObject cloud;
    public float speed = 27f;
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
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 90.81f * Time.deltaTime);
        }

        if (!stagger && !GetComponent<SwarController>().isAttacking)
        {
            transform.LookAt(player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Vector3 dir = (player.transform.position - transform.position).normalized;
            controller.Move(new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime);



        }
    }

    public void deathCheck()
    {
        if (health <= 0)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            scripts.GetComponent<GameManager>().spawnKrawl();
        }
    }

    public void Hit(Vector3 dir, float force, float dmg)
    {
            GetComponent<SwarController>().deactivateHurtBox();
            stagger = true;
            impact.AddImpact(dir, force);
            transform.rotation = Quaternion.LookRotation(new Vector3(-dir.x, 0, -dir.z));
           
            health -= dmg;
            animator.SetTrigger("Hit");
    }
}
