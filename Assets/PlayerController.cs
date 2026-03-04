using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //player parts
    private Rigidbody rb;
    private Collider col;
    //movement

    [SerializeField] private Vector3 MoveDirection;
    public int speed = 5;
    //jump
    Vector3 PlayerFall = new Vector3 (0,0,0);
    public int Gravity = 5;
    public int Jumphighet = 7;
    [SerializeField] private bool Jumping = false;
    [SerializeField]private bool isGrounded = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        if(Gravity > 0)
        {
            Gravity *= -1;
        }
        MoveDirection = rb.position;
    }
    public void OnJump(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            Debug.Log("Jumped");
            Jumping = true;
            
        }
        PlayerFall.y = Jumphighet * -Gravity;
        if (Context.canceled)
        {
            Jumping = false;
        }
    }
    public void OnMove(InputAction.CallbackContext Context)
    {
        Debug.Log("moving");
        Vector2 direction2d = Context.ReadValue<Vector2>();
        MoveDirection = new Vector3(direction2d.x, PlayerFall.y, direction2d.y);
    }
    public bool cheakIfGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,col.bounds.extents.y + 0.1f);

        
    }
    void Update()
    {
        isGrounded = cheakIFGrounded();
        
        if (isGrounded && !Jumping)
        {
            Debug.Log("isGround");
            PlayerFall.y = 0;
        }
        PlayerFall.y += Gravity * Time.deltaTime;
        rb.MovePosition(rb.position + MoveDirection * speed * Time.deltaTime);
    }
}
