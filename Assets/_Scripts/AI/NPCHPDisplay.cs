using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCHPDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshPro>().text = "HP: " + Mathf.RoundToInt(transform.root.GetComponent<Krawl>().health).ToString();
    }
}