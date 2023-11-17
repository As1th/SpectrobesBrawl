using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    bool active = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("l"))
        {
            if (!active)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    
                }
                active = true;
            } else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);

                }
                active = false;
            }

        }
    }
}
