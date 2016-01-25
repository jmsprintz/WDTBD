using UnityEngine;
using System.Collections;

public class CD : MonoBehaviour {
    public static CD S;

    public Material MatOn;
    public Material MatOff;

    public GameObject CD1;
    public GameObject CD2;
    public GameObject CD3;
    public GameObject CD4;

    public GameObject[] CDarray;
    void Awake()
    {
        S = this;
    }
	// Use this for initialization
	void Start () {
        CDarray = new GameObject[8];
        CDarray[0] = CD1;
        CDarray[1] = CD2;
        CDarray[2] = CD3;
        CDarray[3] = CD4;
    }
	
    public void initialize(int i, float len)
    {
        CDarray[i].GetComponent<CooldownBarController>().setCdLength(len);
    }

    public void startCooldown(int i)
    {
        //print("off " + i);
        CDarray[i].GetComponent<CooldownBarController>().startCooldown();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
