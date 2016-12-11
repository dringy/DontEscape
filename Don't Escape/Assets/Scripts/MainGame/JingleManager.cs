using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the Jingle sound for doing something right
public class JingleManager : MonoBehaviour {
    public static JingleManager get;

    private void Awake()
    {
        get = this;
    }

    [SerializeField]
    private AudioSource source;

	public void PlayJingle()
    {
        source.Play();
    }
}
