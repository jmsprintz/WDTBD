using UnityEngine;
using System.Collections;

public class BlinkFromController : MonoBehaviour {

    private float time_to_live = 0.8f;
	
	// Update is called once per frame
	void Update () {
        time_to_live -= Time.deltaTime;
        if(time_to_live < 0)
        {
            Destroy(gameObject);
        }
    }
}
