using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public bool isGrounded = true;
    public bool isRunning = false;
    public bool isClimbing = false;
    public bool isRepairing = false;
    public bool isInteracting = false;
    public bool facingLeft = false;
    
    public LayerMask floorLayerMask;
    public float raycastLength = 0.5f;
    Animator animator;
    
    public PLAYER player = PLAYER.PLAYER1;

    private Rigidbody2D RB;
    private string horizontalAxis = "HorizontalP1";
    private string verticalAxis = "VerticalP1";
    private string repair = "Player1Repair";
    private string interact = "Player1Action";
    private float ladderCenter = 0f;
    private Collider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(player == PLAYER.PLAYER2)
        {
            horizontalAxis = "HorizontalP2";
            verticalAxis = "VerticalP2";
            repair = "Player2Repair";
            interact = "Player2Action";
        }
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Animate();
        Repair();
        Interact();
        isGroundedRay();
        
    }

    public void MoveCharacter()
    {
        float UserInputHorizontal = Input.GetAxisRaw(horizontalAxis);
        Debug.Log(UserInputHorizontal);
        if (UserInputHorizontal != 0)
        {
            isRunning = true;

            if (UserInputHorizontal < 0)
            {
                facingLeft = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                if (facingLeft)
                {
                    facingLeft = false;
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
        else
        {
            isRunning = false;
        }
        
        float UserInputVertical;
        if (isClimbing)
        {
            UserInputVertical = Input.GetAxisRaw(verticalAxis);
            if (UserInputVertical == 0 && !isGrounded)
            {
                animator.enabled = false;
            }
            else
            {
                animator.enabled = true;
            }
        }
        else
        {
            UserInputVertical = 0;
            animator.enabled = true;
        }
        float newposX = Time.deltaTime * Speed * UserInputHorizontal + transform.position.x;
        float newposY = Time.deltaTime * Speed * UserInputVertical + transform.position.y;

        if(UserInputVertical != 0)
        {
            UserInputHorizontal = 0;
        }
        transform.position = new Vector3(newposX, newposY, 0);

    }

    private void Animate()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isClimbing", isClimbing && !isGrounded);
        animator.SetBool("isRepairing", isRepairing);
        animator.SetBool("isInteracting", isInteracting);
    }

    private void Repair()
    {
        if (Input.GetButtonDown(repair))
        {
            isRepairing = true;
        }
        else
        {
            isRepairing = false;
        }
    }

    private void Interact()
    {
        if(Input.GetButton(interact))
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
    }

    public void isGroundedRay()
    {
        RaycastHit2D rh = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, raycastLength, floorLayerMask);
        isGrounded = rh.collider != null;
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            isClimbing = true;
            RB.gravityScale = 0;
            RB.velocity = Vector2.zero;
            ladderCenter = col.bounds.center.x;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            isClimbing = false;
            RB.gravityScale = 1;
        }
    }

    
}
