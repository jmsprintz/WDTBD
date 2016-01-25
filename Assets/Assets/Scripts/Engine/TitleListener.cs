using UnityEngine;
using System.Collections;

public class TitleListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //*
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Application.LoadLevel("tc_testing");
        }
        /*/
        /*
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Application.LoadLevel("tc_testing");
        }
        //*/
    }
}
