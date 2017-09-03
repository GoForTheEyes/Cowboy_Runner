using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour {

    private float speed = 0.1f;
    private Vector2 offset = Vector2.zero;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        //move background slower if player is walking back
        if (Player.walkingBack)
        {
            offset.x += speed/2 * Time.deltaTime;
            mat.SetTextureOffset("_MainTex", offset);
        }
        else
        {
            offset.x += speed * Time.deltaTime;
            mat.SetTextureOffset("_MainTex", offset);
        }

    }
}
