using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCIDDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshPro>().text = "ID: "+(transform.root.GetComponent<Krawl>().gm.currentKrawl.IndexOf(transform.root.gameObject)+1).ToString();
    }
}
