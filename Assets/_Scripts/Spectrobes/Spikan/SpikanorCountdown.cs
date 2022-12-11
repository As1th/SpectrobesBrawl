using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikanorCountdown : MonoBehaviour
{
    public GameObject scripts;
    public float count;
    public GameObject spikan;
    public ParticleSystem evolvePart;
    // Start is called before the first frame update
    void Start()
    {
        scripts =  GetComponent<SpectrobeController>().scripts;
        count = 2000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scripts.GetComponent<GameManager>().health = scripts.GetComponent<GameManager>().healthStore;
        scripts.GetComponent<GameManager>().ev = count / 2000 * 400;


        if (count > 0)
        { count--; }
        else {
            var eff = Instantiate(evolvePart, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.identity);
            var var = Instantiate(spikan, transform.position, transform.rotation);
            var.GetComponent<SpectrobeController>().scripts = GetComponent<SpectrobeController>().scripts;
            var.GetComponent<SpectrobeController>().evolved = false;
            var.GetComponent<SpectrobeController>().enabled = true;
            eff.transform.parent = var.transform;
            Camera.main.transform.parent = var.transform;
            foreach (GameObject k in scripts.GetComponent<GameManager>().currentKrawl)
            {
                k.GetComponent<Krawl>().player = var;
            }
            Destroy(this.gameObject);
        }
    }
}
