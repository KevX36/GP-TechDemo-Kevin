using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SurviceHub hub;
    [SerializeField] private GameStateManager stateManager;
    //player parts
    private Rigidbody rb;
    private Collider col;
    [SerializeField] private Camera cam;

    //movement

    [SerializeField] private Vector2 MoveDirection;
    public int speed = 5;
    private int baseSpeed;
    [SerializeField] private Vector3 move;
    //jump
    [SerializeField] Vector3 PlayerFall = new Vector3(0, 0, 0);
    [SerializeField] private int airJumps = 1;
    public int maxAirJumps = 1;
    private int baseMaxJumps;
    public float JumpHighet = 7;
    public float baseJumpHighet;
    private bool Jumping = false;
    [SerializeField] private bool isGrounded = true;
    // box
    public GameObject GrabSpot;
    public int TossPower = 5;
    [SerializeField] private GameObject boxGO;
    [SerializeField] private Rigidbody boxRB;
    [SerializeField] private IBox boxOpen;
    
    public float GrabRange = 5;



    //speed boost
    public float speedTime = 30;
    private float speedTimer;
    public int speedBoostInt = 2;
    private bool activeSpeedBoost = false;
    //jump boost
    public int jumpsToAdd = 2;
    private bool ExtraJumpsActive = false;
    public float extraJumpTime = 30;
    private float extraJumpTimer;



    //makes sure no movement happens at start without imput
    private bool doNothingOnStart = true;
    IEnumerator JumpBoost()
    {

        if (!ExtraJumpsActive)
        {
            ExtraJumpsActive = true;
            extraJumpTimer = extraJumpTime;
            maxAirJumps += jumpsToAdd;

            airJumps = maxAirJumps;
            while (speedTimer >= 0)
            {
                extraJumpTimer -= Time.deltaTime;
                Debug.Log(extraJumpTimer);
                yield return null;
            }
            maxAirJumps = baseMaxJumps;
            ExtraJumpsActive = false;
            if (airJumps > maxAirJumps)
            {
                extraJumpTimer = extraJumpTime;
                airJumps = maxAirJumps;
                Debug.Log("Reset air jumps");
            }
        }
        else
        {
            extraJumpTimer = extraJumpTime;
            airJumps = maxAirJumps;
        }
    }
    IEnumerator SpeedBoost()
    {
        
        if (!activeSpeedBoost)
        {
            activeSpeedBoost = true;
            speed *= speedBoostInt;
            speedTimer = speedTime;
            JumpHighet /= 1.4f;
            while (speedTimer >= 0)
            {
                speedTimer -= Time.deltaTime;
                Debug.Log(speedTimer);
                yield return null;
            }
            speed = baseSpeed;
            JumpHighet = baseJumpHighet;
            activeSpeedBoost = false ;
        }
        else
        {
            speedTimer = speedTime;
        }
    }
    public void SetBuff(string boost)
    {
        Debug.Log(boost);
        if(boost == "speed")
        {
            StartCoroutine(SpeedBoost());
        }
        if (boost == "jump")
        {
            StartCoroutine(JumpBoost());
        }
        else
        {
            Debug.Log("not a boost");
        }
    }
    
    void Awake()
    {
        stateManager = hub.stateManager;
        airJumps = maxAirJumps;
        baseMaxJumps = maxAirJumps;
        baseSpeed = speed;
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        MoveDirection = rb.position;
        baseJumpHighet = JumpHighet;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(stateManager.currentState == GameStateManager.GameState.GamePlay)
        {
            InputAction input = context.action;
            Debug.Log("started jump");
            if (input.IsPressed())
            {
                if (isGrounded)
                {
                    Jumping = true;
                }
                else if (airJumps > 0)
                {
                    Jumping = true;
                    
                }


            }
        }
        

        
    }
    public void OnPause()
    {
        stateManager.OnPause();
    }
    public void GrabAndDrop()
    {
        RaycastHit hit;
        if (boxGO == null && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, GrabRange))
        {
            Debug.Log("Tried To pick up box");
            boxGO = hit.transform.gameObject;
            if (boxGO.CompareTag("PickUp"))
            {
                Debug.Log("Grabed box");
                boxRB = boxGO.GetComponent<Rigidbody>();
                boxGO.GetComponent<BoxCollider>().enabled = false;
                boxRB.constraints = RigidbodyConstraints.FreezeAll;
                boxOpen = boxGO.GetComponent<IBox>();
            }
            else
            {
                Debug.Log("tried to grab something that was not a box");
                boxGO = null;

            }
            
            
        }
        else if (boxGO != null)
        {
            Debug.Log("toss the box");
            boxGO.GetComponent<BoxCollider>().enabled = true;
            boxRB.constraints = RigidbodyConstraints.None;
            boxRB.AddForce(cam.transform.forward);
            boxGO = null;
            boxRB = null;
            boxOpen = null;
        }



    }
    public void Throw()
    {
        if (boxGO != null)
        {
            Debug.Log("toss the box");
            boxGO.GetComponent<BoxCollider>().enabled = true;
            boxRB.constraints = RigidbodyConstraints.None;
            boxRB.AddForce((cam.transform.forward * TossPower) + (cam.transform.up* (TossPower / 2)));
            boxGO = null;
            boxRB = null;
            boxOpen = null;
        }
    }
    public void cutBox()
    {
        Debug.Log("tried to cut box");
        if (boxGO != null)
        {
            Debug.Log("cut box open");



            boxOpen.Open();
            boxGO.GetComponent<BoxCollider>().enabled = true;
            boxRB.constraints = RigidbodyConstraints.None;
            
            boxGO = null;
            boxRB = null;
            boxOpen = null;

        }
        
    }
    public void OnMove(InputAction.CallbackContext Context)
    {
        if (stateManager.currentState == GameStateManager.GameState.GamePlay)
        {
            Debug.Log("moving");
            MoveDirection = Context.ReadValue<Vector2>();
        }
        
    }
    public bool cheakIfGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,col.bounds.extents.y + 0.1f);

        
    }
    
    void Update()
    {
        Debug.DrawLine(cam.transform.position, (cam.transform.forward * GrabRange) + cam.transform.position, Color.red);
        if (stateManager.currentState == GameStateManager.GameState.GamePlay)
        {
            isGrounded = cheakIfGrounded();
            if (isGrounded)
            {
                airJumps = maxAirJumps;


            }
            if (PlayerFall.y > 0)
            {
                PlayerFall += Physics.gravity * Time.deltaTime;
            }
            if (Jumping)
            {
                PlayerFall.y = JumpHighet;
                if (!isGrounded)
                {
                    airJumps--;
                }

                Debug.Log("Jumped");
                Jumping = false;
            }
            
            if (boxGO != null)
            {

                boxGO.transform.position = GrabSpot.transform.position;
                boxGO.transform.rotation = GrabSpot.transform.rotation;
            }


            move = new Vector3(MoveDirection.x, PlayerFall.y, MoveDirection.y);
            move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;
            if (doNothingOnStart)
            {
                MoveDirection.y = 0;
                move.z = 0;
                doNothingOnStart = false;
            }
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);
        }
        
        
        
        
    }
}
