using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector3 velocity;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float nextHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundTreshold = 1;



    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        float groundDistance=Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance<=jumpGroundTreshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump=true;
                holdJumpTimer = 0;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false; 
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if(!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= nextHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            

            if(pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded=true;
                holdJumpTimer = 0;
            }
        }

        transform.position = pos;
    }
}
