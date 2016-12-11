using UnityEngine;
using System.Collections;

//Sets the window to a specific size
public class FixedSizedCamera : MonoBehaviour {

    [SerializeField]
    private int height;

    [SerializeField]
    private int width;

    private void Start()
    {
        Screen.SetResolution(width, height, false);
    }
}
