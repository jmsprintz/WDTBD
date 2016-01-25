using UnityEngine;
using System.Collections;

public class BlinkReticleController : MonoBehaviour {

    private GameObject ship;
    private ShipController ship_ctrl;
    private Vector3 point;

	// Use this for initialization
	void Start () {
        ship = GameObject.Find("Ship");
        ship_ctrl = ship.GetComponent<ShipController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(ship_ctrl.transform.position.x, ship_ctrl.transform.position.y, 4) + ship_ctrl.saved_dir;
	}
}
