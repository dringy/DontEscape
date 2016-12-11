using UnityEngine;
using System.Collections;

//Player control and interaction
public class Player : MonoBehaviour {
    public static Player get;

    private void Awake()
    {
        get = this;
    }

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Interactor interactor;

    private Rigidbody2D rbody;

    private void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    //Movement
    private void FixedUpdate () {
        //Don't accept input when speaking, the speech manager will handle it
        if (SpeechManager.get.InSpeechMode())
            return;

        //Get x and y movement
        float y = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;
        float x = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;

        if (x != 0.0f || y != 0.0f)
        {
            //Physics move
            rbody.MovePosition(rbody.position + new Vector2(x, y));

            //Rotate the player to match direction of movement
            //This can probably be done without branches but... tick tock tick tock
            float ideal_angle;
            if (x > 0.0f)
            {
                if (y > 0.0f)
                {
                    ideal_angle = 135.0f;
                }
                else if (y < 0.0f)
                {
                    ideal_angle = 45.0f;
                }
                else
                {
                    ideal_angle = 90.0f;
                }
            }
            else if (x < 0.0f)
            {
                if (y > 0.0f)
                {
                    ideal_angle = -135.0f;
                }
                else if (y < 0.0f)
                {
                    ideal_angle = -45.0f;
                }
                else
                {
                    ideal_angle = -90.0f;
                }
            }
            else if (y > 0.0f)
            {
                ideal_angle = 180.0f;
            }
            else
            {
                ideal_angle = 0.0f;
            }

            //Lerp to smooth rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, ideal_angle), rotationSpeed);
        }
	}

    //Interaction
    void Update()
    {
        //Don't accept input when speaking, the speech manager will handle it
        if (SpeechManager.get.InSpeechMode())
            return;

        //Interact with space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interactor.Interact();
        }
    }
}
