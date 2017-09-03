using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
#pragma warning disable 0649
    private AudioClip jumpClip;
#pragma warning restore 0649

    private float jumpForce = 12f, forwardForce = 0f;
    private float backwardSpeed = -4f;
    private Rigidbody2D myBody;
    private bool canJump, canWalkback;

    public static bool walkingBack = false;

    private Button jumpButton;
    private Button walkbackButton;
    //private Button goBack;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        jumpButton = GameObject.Find("Jump Button").GetComponent<Button>();
        jumpButton.onClick.AddListener( () => Jump() );
        walkbackButton = GameObject.Find("Walkback Button").GetComponent<Button>();
        walkbackButton.onClick.AddListener( () => WalkBack() );

    }

    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            AudioSource.PlayClipAtPoint(jumpClip, transform.position);
            if (transform.position.x<0)
            {
                forwardForce = 1f;
            }
            else
            {
                forwardForce = 0f;
            }

            myBody.velocity = new Vector2(forwardForce, jumpForce);
            canWalkback = false;
        }
    }

    public void WalkBack()
    {
        if (canWalkback)
        {
            myBody.velocity = new Vector2(backwardSpeed, 0f);
            walkingBack = true;
        }

    }


    // Update is called once per frame
    void Update () {
        walkingBack = false;

        if (Mathf.Abs(myBody.velocity.y) < 0.1f)
        {
            canJump = true;
            canWalkback = true;
        }
        ControlByKeyboard();
        
        //Check for move:
        if (walkbackButton.GetComponent<WalkBackButton>().PlayerMovingBackPressed==true)
        {
            WalkBack();
        }


    }

    void ControlByKeyboard() {
        if (Input.GetKey("left"))
        {
            walkbackButton.onClick.Invoke();
        }
        if (Input.GetKey("space"))
        {
            jumpButton.onClick.Invoke();
        }
    }




}
