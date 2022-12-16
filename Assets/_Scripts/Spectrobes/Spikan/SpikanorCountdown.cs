using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikanorCountdown : MonoBehaviour
{
    public GameObject scripts;
    public float count;
    public GameObject spikan;
    public ParticleSystem evolvePart;
    GameManager gm;
    SpectrobeController spectrobeController;
    // Start is called before the first frame update
    void Start()
    {
        spectrobeController = GetComponent<SpectrobeController>();
        scripts = spectrobeController.scripts;
        gm = scripts.GetComponent<GameManager>();
        count = 2000;
        if (scripts.GetComponent<Menu>().introScene)
        {
            count = 1400;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gm.health = gm.healthStore;
        gm.ev = count / 2000 * 400;


        if (count > 0)
        { count--; }
        else {
            var eff = Instantiate(evolvePart, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.identity);
            var var = Instantiate(spikan, transform.position, transform.rotation);
            SpectrobeController controller = var.GetComponent<SpectrobeController>();
            controller.scripts = spectrobeController.scripts;
            controller.evolved = false;
            controller.enabled = true;
            gm.player = var;
            if (scripts.GetComponent<Menu>().introScene)
            {
                controller.EVCost = 0;
                controller.CHCost = 0;
                Camera.main.transform.parent.transform.parent = var.transform;
            }
            else
            {
                Camera.main.transform.parent = var.transform;

            }
            eff.transform.parent = var.transform;
           
            foreach (GameObject k in gm.currentKrawl)
            {
                k.GetComponent<Krawl>().player = var;
            }
            Destroy(this.gameObject);
        }
    }
}
