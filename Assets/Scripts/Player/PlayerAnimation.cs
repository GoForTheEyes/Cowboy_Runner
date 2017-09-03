using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Obstacle")
        {
            anim.Play("Player Idle");
        }

    }

    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.tag == "Obstacle")
        {
            anim.Play("Player Walk");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.walkingBack) {
            anim.Play("Player Walk");
        }

    }
}
