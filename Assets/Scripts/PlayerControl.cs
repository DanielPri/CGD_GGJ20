using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public bool isRunning = false;
    public bool isClimbing = false;
    public bool isRepairing = false;
    Animator animator;
    
    public PLAYER player = PLAYER.PLAYER1;

    private Rigidbody2D RB;
    private string horizontalAxis = "HorizontalP1";
    private string verticalAxis = "VerticalP1";
    
    // Start is called before the first frame update
    void Start()
    {
        Speed = 1;
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if(player == PLAYER.PLAYER2)
        {
            horizontalAxis = "HorizontalP2";
            verticalAxis = "VerticalP2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Animate();
    }

    public void MoveCharacter()
    {
        float UserInputHorizontal = Input.GetAxisRaw(horizontalAxis);
        
        if (UserInputHorizontal != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        float UserInputVertical;
        
        if (isClimbing)
        {
            UserInputVertical = Input.GetAxisRaw(verticalAxis);
        }
        else
        {
            UserInputVertical = 0;
        }
        transform.position = new Vector3(Time.deltaTime * Speed * UserInputHorizontal + transform.position.x, Time.deltaTime * Speed * UserInputVertical + transform.position.y, 0);
    }

    private void Animate()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isClimbing", isClimbing);
        animator.SetBool("isRepairing", isRepairing);
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            isClimbing = true;
            RB.gravityScale = 0;
            RB.velocity = Vector2.zero;
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
