using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour {

    [SerializeField]
#pragma warning disable 0649
    private AudioClip jumpClip;
#pragma warning restore 0649

    private float jumpForce = 12f, forwardForce = 0f;
    private Rigidbody2D myBody;
    private bool canJump;

    private Button jumpButton;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        jumpButton = GameObject.Find("Jump Button").GetComponent<Button>();
        jumpButton.onClick.AddListener( () => Jump() );
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
        }
    }



	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(myBody.velocity.y) < 0.1f)
        {
            canJump = true;
        }
	}
}
