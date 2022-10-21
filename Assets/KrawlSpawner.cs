using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrawlSpawner : MonoBehaviour
{
    CharacterController character;
    SkinnedMeshRenderer render;
    public GameObject swar;
    bool called;
    // Start is called before the first frame update
    void Start()
    {
        render = transform.Find("KrawlSpawnCloud").Find("E024").GetComponent<SkinnedMeshRenderer>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!character.isGrounded)
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
                Instantiate(swar, transform.position, Quaternion.identity);
            }
        }
    }


}