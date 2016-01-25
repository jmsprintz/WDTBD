using UnityEngine;
using System.Collections;

public class CooldownBarController : MonoBehaviour {

    private float cd_length = 0;
    private float remaining_time = 0;
    private bool cooling_down = false;
    private float y_scale = 0;

	// Use this for initialization
	void Start () {
        gameObject.transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (cooling_down)
        {
            remaining_time -= Time.deltaTime;
            y_scale = (remaining_time / cd_length) * 0.75f;
            gameObject.transform.localScale = new Vector3(transform.localScale.x, y_scale, transform.localScale.z);

            if(remaining_time <= 0)
            {
                cooling_down = false;
                remaining_time = 0;
                gameObject.transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            }
        }
	}

    public void setCdLength(float len)
    {
        cd_length = len;
    }

    public void startCooldown()
    {
        // Full length bar is 0.75 y-scale
        gameObject.transform.localScale = new Vector3(transform.localScale.x, 0.75f, transform.localScale.z);
        remaining_time = cd_length;
        cooling_down = true;
    }
}
