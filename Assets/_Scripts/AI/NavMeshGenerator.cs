//using System.Collections.Generic;
// UnityEngine.AI;
using UnityEngine;
// System;
using Unity.AI.Navigation;
//using static Unity.VisualScripting.Metadata;
//using Palmmedia.ReportGenerator.Core;

public class NavMeshGenerator : MonoBehaviour
{


    public bool done = false;
  

     void Update()
    {
        if (Input.GetButtonDown("l") && !done)
        {
            GetComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;
            GetComponent<NavMeshSurface>().BuildNavMesh();
            done = true;
           
        }
    }



}