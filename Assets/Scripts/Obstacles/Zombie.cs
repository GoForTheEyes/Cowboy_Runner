using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

    private float speed = -4.5f;

    private Rigidbody2D myBody;
    private bool isActive = false;

#pragma warning disable 0649
    [SerializeField]
    private AudioClip zombieMoan;
#pragma warning restore 0649

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        
    }



    // Update is called once per frame
    void Update()
    {
        myBody.velocity = new Vector2(speed, 0f);
        if (!isActive)
        {
            AudioSource.PlayClipAtPoint(zombieMoan, new Vector3 (0,0,0));
            isActive = true;
        }

    }
}
