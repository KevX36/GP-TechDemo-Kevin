using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController Controller;

    //movement
    private Rigidbody rb;
    private Vector3 MoveDirection;
    public int speed = 5;
    //jump
    Vector3 PlayerFall = new Vector3 (0,0,0);
    public int Gravity = 5;
    public int Jumphighet = 7;
    private bool Jumping = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Controller = this.GetComponent<CharacterController>();
        MoveDirection = rb.position;
    }
    public void OnJump(InputAction.CallbackContext Context)
    {
        if (Context.started)
        {
            Debug.Log("Jumped");
            Jumping = true;
            PlayerFall.y = Jumphighet * Gravity;
        }
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
    // Update is called once per frame
    void Update()
    {
        if (Controller.isGrounded && !Jumping)
        {
            PlayerFall.y = 0;
        }
        PlayerFall.y -= Gravity * Time.deltaTime;
        rb.MovePosition(rb.position + MoveDirection * speed * Time.deltaTime);
    }
}
