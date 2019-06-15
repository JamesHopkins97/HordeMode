using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    private float usingSpeed;
    private Rigidbody RB;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera cam;

    private bool UsingMouse = false;

    private GunController GC;

    private GameObject Gun;


    //Store the current horizontal input in the float moveHorizontal.
    float moveHorizontal;

    //Store the current vertical input in the float moveVertical.
    float moveVertical;

    //Use the two store floats to create a new Vector2 variable movement.
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();
        usingSpeed = movementSpeed;
        Gun = GameObject.Find("Gun");
        GC = Gun.GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0, moveVertical);

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * usingSpeed;

        if (UsingMouse)
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.up, Vector3.zero);

            float rayLength;

            if (ground.Raycast(camRay, out rayLength))
            {
                Vector3 pointDir = camRay.GetPoint(rayLength);
                Debug.DrawLine(camRay.origin, pointDir, Color.blue);
                transform.LookAt(new Vector3(pointDir.x, transform.position.y, pointDir.z));
            }
        }
        else
        {
            Vector3 controllerDirection = Vector3.right * Input.GetAxisRaw("HorizontalC") + Vector3.forward * -Input.GetAxisRaw("VerticalC");

            if (controllerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(controllerDirection, Vector3.up);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("RTriggerC"))
        {
            GC.SetFiring(true);
        }

    }

    void FixedUpdate()
    {
        //RB.velocity = moveVelocity;
        RB.velocity = (movement * usingSpeed);

    }
}
