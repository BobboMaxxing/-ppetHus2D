using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool player1;
    [SerializeField] bool player2;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 20f;

    [Header("GroundCheck")]
    [SerializeField] Transform groundTransform;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;

    Vector2 movement1;
    Vector2 movement2;

    Rigidbody2D playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move1();

        if (Input.GetKey(KeyCode.D))
        {
            movement1 = Vector2.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement1 = Vector2.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement2 = Vector2.right;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement2 = Vector2.left;
        }
    }

    void Update()
    {
        HandleSpriteFlip();
    }

    void OnJump()
    {
        if (GroundCheck())
        {
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Move1()
    {
        playerRigidbody.linearVelocityX = movement1.x * moveSpeed;
    }

    void Move2()
    {
        playerRigidbody.linearVelocityX = movement2.x * moveSpeed;
    }

    /*void OnMove(InputValue value) 
    {
        movement = value.Get<Vector2>();
    }*/

    void HandleSpriteFlip()
    {
        if(playerRigidbody.linearVelocityX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (playerRigidbody.linearVelocityX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    bool GroundCheck()
    {
        Collider2D isGrounded = Physics2D.OverlapBox(groundTransform.position, groundCheckSize, 0f, groundLayer);
        return isGrounded;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundTransform.position, groundCheckSize);
    }


}
