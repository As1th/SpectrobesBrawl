using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VortexHPDisplay : MonoBehaviour
{
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = transform.root.GetComponent<VortexController>().health.ToString();
        transform.LookAt(Camera.main.transform);
    }
}
