using UnityEngine;
using System.Collections;

public class FireTrail : MonoBehaviour
{

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.position.y > 18) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        coll.gameObject.SendMessage("Laser");
        //Destroy(gameObject);
    }
}
