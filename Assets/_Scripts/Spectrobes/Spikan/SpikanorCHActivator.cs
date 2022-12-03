using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikanorCHActivator : MonoBehaviour
{
    public GameObject damage1;
    public GameObject damage2;
    public GameObject damage3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(activationPathway());
    }

    void activateDamage1()
    {
        damage1.SetActive(true);
        damage2.SetActive(false);
        damage3.SetActive(false);
    }
    void activateDamage2()
    {
        damage1.SetActive(false);
        damage2.SetActive(true);
        damage3.SetActive(false);
    }
    void activateDamage3()
    {
        damage1.SetActive(false);
        damage2.SetActive(false);
        damage3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator activationPathway()
    {
       
        activateDamage1();
        yield return new WaitForSeconds(0.5f);
        activateDamage2();
        yield return new WaitForSeconds(0.5f);
        activateDamage3();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
