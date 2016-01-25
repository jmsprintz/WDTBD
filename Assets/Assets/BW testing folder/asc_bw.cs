using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class asc_bw : MonoBehaviour {

    private EnemySpawner es;
    private ShipController ship_controller;

    public GameObject UI;

    private List<string> abilities = new List<string>();
    public GameObject blink_tile;
    public GameObject time_slow_tile;
    public GameObject shield_tile;
    public GameObject invincible_tile;
    public GameObject hex_tile;
    public GameObject fire_trail_tile;

    private List<string> mods = new List<string>();
    public GameObject turret_tile;
    public GameObject sprinter_tile;
    public GameObject spinner_tile;
    public GameObject missle_tile;
    public GameObject swapSticks_tile;
    public GameObject inverseWorld_tile;


    


    
    List<string> chosen;
    List<string> modsChosen;
    List<GameObject> active_tiles;
    public bool presenting = false;

    public static asc_bw S;

    void Awake()
    {
        S = this;
    }
	// Use this for initialization
	void Start () {
        es = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        ship_controller = GameObject.Find("Ship").GetComponent<ShipController>();

        // Add all implemented abilities to list
        abilities.Add("Blink");
        abilities.Add("Time Slow");
        abilities.Add("Shield");
        abilities.Add("Invincible");
        abilities.Add("Hex Fire");
        abilities.Add("Fire Trail");

        mods.Add("turret");
        mods.Add("sprinter");
        mods.Add("spinner");
        mods.Add("missle");
        mods.Add("swapSticks");
        mods.Add("inverseWorld");

        UI.SetActive(false);
	}

    void Update ()
    {
        if (presenting)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
            //    ship_controller.addAbility(chosen[0],0);
                AddMod(modsChosen[0]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
            //    ship_controller.addAbility(chosen[1],1);
                AddMod(modsChosen[1]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
            //    ship_controller.addAbility(chosen[2],2);
                AddMod(modsChosen[2]);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
           //     ship_controller.addAbility(chosen[3],3);
                AddMod(modsChosen[3]);
                closeAbilityPresentation();
            }
        }
    }
	
	public void pickNewAbility()
    {
        // Drain energy bar and double cap for next upgrade
        ship_controller.energy_collected -= ship_controller.energy_cap;
        ship_controller.energy_cap *= 2f;

        es.holdWave();
        chosen = new List<string>();
        modsChosen = new List<string>();

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

        for (int i = 0; i < 4; i++)
        {
            int chosen_ability = Mathf.FloorToInt(Random.Range(0f,mods.Count - 0.01f));
            if (modsChosen.Contains(mods[chosen_ability]))
            {
                i--;
                continue;
            }
            modsChosen.Add(mods[chosen_ability]);
        }

        presentAbilities(chosen, modsChosen);
    }

    void presentAbilities(List<string> chosen, List<string> modsChosen)
    {
        UI.gameObject.SetActive(true);
        active_tiles = new List<GameObject>();
        //gameObject.transform.Translate(Vector3.right * 4f);
        GameObject selected_slot;
        for(int i = 1; i <= 4; i++){
            selected_slot = GameObject.Find("Slot" + i);
            switch (chosen[i-1])
            {
                case "Blink":
                    GameObject bl_tl = Instantiate(blink_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    bl_tl.transform.parent = selected_slot.transform;
                    bl_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(bl_tl);
                    break;
                case "Time Slow":
                    GameObject ts_tl = Instantiate(time_slow_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    ts_tl.transform.parent = selected_slot.transform;
                    ts_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(ts_tl);
                    break;
                case "Shield":
                    GameObject s_tl = Instantiate(shield_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    s_tl.transform.parent = selected_slot.transform;
                    s_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(s_tl);
                    break;
                case "Invincible":
                    GameObject r_tl = Instantiate(invincible_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    r_tl.transform.parent = selected_slot.transform;
                    r_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(r_tl);
                    break;
                case "Hex Fire":
                    GameObject h_tl = Instantiate(hex_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    h_tl.transform.parent = selected_slot.transform;
                    h_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(h_tl);
                    break;
                case "Fire Trail":
                    GameObject ft_tl = Instantiate(fire_trail_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    ft_tl.transform.parent = selected_slot.transform;
                    ft_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(ft_tl);
                    break;
                default:
                    break;
            }
        }
        for (int i = 1; i <= 4; i++)
        {
            selected_slot = GameObject.Find("modSlot" + i);
            switch (modsChosen[i - 1])
            {
                
                case "turret":
                    GameObject bl_tl = Instantiate(missle_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    bl_tl.transform.parent = selected_slot.transform;
                    bl_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(bl_tl);
                    break;
                case "sprinter":
                    GameObject ts_tl = Instantiate(sprinter_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    ts_tl.transform.parent = selected_slot.transform;
                    ts_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(ts_tl);
                    break;
                case "spinner":
                    GameObject s_tl = Instantiate(spinner_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    s_tl.transform.parent = selected_slot.transform;
                    s_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(s_tl);
                    break;
                case "missle":
                    GameObject r_tl = Instantiate(missle_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    r_tl.transform.parent = selected_slot.transform;
                    r_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(r_tl);
                    break;
                case "swapSticks":
                    GameObject h_tl = Instantiate(swapSticks_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    h_tl.transform.parent = selected_slot.transform;
                    h_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(h_tl);
                    break;
                case "inverseWorld":
                    GameObject ft_tl = Instantiate(inverseWorld_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
                    ft_tl.transform.parent = selected_slot.transform;
                    ft_tl.transform.localPosition = Vector3.back * 5f;
                    active_tiles.Add(ft_tl);
                    break;
                default:
                    break;
            }
        }

        presenting = true;
    }

    void closeAbilityPresentation()
    {
        //gameObject.transform.Translate(Vector3.left * 4f);
        foreach (GameObject tile in active_tiles) Destroy(tile);
        es.releaseWave();
        presenting = false;
        UI.gameObject.SetActive(false);
    }


   void AddMod(string mod)
    {
        switch (mod)
        {
            case "null":
                break;
            case "turret":
                print("turret");
                if (!es.availEnemies.Contains(es.turret_prefab))
                {
                    es.availEnemies.Add(es.turret_prefab);
                }
                break;
            case "sprinter":
                print("sprint");
                if (!es.availEnemies.Contains(es.sprinter_prefab))
                {
                    es.availEnemies.Add(es.sprinter_prefab);
                }
                break;
            case "missle":
                break;
            case "spinner":
                break;
            case "inverseWorld":
                break;
            case "switchSticks":
                break;
        }
    }
}
