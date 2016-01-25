using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilitySelectionController : MonoBehaviour {

    private EnemySpawner es;
    private ShipController ship_controller;

    public GameObject blink_tile;
    public GameObject time_slow_tile;
    public GameObject shield_tile;
    public GameObject invincible_tile;
    public GameObject hex_tile;
    public GameObject fire_trail_tile;
    public GameObject invert_tile;
    public GameObject freeze_tile;
    public GameObject double_shot_tile;
    public GameObject paddle_tile;

    public static AbilitySelectionController S;


    private List<string> abilities = new List<string>();
    List<string> chosen;
    List<GameObject> active_tiles;
    public bool presenting = false;
    public bool sliding_in = false;
    public bool sliding_out = false;

    private Rigidbody rigid;

    void Awake()
    {
        S = this;
    }
	// Use this for initialization
	void Start () {
        es = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        ship_controller = GameObject.Find("Ship").GetComponent<ShipController>();
        rigid = gameObject.GetComponent<Rigidbody>();

        // Add all implemented abilities to list
        abilities.Add("Blink");
        abilities.Add("Time Slow");
        abilities.Add("Shield");
        abilities.Add("Invincible");
        abilities.Add("Hex Fire");
        abilities.Add("Fire Trail");
        abilities.Add("Invert");
        abilities.Add("Freeze");
        abilities.Add("Double Shot");
        abilities.Add("Paddle");

    }

    void Update ()
    {
        if (presenting)
        {
            // X360
            //*
            if (Input.GetKeyDown(KeyCode.JoystickButton0))            
            {
                ship_controller.addAbility(chosen[0]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                ship_controller.addAbility(chosen[1]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                ship_controller.addAbility(chosen[2]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                ship_controller.addAbility(chosen[3]);
                closeAbilityPresentation();
            }
            //*/

            // PS4
            /*
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                ship_controller.addAbility(chosen[0]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                ship_controller.addAbility(chosen[1]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                ship_controller.addAbility(chosen[2]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                ship_controller.addAbility(chosen[3]);
                closeAbilityPresentation();
            }
            //*/

        }

        if (sliding_in && rigid.velocity.magnitude < 3)
        {
            rigid.velocity += new Vector3(0.2f, 0);
        }
        else if(sliding_in && transform.position.x >= -13.25)
        {
            sliding_in = false;
            rigid.velocity = new Vector3(0, 0);
            transform.position = new Vector3(-13.25f, transform.position.y);
            presenting = true;
        }

        if(sliding_out && rigid.velocity.magnitude < 3)
        {
            rigid.velocity += new Vector3(-0.2f, 0);
        }
        else if (sliding_out && transform.position.x <= -17.2)
        {
            rigid.velocity = new Vector3(0, 0);
            transform.position = new Vector3(-17.2f, transform.position.y);
            cleanupAbilityPresentation();
        }
    }
	
	public void pickNewAbility()
    {
        // Drain energy bar and double cap for next upgrade
        ship_controller.energy_collected -= ship_controller.energy_cap;
        ship_controller.energy_cap *= 2f;

        es.holdWave();
        chosen = new List<string>();

        for (int i = 0; i < 4; i++)
        {
            int chosen_ability = Mathf.FloorToInt(Random.Range(0f, abilities.Count - 0.01f));
            if (chosen.Contains(abilities[chosen_ability]))
            {
                i--;
                continue;
            }
            chosen.Add(abilities[chosen_ability]);
        }

        presentAbilities(chosen);
    }

    void presentAbilities(List<string> chosen)
    {
        active_tiles = new List<GameObject>();
        //gameObject.transform.Translate(Vector3.right * 4f);
        GameObject selected_slot;
        for(int i = 1; i <= 4; i++){
            selected_slot = GameObject.Find("Slot" + i);
            GameObject tl_test = Instantiate(blink_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
            switch (chosen[i-1])
            {
                case "Blink":
                    tl_test = Instantiate(blink_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Time Slow":
                    tl_test = Instantiate(time_slow_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Shield":
                    tl_test = Instantiate(shield_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Invincible":
                    tl_test = Instantiate(invincible_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Hex Fire":
                    tl_test = Instantiate(hex_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Fire Trail":
                    tl_test = Instantiate(fire_trail_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Invert":
                    tl_test = Instantiate(invert_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Double Shot":
                    tl_test = Instantiate(double_shot_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Freeze":
                    tl_test = Instantiate(freeze_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                case "Paddle":
                    tl_test = Instantiate(paddle_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    break;
                default:
                    break;
            }
            tl_test.transform.parent = selected_slot.transform;
            tl_test.transform.localPosition = Vector3.back * 5f;
            active_tiles.Add(tl_test);
        }

        sliding_in = true;
    }

    void closeAbilityPresentation()
    {
        //gameObject.transform.Translate(Vector3.left * 4f);
        presenting = false;
        sliding_out = true;
    }
    
    void cleanupAbilityPresentation()
    {
        sliding_out = false;
        es.releaseWave();
        foreach (GameObject tile in active_tiles) Destroy(tile);
    }
}
