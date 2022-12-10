using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataSaver : MonoBehaviour
{
    public int playerSpectrobe;
    public int gameMode;
    public GameObject[] SpectrobeList;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
