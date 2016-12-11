using UnityEngine;
using System.Collections;

//A giant black image that can be pushed during the boundary task of the game
public class UpwardBoundary : MonoBehaviour {

    private Vector3 starting_position; //needed for resets
    private Rigidbody2D rbody; //We fiddle with the constraints

    [SerializeField]
    private float heightTrigger; //When do we stop

    //Set up
	private void Start () {
        starting_position = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        Deactivate();
    }

    public void Activate()
    {
        //Freeze all except y movement
        RigidbodyConstraints2D constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        rbody.constraints = constraints;
    }

    public void Deactivate()
    {
        //Freeze all
        RigidbodyConstraints2D constraints = RigidbodyConstraints2D.FreezeAll;
        rbody.constraints = constraints;
    }

    public bool IsTriggered()
    {
        //Reached target y?
        return transform.position.y >= heightTrigger;
    }

    public void Reset()
    {
        //Move back
        transform.position = starting_position;
        Deactivate();
    }

    public void MoveToTriggered()
    {
        //Used for reloads in the quit phase
        transform.position = new Vector3(starting_position.x, heightTrigger, starting_position.z);
        Deactivate();
    }
}
