using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
 
    [SerializeField]
    private int maxSpeed, jumpMultiplyer, secondJumpMultiplyer;



    [SerializeField]
    private float speedX, dashingDelay;

    [SerializeField, HideInInspector]
    private float speedMultipler, stoppingSpeed;

    private float inAirStoppingSpeed;// not used

    private float maxSpeedInAir;// not used

    [SerializeField]
    private int dashingSpeed;

    private float rayDistance, sideRayDistance;

    private bool notFalling = true, doubleJump = false, isDashing = false, stopDashing = true;

   
    [SerializeField]
    private bool grounded = false;

    private Rigidbody rb;

    private Vector2 velocitySpeed;

    int groundLayerMask = 1 << 3;
    [SerializeField]
    private Transform raycastPoint1, raycastPoint2, raycastPoint3, raycastPoint4;


    [SerializeField] private GameObject playerObj, dashParticalEffect;

    public Animator animator;
    public GameObject animObj;

    private bool rightMovement = false;
    private bool leftMovement = false;
    private bool jumpMovement = false;
    private bool lastJump = false;
    private bool dashingMovement = false;
    private bool dashingLeft = false;
    private bool dashingRight = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
       animator = animObj.GetComponent<Animator>();
        animObj.SetActive(true);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(velocitySpeed.x, rb.velocity.y);
    }

    void Update()
    {
        //Vector3 down = transform.TransformDirection(Vector3.down) / 2;
        //Debug.DrawRay(transform.position, down, Color.red);
        movement();

        jumping();

        //shrinking(); needs fixing 

        rayCasting();

        //debuging();

        dashing();

        anim();

        unstuct();
    }
   

    void movement()
    {
        Vector2 horizontal = transform.TransformDirection(Vector2.right);

        if (rightMovement && (speedX < maxSpeed) && grounded && notFalling)
        {
            speedX += speedMultipler;
            //Debug.Log("D key pressed \n");
            //Debug.Log(speedX);

        }
        



        // curspeedX -= (speedMultipler / 2);

        else if (leftMovement && (speedX > -maxSpeed) && grounded && notFalling)
        {

            speedX -= speedMultipler;

            //Debug.Log("A key pressed \n");
            //Debug.Log(speedX);
        }

        // else if (!grounded)
        //  inAir();

        else if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D))
            stopping();

       // else
         //   stopping();

        //Mathf.RoundToInt(speedX);
        velocitySpeed = horizontal * Mathf.RoundToInt(speedX);
    }

    void jumping()
    {
       
        if (jumpMovement && grounded)
        {
            jumpMovement = false;
            StartCoroutine(doubleJumpDelay());
            rb.velocity = new Vector2(rb.velocity.x, jumpMultiplyer);
            
        }
        if (lastJump && doubleJump)
        {
           //Debug.Log("double jumping");
            rb.velocity = new Vector2(rb.velocity.x, secondJumpMultiplyer);
            lastJump = false;
            doubleJump = false;
        }
       // if (Input.GetKeyDown(KeyCode.Space))
          //  rb.velocity = new Vector2(rb.velocity.x, jumpMultiplyer);
    }

    IEnumerator doubleJumpDelay()
    {
        yield return new WaitForSeconds(0.2f);
        doubleJump = true;
    }



    void shrinking()
    {//__________________________________________working on needs finishing 
        int numSwitch = 0;
        if (Input.GetKeyDown(KeyCode.Tab))
            ++numSwitch;
        
        //Debug.Log(numSwitch);
        switch (numSwitch)
        {
            case 1:
                playerObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                playerObj.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }
        if (numSwitch >= 3)
            numSwitch = 0;
        //________________________________________________________________
    }


    void dashing()
    { if (!isDashing)
        {
            float speedXValueHold = 0;
            if (dashingMovement && rightMovement)
                StartCoroutine(dashDelayRight(speedXValueHold));

            else if (dashingMovement && leftMovement)
                StartCoroutine(dashDelayLeft(speedXValueHold));
        }

    }

    IEnumerator dashDelayRight(float speedXValueHold) 
    {
        dashingRight = true;
        dashingMovement = false;
        speedXValueHold = speedX;
        isDashing = true;
        stopDashing = false;
        speedX = dashingSpeed;
        dashingEffectRight();
        yield return new WaitForSeconds(0.2f);
        dashingRight = false;
        speedX = speedXValueHold;
        stopDashing = true;
        yield return new WaitForSeconds(dashingDelay);
        isDashing = false;
        
    }

    IEnumerator dashDelayLeft(float speedXValueHold) 
    {
        dashingLeft = true;
        dashingMovement = false;
        speedXValueHold = speedX;
        isDashing = true;
        stopDashing = false;
        speedX = -dashingSpeed;
        dashingEffectLeft();
        yield return new WaitForSeconds(0.2f);
        dashingLeft = false;
        speedX = speedXValueHold;
        stopDashing = true;
        yield return new WaitForSeconds(dashingDelay);
        isDashing = false;
        
    }



    void dashingEffectRight()
    {
        Instantiate(dashParticalEffect, playerObj.transform.position, dashParticalEffect.transform.rotation = Quaternion.Euler(new Vector3(0,-90, 0)));// rotates to the left
    }

    void dashingEffectLeft()
    {
        Instantiate(dashParticalEffect, playerObj.transform.position, dashParticalEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)));
    }

    void rayCasting()
    {
        RaycastHit hitDown;
        if (Physics.Raycast(raycastPoint3.position, Vector3.down, out hitDown, 5, groundLayerMask) ||
            Physics.Raycast(raycastPoint4.position, Vector3.down, out hitDown, 5, groundLayerMask))// shoots a ray cast bellow the character
        {
            rayDistance = hitDown.distance;
            if (rayDistance < 0.08)//_____________ set character hight__________________________________ distance off the ground
            {
                
                
                grounded = true;
                doubleJump = false;

            }
           
            else
                grounded = false;
            
        }
      

        RaycastHit hitSide;
        if (Physics.Raycast(raycastPoint1.position, Vector3.right, out hitSide, 1, groundLayerMask) || Physics.Raycast(raycastPoint2.position, Vector3.right, out hitSide, 1, groundLayerMask) || 
            Physics.Raycast(raycastPoint1.position, Vector3.left, out hitSide, 1, groundLayerMask) || Physics.Raycast(raycastPoint2.position, Vector3.left, out hitSide, 1, groundLayerMask))
        {
            sideRayDistance = hitSide.distance;
            if (sideRayDistance < 0.50 && grounded == false)// _______________set character thickness__________________________ thickness of character 
            {
                //Debug.Log("raytrace side hit");
                speedX = 0;
                notFalling = false;
            }
            else
                notFalling = true;

        }
        
        Debug.DrawRay(raycastPoint1.position, Vector3.right * sideRayDistance, Color.green);
        Debug.DrawRay(raycastPoint1.position, Vector3.left * sideRayDistance, Color.red);
        Debug.DrawRay(raycastPoint2.position, Vector3.right * sideRayDistance, Color.green);
        Debug.DrawRay(raycastPoint2.position, Vector3.left * sideRayDistance, Color.red);  
        Debug.DrawRay(raycastPoint3.position, Vector3.down * rayDistance, Color.green);
        Debug.DrawRay(raycastPoint4.position, Vector3.down * rayDistance, Color.red);
        
    }
  

    void inAir()
    {
        if (speedX > maxSpeedInAir)
            speedX -= inAirStoppingSpeed;
        else if (speedX < -maxSpeedInAir)
            speedX += inAirStoppingSpeed;
        
    }
    void stopping()
    {
      
        int intSpeedX = 0;
        Mathf.RoundToInt(speedX);
        intSpeedX = (int)speedX;
        //Debug.Log(intSpeedX);
        if (speedX != 0 && grounded && stopDashing)
        {
            if (speedX > 0)
            {
                float zero = 0;
                zero -= speedX;
               speedX -= stoppingSpeed;
                //Debug.Log("speedx -");
                //speedX -= zero;

            }
                
            else if (speedX < 0)
            {
                speedX += stoppingSpeed;
                //Debug.Log("speedx +");
                float zero = 0;
                zero -= intSpeedX;
                
                //Debug.Log(zero);
                //speedX += (zero/ 5);
            }
           
        }
    }

    // Registering button Inputs and enableing bools
    public void rightButtonPress()
    {
        Debug.Log("button Pressed \n");
        rightMovement = true;
    }
    public void rightButtonRelease()
    {
        rightMovement = false;
    }

    public void leftButtonPress()
    {
        leftMovement = true;
    }
    public void leftButtonRelease()
    {
        leftMovement= false;
    }
    public void dashingButtonPress()
    {
        dashingMovement = true;
    }

    public void jumpButtonPress()
    {
        if (grounded)
            jumpMovement = true;

        else if(doubleJump)
            lastJump = true;
    }

    void anim()
    {
        /*
        if (leftMovement && grounded)
        {
            walkingLeft = true;
            walkingRight = false;
        }
          

        else if (!leftMovement && grounded)
        {
            walkingLeft = false;
        }
            
        else if (rightMovement && grounded)
        {
            Debug.Log("right movement ");
            walkingRight = true;
            walkingLeft = false;
        }
            

        else if (!rightMovement && grounded)
        {
            walkingRight = false;
        }
         
        else
        {
            walkingLeft = false;
            walkingRight = false;
        }

        /*if (speedX < 0)
        {
            walkingLeft = true;
        }
        if (speedX > 0) 
        {
            walkingRight = true;
        }*/

        animator.SetBool("dashing left", dashingLeft);
        animator.SetBool("dashing right", dashingRight);
        animator.SetBool("walk Left", leftMovement);
        animator.SetBool("walk right", rightMovement);
    }

    void unstuct()
    {
        if(!grounded && speedX <= 0.1)
        {
            grounded = true;    
        }
    }
      void debuging()
      {
        Debug.Log(notFalling + " not falling");
        
        Debug.Log(grounded + " grounded");
      }
}
