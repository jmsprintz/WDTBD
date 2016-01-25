using UnityEngine;
using System.Collections;

public class Skeeps : MonoBehaviour {
    public static Skeeps S;

    public int curScore;
    public int lastScore = 0;
    public int highScore = 0;

    public GameObject sGO;
    public GameObject lGO;
    public GameObject hGO;

    void Awake()
    {
        S = this;
        if(PlayerPrefs.HasKey("High"))
        {
            highScore = PlayerPrefs.GetInt("High");
        }
        if (PlayerPrefs.HasKey("Last"))
        {
            lastScore = PlayerPrefs.GetInt("Last");
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (curScore < 10)
            sGO.GetComponent<GUIText>().text = "Score:  00" + curScore;
        else if (curScore < 100)
            sGO.GetComponent<GUIText>().text = "Score:  0" + curScore;
        else
            sGO.GetComponent<GUIText>().text = "Score:  " + curScore;

        if (curScore > highScore && !ShipController.S.ded )
        {
            highScore = curScore;
            PlayerPrefs.SetInt("High",curScore);
        }
            

        if (highScore < 10)
            hGO.GetComponent<GUIText>().text = "High:    00" + highScore;
        else if (curScore < 100)
            hGO.GetComponent<GUIText>().text = "High:    0" + highScore;
        else
            hGO.GetComponent<GUIText>().text = "High:    " + highScore;

        if (lastScore < 10)
            lGO.GetComponent<GUIText>().text = "Last:    00" + lastScore;
        else if (curScore < 100)
            lGO.GetComponent<GUIText>().text = "Last:    0" + lastScore;
        else
            lGO.GetComponent<GUIText>().text = "Last:     " + lastScore;

    }
}
