using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

    public float active_length = 6f;
    private bool swapped = false;

	// Use this for initialization
	void Start () {
        transform.parent = GameObject.Find("Ship").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = transform.parent.position;
        active_length -= Time.deltaTime;

        if(active_length < 1.5 && !swapped)
        {
            swapped = true;
            Sprite[] sprite = Resources.LoadAll<Sprite>("Ship/laser");
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = sprite[15];
        }
        //if (active_length <= 0) Destroy(gameObject);
	}

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject != null) coll.gameObject.SendMessage("Shield");
    }
}
