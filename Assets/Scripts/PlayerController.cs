using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    //player parts
    private Rigidbody rb;
    private Collider col;
    //movement

    [SerializeField] private Vector2 MoveDirection;
    public int speed = 5;
    private int baseSpeed;
    [SerializeField] private Vector3 move;
    //jump
    [SerializeField] Vector3 PlayerFall = new Vector3 (0,0,0);
    public int airJumps = 1;
    public int maxAirJumps = 1;
    private int baseMaxJumps;
    public int JumpHighet = 7;
    private bool Jumping = false;
    [SerializeField]private bool isGrounded = true;
    
    private bool doNothingOnStart = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        airJumps = maxAirJumps;
        baseMaxJumps = maxAirJumps;
        baseSpeed = speed;
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        
        MoveDirection = rb.position;
    }
    public void OnJump(InputAction.CallbackContext context)
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
                airJumps--;
            }
                
            
        }
        

        
    }
    public void OnMove(InputAction.CallbackContext Context)
    {
        Debug.Log("moving");
        MoveDirection = Context.ReadValue<Vector2>();
    }
    public bool cheakIfGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,col.bounds.extents.y + 0.1f);

        
    }
    void Update()
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
            
            
            Debug.Log("Jumped");
            Jumping = false;
        }
        
        
        move = new Vector3(MoveDirection.x, PlayerFall.y, MoveDirection.y);
        if (doNothingOnStart)
        {
            MoveDirection.y = 0;
            move.z = 0;
            doNothingOnStart = false;
        }

        move = Quaternion.Euler(0,transform.eulerAngles.y,0) * move;



        rb.MovePosition(rb.position + move*speed*Time.deltaTime);
        
        
        
    }
}
