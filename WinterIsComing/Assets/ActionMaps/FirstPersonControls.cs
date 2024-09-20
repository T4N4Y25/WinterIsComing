using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.Presets;
//using UnityEditor.ShaderGraph;
//using UnityEditor.Sprites;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;
//using UnityEngine.Windows;
//using System;
public class FirstPersonControls : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public Transform playerCamera; // Reference to the player's camera
                                   // Private variables to store input values and the character controller
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component
    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public Transform firePoint; // Point from which the projectile is fired
    public float projectileSpeed = 20f; // Speed at which the projectile is fired
    public float pickUpRange = 3f; // Range within which objects can be picked up
    private bool holdingGun = false;
    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    public GameObject heldObject; // Reference to the currently held object
    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float CrouchHeight = 1f;
    float StandingHeight = 2f;
    public float CrouchSpeed = 1.5f;
    private bool IsCrouching = false;
    [Header("CLIMBING SETTINGS")]
    [Space(5)]
    public float ClimbSpeed = 1.5f;
    //private bool isClimbing = false;
    private GameObject climbObject;
   // Vector2 climbDirection = new Vector2();
    [Header("AIR DASH SETTINGS")]
    [Space(5)]
    public float dashspeed = 13f; //Speed player dashes forward with
    public float dashTimer = 0.2f; //Time in which the player accelerates forward
    float fCount = 0; //Counter used in dashTimer
    private bool isDashing = false; //Checks if the player is already dashing forward 
    Vector3 DashDirection = new Vector3(); //Direction player dashes in - the direction is set to the direction the camera is facing
    [Header("INTERACT SETTINGS")]
    [Space(5)]
    public Material switchMaterial; // Material to apply when switch is activated
    public GameObject[] objectsToChangeColor; // Array of objects to change color
    [Header("DISPLAY SETTINGS")]
    [Space(5)]
    public string DisplayMessage = "No current objectives";
    public bool bDisplay = false;



    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        // Create a new instance of the input actions
        var playerInput = new MovementController();
        // Enable the input actions
        playerInput.Player.Enable();
        // Subscribe to the movement input events
        playerInput.Player.Movement.performed += ctx => moveInput =
        ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => moveInput =
        Vector2.zero; // Reset moveInput when movement input is canceled
                      // Subscribe to the look input events
        playerInput.Player.LookAround.performed += ctx => lookInput =
        ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Player.LookAround.canceled += ctx => lookInput =
        Vector2.zero; // Reset lookInput when look input is canceled
                      // Subscribe to the jump input event
        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed
        // Subscribe to the shoot input event
        playerInput.Player.Shoot.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed
        // Subscribe to the pick-up input event
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); // Call the PickUpObject method when pick-up input is performed
        playerInput.Player.Crouch.performed += ctx => Crouch();
        playerInput.Player.Climb.performed += ctx => Climb(); //Call to initiate climbing
        playerInput.Player.Dash.performed += ctx => StartDash();

        playerInput.Player.Interact.performed += ctx => Interact(); // Interact with switch
        playerInput.Player.DisplayQuest.performed += ctx => DisplayQuest();
        




    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player  movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();
        //Debug.Log();

        if (isDashing) 
        {
            Dash();
        }


    }

    private void DisplayQuest()
    {
        if (bDisplay)
        {
            bDisplay = false;
        }
        else
        {
            bDisplay = true;
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.fontStyle = FontStyle.BoldAndItalic; 
        style.normal.textColor = Color.white;
        if (bDisplay) 
        {
            GUI.Label(new Rect(10, 10, 600, 200),DisplayMessage,style); 
        }

    }

    private void StartDash() //Initiates the dash by setting the isDashing boolean to true
    {
        isDashing = true;
        fCount = 0; //Initialize counter

        DashDirection = playerCamera.forward; //Set direction of dash to where the camera is facing 
        DashDirection.y = 0; //Prevents dashing upwards; ensures the dashing remians straight
    }

    private void Dash()
    {
        fCount += Time.deltaTime; //Increment the counter by seconds
        characterController.Move(DashDirection*dashspeed*Time.deltaTime); //Intitate the Move function with the velocity created with the paraeters (from Unity Documentation)

        if (fCount >= dashTimer) //When the Counter increments to the dashTimer value, the dash is ended
        {
            isDashing = false; //Dashing can only initiate when this variable is true (used in update method)
        }
    }

   



    public void Move()
    {
        float CurrentSpeed;

        if (IsCrouching)
        {
            CurrentSpeed = CrouchSpeed;
        }
        else
        {
            CurrentSpeed = moveSpeed;
        }
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        // Transform direction from local to world space
        move = transform.TransformDirection(move);
        // Move the character controller based on the movement vector and  speed
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on  sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;
        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);
        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f,
        90f);
        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation,
        0, 0);
    }
    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
    }
    public void Jump()
    {

        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }
    public void Shoot()
    {
        if (holdingGun == true)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab,
            firePoint.position, firePoint.rotation);
            // Get the Rigidbody component of the projectile and set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;
            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);
        }
    }
    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingGun = false;
        }
        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward *
        pickUpRange, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                // Disable physics
                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
            }
            else if (hit.collider.CompareTag("Gun"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                // Disable physics
                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingGun = true;
            }
        }
    }

    public void Crouch()
    {
        if (IsCrouching)
        {
            characterController.height = StandingHeight;
            IsCrouching = false;
        }
        else
        {
            characterController.height = CrouchHeight;
            IsCrouching = true;
        }
    }

    public void Climb()
    {


        Ray ray = new Ray(playerCamera.position, playerCamera.forward); //Same Raycast method from pickup objects
        RaycastHit hit;
        Debug.DrawRay(playerCamera.position, playerCamera.forward *
        pickUpRange, Color.blue, 2f);
        Vector3 playeronladder = new Vector3();
        float objectHeight = 0f;

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {

            if (hit.collider.CompareTag("Climbable")) //If a ladder or climbable object is encountered with the raycast the player can climb it
            {

                climbObject = hit.collider.gameObject; //object with climable tag gets stored in object variable when raycast hits it
                objectHeight = climbObject.GetComponent<Renderer>().bounds.size.y; //The height of the climable object is stored as reference for how high the player should propel upwards (From Unity Documentation)
               // isClimbing = true; //Set to true to indicate climbing (was used in a previous method to incorporate the climbing but does not have a purpose anymore
                climbObject.GetComponent<Rigidbody>().isKinematic = false; //From pickup method (this method was used as reference), was set to false as it did not seem to affect anything

                playeronladder = hit.collider.transform.position; //the vector is used to orientate the player while climbing
                playeronladder.x = this.gameObject.transform.position.x;//x poition remains the same as the player is only moving upward
                playeronladder.z = this.gameObject.transform.position.z;//z position remains the same as the player is only moving upward
                this.gameObject.transform.position = playeronladder;
                velocity.y = Mathf.Sqrt(objectHeight * 2f);//Same method used in jumping - by repeatedly applying this using the raycast the player can appear to be climbing




                // this.gameObject.transform.rotation = climbObject.transform.rotation;

            }
            ////else
            //{
              //  isClimbing = false;
            //}

        }
    }

    public void Interact()
    {
        // Perform a raycast to detect the lightswitch
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("Switch")) // Assuming the switch has this tag
            {
                // Change the material color of the objects in the array
                foreach (GameObject obj in objectsToChangeColor)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = switchMaterial.color; // Set the color to match the switch material color
                    }
                }
            }

            else if (hit.collider.CompareTag("Door")) // Check if the object is a door
            {
                 //Start moving the door upwards
                StartCoroutine(RaiseDoor(hit.collider.gameObject));
            }
        }
    }


    private IEnumerator RaiseDoor(GameObject door)
    {
        float raiseAmount = 5f; // The total distance the door will be raised
        float raiseSpeed = 2f; // The speed at which the door will be raised
        Vector3 startPosition = door.transform.position; // Store the initial position of the door
        Vector3 endPosition = startPosition + Vector3.up * raiseAmount; // Calculate the final position of the door after raising

        // Continue raising the door until it reaches the target height
        while (door.transform.position.y < endPosition.y)
        {
            // Move the door towards the target position at the specified speed
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPosition, raiseSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame before continuing the loop
        }
    }






}