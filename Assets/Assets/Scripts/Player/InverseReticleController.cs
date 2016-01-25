using UnityEngine;
using System.Collections;

public class InverseReticleController : MonoBehaviour {

    GameObject ship;

	// Use this for initialization
	void Start () {
        ship = GameObject.Find("Ship");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Clamp(ship.transform.position.x * -1f, -15, 15), Mathf.Clamp(ship.transform.position.y * -1f, -7, 10), 4f);
	}
}
