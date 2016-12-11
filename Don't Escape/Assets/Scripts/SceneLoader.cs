using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Loads a given scene when space is pressed
public class SceneLoader : MonoBehaviour {
    [SerializeField]
    private string scene_name;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(scene_name);
        }
	}
}
