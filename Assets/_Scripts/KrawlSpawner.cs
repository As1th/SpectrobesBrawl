using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrawlSpawner : MonoBehaviour
{
    CharacterController character;
    SkinnedMeshRenderer render;
    public GameObject krawl;
    bool called;
    public GameObject scripts;
    // Start is called before the first frame update
    void Start()
    {
        render = transform.Find("KrawlSpawnCloud").Find("E024").GetComponent<SkinnedMeshRenderer>();
        character = GetComponent<CharacterController>();
        scripts = GameObject.Find("Scripts");
    }

    // Update is called once per frame
    void Update()
    {
     //   if (!character.isGrounded)
     if(false)
        {
            character.Move(Vector3.down / 3);
        }
        else {
            if (!called)
            {

                called = true;
                render.enabled = true;
                character.enabled = false;
                GetComponent<ParticleSystem>().Play();
                render.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("Spawn");
                GetComponent<CFX_AutoDestructShuriken>().enabled = true;
                var var = Instantiate(krawl, transform.position, Quaternion.identity);
                scripts.GetComponent<GameManager>().currentKrawl.Add(var);
            }
        }
    }


}
