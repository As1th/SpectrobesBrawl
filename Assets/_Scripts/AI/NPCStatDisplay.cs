using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;
    void Start()
    {
        gm = transform.root.GetComponent<Krawl>().gm;
    }

    // Update is called once per frame
    void Update()
    {
       
            if (gm.displayNPCStats)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    
                }
                transform.LookAt(Camera.main.transform.position);
               
            } else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);

                }
               
            }

        
    }
}
