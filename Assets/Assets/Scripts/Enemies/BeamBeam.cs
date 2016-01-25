using UnityEngine;
using System.Collections;

public class BeamBeam : MonoBehaviour
{
    bool ship = true;

    // Use this for initialization
    void Start()
    {
        Invoke("End", 2);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   void End()
    {
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "Ship")
        {
            if(ship)
            {
                ShipController.S.Beam();
                ship = false;
            }
            return;
        }
        if(coll.gameObject.name != "Energy(Clone)")
        {
            coll.gameObject.SendMessage("Beam");

        }
    }
}
