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
        print(target);
        transform.LookAt(target);
        controller.Move(((target - transform.position).normalized)*moveSpeed);
    }


  


    bool MoveTowardsTarget(Vector3 target)
    {
        
        var offset = target - transform.position;
        //Get the difference.
        if (offset.magnitude > .1f)
        {
            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            offset = offset.normalized * moveSpeed;
            //normalize it and account for movement speed.
            controller.Move(offset * Time.deltaTime);
            //actually move the character.
            return false;
        }
        else
        {
            return true;
        }
    }
}
