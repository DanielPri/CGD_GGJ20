using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed;
    public bool IsClimbing=false;
    private Rigidbody2D RB;

    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {

        float UserInputHorizontal=Input.GetAxisRaw("Horizontal");
        float UserInputVertical;
        if (IsClimbing)
        {
            UserInputVertical = Input.GetAxisRaw("Vertical");
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
