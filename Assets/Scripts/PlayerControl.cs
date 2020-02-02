﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public bool isRunning = false;
    public bool isClimbing = false;
    public bool isRepairing = false;
    public bool facingLeft = false;
    public bool isGrounded = true;
    public bool snapToLadder = false;
    Animator animator;
    
    public PLAYER player = PLAYER.PLAYER1;

    private Rigidbody2D RB;
    private string horizontalAxis = "HorizontalP1";
    private string verticalAxis = "VerticalP1";
    private string repair = "Player1Repair";
    private float ladderCenter = 0f;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Animate();
        Repair();
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
                Debug.Log("AM HERE");
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
        }
        else
        {
            UserInputVertical = 0;
        }
        transform.position = new Vector3(Time.deltaTime * Speed * UserInputHorizontal + transform.position.x, Time.deltaTime * Speed * UserInputVertical + transform.position.y, 0);

        if (isClimbing && !isGrounded)
        {
            snapToLadder = true;
            transform.position = new Vector3(ladderCenter, Time.deltaTime * Speed * UserInputVertical + transform.position.y, 0);
        }
        else
        {
            snapToLadder = false;
        }
        

    }

    private void Animate()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isClimbing", snapToLadder);
        animator.SetBool("isRepairing", isRepairing);
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

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            isClimbing = true;
            RB.gravityScale = 0;
            RB.velocity = Vector2.zero;
            ladderCenter = col.bounds.center.x;
        }
        if (col.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            isClimbing = false;
            RB.gravityScale = 1;
        }

        if (col.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
