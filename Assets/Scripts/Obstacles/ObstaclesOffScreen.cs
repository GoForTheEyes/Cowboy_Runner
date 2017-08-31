using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesOffScreen : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Obstacle" || target.tag == "Enemy")
        {
            target.gameObject.SetActive(false);
        }

    }
}
