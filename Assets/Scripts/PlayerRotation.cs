using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

  [SerializeField]
  private float movementSpeed;

  private Rigidbody RB;

  private Vector3 moveInput;
  private Vector3 moveVelocity;

  private Camera cam;

  private bool UsingMouse = false;

    // Start is called before the first frame update
    void Start()
    {
      RB = GetComponent<Rigidbody>();
      cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
      moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
      moveVelocity = moveInput * movementSpeed;

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
    }

    void FixedUpdate()
    {
      RB.velocity = moveVelocity;
    }
}
