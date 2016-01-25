using UnityEngine;
using System.Collections;

public class EnemyExplodeController : MonoBehaviour {

    private float time_alive = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        time_alive -= Time.deltaTime;
        if (time_alive <= 0) Destroy(gameObject);
	}
}
