using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public bool IsClimbing=false;
    public PLAYER player = PLAYER.PLAYER1;

    private Rigidbody2D RB;
    private string horizontalAxis = "HorizontalP1";
    private string verticalAxis = "VerticalP1";

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
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
    }

    public void MoveCharacter()
    {

        float UserInputHorizontal=Input.GetAxisRaw(horizontalAxis);
        float UserInputVertical;
        if (IsClimbing)
        {
            UserInputVertical = Input.GetAxisRaw(verticalAxis);
        }
        else
        {
            UserInputVertical = 0;
        }
        transform.position = new Vector3(Time.deltaTime*Speed*UserInputHorizontal+transform.position.x, Time.deltaTime * Speed * UserInputVertical + transform.position.y, 0);
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            IsClimbing = true;
            RB.gravityScale = 0;
            RB.velocity = Vector2.zero;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            IsClimbing = false;
            RB.gravityScale = 1;

        }
    }

}
