using UnityEngine;
using System.Collections;

public class EnergyController : MonoBehaviour {

    public float energy_val;

	// Use this for initialization
	void Start () {
        energy_val = Random.Range(5f, 25f);
        energy_val = Mathf.Floor(energy_val);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
