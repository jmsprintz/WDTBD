using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModSelector : MonoBehaviour
{

    private EnemySpawner es;
    private ShipController ship_controller;

    public GameObject UI;

    public GameObject turret_tile;
    public GameObject sprinter_tile;
    public GameObject spinner_tile;
    public GameObject missle_tile;
    public GameObject swapSticks_tile;
    public GameObject inverseWorld_tile;


    private List<string> mods = new List<string>();
    List<string> chosen;
    List<GameObject> active_tiles;
    public bool presenting = false;

    public static ModSelector S;

    void Awake()
    {
        S = this;
    }
    // Use this for initialization
    void Start()
    {
        es = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        ship_controller = GameObject.Find("Ship").GetComponent<ShipController>();

        // Add all implemented abilities to list
        mods.Add("turret");
        mods.Add("sprinter");
        mods.Add("spinner");
        mods.Add("missle");
        mods.Add("swapSticks");
        mods.Add("inverseWorld");

        UI.SetActive(false);
    }

    void Update()
    {
        if (presenting)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
              //  ship_controller.addAbility(chosen[0], 0);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
             //   ship_controller.addAbility(chosen[1], 1);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
           //     ship_controller.addAbility(chosen[2], 2);
                closeAbilityPresentation();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
          //      ship_controller.addAbility(chosen[3], 3);
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

        for (int i = 0; i < 4; i++)
        {
            int chosen_ability = Mathf.FloorToInt(Random.Range(0f, mods.Count - 0.01f));
            if (chosen.Contains(mods[chosen_ability]))
            {
                i--;
                continue;
            }
            chosen.Add(mods[chosen_ability]);
        }

        presentAbilities(chosen);
    }

    void presentAbilities(List<string> chosen)
    {
        UI.gameObject.SetActive(true);
        active_tiles = new List<GameObject>();
        //gameObject.transform.Translate(Vector3.right * 4f);
        GameObject selected_slot;
        for (int i = 1; i <= 4; i++)
        {
            selected_slot = GameObject.Find("Slot" + i);
            switch (chosen[i - 1])
            {
                case "turret":
                    GameObject bl_tl = Instantiate(turret_tile, selected_slot.transform.position, Quaternion.identity) as GameObject;
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
}
