using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekerMoveTets : MonoBehaviour
{
    public Pathfinding pathfinding;
    public float moveSpeed;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }
    
    // Update is called once per frame
    void Update()
    {

        var target = pathfinding.grid.path[0].worldPosition;
        
        transform.LookAt(new Vector3 (target.x, this.transform.position.y, target.z));
        controller.Move(((target - transform.position).normalized)*moveSpeed);
    }


  


   
}
