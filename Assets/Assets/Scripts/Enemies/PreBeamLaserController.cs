using UnityEngine;
using System.Collections;

public class PreBeamLaserController : MonoBehaviour {

    private float time_alive = 1;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 4);
	}
	
	// Update is called once per frame
	void Update () {
        time_alive -= Time.deltaTime;
        if (time_alive <= 0) Destroy(gameObject);
	}
}
