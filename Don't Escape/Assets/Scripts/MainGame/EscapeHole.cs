using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The hole that leads to the final part
public class EscapeHole : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Just loads the scene
            SceneManager.LoadScene("Outside");
        }
    }
}
