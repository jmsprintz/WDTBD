using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class ShipController : MonoBehaviour
{
    public bool ____ShipVars_________;
    public int frames = 0;
    public bool inv = false;
    public static ShipController S;
    public bool ____Movement_________;
    //Controller Vars
    public float leftX;
    public float leftY;
    public float moveX;
    public float moveY;
    public Vector3 movement;

    // Float to handle speed of movement
    public float speed_factor; 
    public float energy_collected = 0;
    Rigidbody rigidbody;
    private List<string> abilities = new List<string>();

    public bool ____TilesAndMats_________;
    // Tiles for chosen abilities panel
    public GameObject blink_tile;
    public GameObject time_slow_tile;
    public GameObject shield_tile;
    public GameObject random_tile;
    public GameObject hex_tile;
    public GameObject fire_trail_tile;
    public GameObject invert_tile;
    public GameObject freeze_tile;
    public GameObject double_shot_tile;
    public GameObject paddle_tile;

    // Handle input on axes
    private float horiz_in;
    private float vert_in;
    private float x_min = -14f;
    private float x_max = 14f;
    private float y_min = -6.2f;
    private float y_max = 9f;

    // Load sprite sheet on start to improve efficiency
    Sprite[] sheet;
    SpriteRenderer sr;

    // HUD Vars
    private GameObject health_bar;
    private GameObject energy_bar;
    public float health = 100;
    public float energy_cap = 100;
    public GameObject player_death;
    private bool already_exploded = false;

    public bool ____CoolDown_________;
    // Cool Down Stuff
    public List<float> coolDowns = new List<float>();
    public List<float> maxCoolDowns = new List<float>();
    int button = -1;

    public bool ____BlinkVars_________;
    // Blink vars
    public Vector3 saved_dir; // Grab direction of movement to blink in
    private bool blinking = false; // Flag to run second frame of blink function
    public float blink_cooldown = 0; // Cooldown after ability
    public float blink_cooldown_length; // Set in editor for easier playtesting
    public int blink_index = -1;
    public GameObject blink_from;
    public GameObject blink_reticle;

    public bool ____TimeSlowVars_________;
    // Time Slow vars
    public float time_slow_active_length; // Length of time slow runs for. Set in editor for easier playtesting
    public float time_slow_cooldown_length; // Set in editor for easier playtesting
    private float time_slow_cooldown = 0; // Actual timer for cooldown
    public float remaining_duration = -1;

    public bool ____LaserVars_________;
    // Shoot laser vars
    public GameObject laser_prefab;
    public int framesBetweenShots = 50;
    public float rightX;
    public float rightY;
    private Vector3 laser_velocity_vector = new Vector3(0, 20);

    public bool ____ShieldVars_________;
    // Activate shield vars
    public GameObject shield_prefab;
    public float shield_active_length;
    public float shield_cooldown_length;
    private float shield_active_remaining_duration = -1;
    private float shield_cooldown = -1;
    private bool shield_active = false;
    private GameObject shield_instance;


    //Hexagon Fire Vars
    public int invFrames = -1;

    public bool hexFire = true;
    public int numOfHexBullets = 6;
    int hexFrames = -1;

    public bool fireTrail = false;
    int fireTrailFrames = -1;
    List<GameObject> fireTrailList = new List<GameObject>();
    public GameObject fireTrail_prefab;

    // Invert vars
    float invx, invy;
    public GameObject inv_reticle;

    //reverse fire Vars
    Vector3 reverseDir;
    bool rev = false;

    //laser freeze Vars
    public static bool freezer = false;

    //Paddle
    public bool paddle = false;

    //Random/Debug Vars
    private EnemySpawner spawner;
    public List<GameObject> items;
    public bool ded = false;


    void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        spawner = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        sheet = Resources.LoadAll<Sprite>("Ship/ship");
        sr = gameObject.GetComponent<SpriteRenderer>();

        health_bar = GameObject.Find("HealthPivot");
        energy_bar = GameObject.Find("EnergyPivot");

        rev = false;
        freezer = false;
        foreach (Transform child in transform)
        {
            items.Add(child.gameObject);
        }
        items[0].SetActive(!paddle);
        items[1].SetActive(paddle);
    }

 
	// Update is called once per frame
	void FixedUpdate () {
        frames++;

        if(health <= 0)
        {
            ded = true;
            PlayerPrefs.SetInt("Last", Skeeps.S.curScore);
            //Sprite[] explosions = Resources.LoadAll<Sprite>("Ship/explosion");
            //sr.sprite = explosions[0];
            if (!already_exploded)
            {
                GameObject death = Instantiate(player_death, transform.position, Quaternion.identity) as GameObject;
                already_exploded = true;
            }
            rigidbody.velocity = new Vector3(0, 0);
            Invoke("RestartLevel", 5f);
            return;
        }
        //////////New Movement////////////////////
        var inputDevice = InputManager.ActiveDevice;
        leftX = inputDevice.LeftStickX;
        leftY = inputDevice.LeftStickY;

        horiz_in = leftX;
        vert_in = leftY;

        if (horiz_in < 0) switchSprite(1);
        else if (horiz_in > 0) switchSprite(2);
        else switchSprite(0);

        movement = new Vector3(horiz_in, vert_in, 0f);
        rigidbody.velocity = movement * speed_factor;

        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, x_min, x_max), Mathf.Clamp(rigidbody.position.y, y_min, y_max));

        /* ////////Old Movement///////////////
        horiz_in = Input.GetAxis("Horizontal");
        vert_in = Input.GetAxis("Vertical");

        if (horiz_in < 0) switchSprite(1);
        else if (horiz_in > 0) switchSprite(2);
        else switchSprite(0);

        Vector3 movement = new Vector3(horiz_in, vert_in, 0f);
        rigidbody.velocity = movement * speed_factor;

        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, x_min, x_max), Mathf.Clamp(rigidbody.position.y, y_min, y_max));

        */

        // Execute abilities ////////////////////////////////////////
        // X360 Controls
        //*
        if ( Input.GetAxis("rightTrigger") == 1 && abilities.Count > 0) button = 0;
        if (Input.GetAxis("leftTrigger") == 1 && abilities.Count > 1) button = 1;
        if (Input.GetKeyDown(KeyCode.Joystick1Button5) && abilities.Count > 2) button = 2;
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) && abilities.Count > 3) button = 3;
        //*/


        // PS4 Controls
        /*
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && abilities.Count > 0) button = 0;
        if (Input.GetKeyDown(KeyCode.JoystickButton6) && abilities.Count > 1) button = 1;
        if (Input.GetKeyDown(KeyCode.JoystickButton5) && abilities.Count > 2) button = 2;
        if (Input.GetKeyDown(KeyCode.JoystickButton4) && abilities.Count > 3) button = 3;
        //*/

        if (button != -1)
        {
            if (coolDowns[button] <= 0)
            {
                performAbility(abilities[button]);
                CD.S.startCooldown(button);
                coolDowns[button] = maxCoolDowns[button];
            }
            button = -1;
        }
        
        //shoots only if pointing in a direction and only shoots a set rate
        if(frames%framesBetweenShots == 0 && !shield_active)
        {
            rightX = inputDevice.RightStickX;
            rightY = inputDevice.RightStickY;
            Vector2 norm = new Vector2(rightX, rightY);
            norm = norm.normalized;
            rightX = norm.x;
            rightY = norm.y;
            if (hexFire)
            {
                float initAngle = Mathf.PI / 2;
                float hexAngle = 2 * Mathf.PI / numOfHexBullets;
                for (int i = 0; i < numOfHexBullets; i++)
                {
                    float finalAngle = (initAngle + hexAngle * i);
                    shootLaser(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));
                }

            }
            
            else if (rightX !=0 && rightY != 0)
            {

                if (rev)
                {
                    float shot = 5;
                    float initAngle = Mathf.Atan2(rightX, rightY) - Mathf.PI /2;
                    float hexAngle =  Mathf.PI / 2 / shot;
                    for (int i = 0; i < shot; i++)
                    {
                        float finalAngle = (initAngle + hexAngle * (i - 2));
                        shootLaser(Mathf.Cos(finalAngle), -Mathf.Sin(finalAngle));
                    }

                }
                else
                shootLaser(rightX, rightY);
              
            }
        }

        if (horiz_in != 0 && vert_in != 0) {
            saved_dir = new Vector3(horiz_in, vert_in) * 4f;
        }

        transform.LookAt(transform.position + rigidbody.velocity, Vector3.back);
        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);

        if(paddle)
        {
            transform.rotation = new Quaternion(0, 0, 0,0);

        }
        ////////////////////////////////////////////////////////////

        for (int i = 0; i < coolDowns.Count; i++)
        {
            coolDowns[i] -= Time.fixedDeltaTime;
        }

        // Time Slow
        if (remaining_duration >= 0) remaining_duration -= Time.fixedDeltaTime;
        else if (remaining_duration != -1)
        {
            remaining_duration = -1;
            time_slow_cooldown = time_slow_cooldown_length;
            Time.timeScale = 1f;
            Time.fixedDeltaTime *= 5;
        }

        // Blink 2
        if (blinking) blinkSecondFrame();

        //Invincible
        if(frames == invFrames + 250 && invFrames != -1)
        {
            invFrames = -1;
            inv = false;
            OffsetScroller.S.ExitInv();
        }

        if (frames == hexFrames + 250 && hexFrames != -1)
        {
            hexFrames = -1;
            hexFire = false;
            framesBetweenShots = 15;
        }

        if(fireTrail)
        {
            DropFire();
        }

        if (frames == fireTrailFrames + 250 && fireTrailFrames != -1)
        {
            fireTrailFrames = -1;
            fireTrail = false;
            for(int i = 0; i<fireTrailList.Count; i++)
            {
                Destroy(fireTrailList[i]);
            }
            fireTrailList.Clear();
        }

        if (shield_active)
        {
            shield_active_remaining_duration -= Time.deltaTime;
            if(shield_active_remaining_duration <= 0)
            {
                speed_factor /= 2;
                shield_active = false;
                Destroy(shield_instance);
            }
        }
        items[0].SetActive(!paddle);
        items[1].SetActive(paddle);

        ///////////////////////Debug Tools, Delete for final//////////////////////
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            energy_collected = energy_cap;
            spawner.doEndOfWaveProcessing();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.SetInt("High", 0);
            PlayerPrefs.SetInt("Last", 0);
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            paddle = !paddle;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rev = !rev;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inv = !inv;
            if (inv)
            {
                OffsetScroller.S.EnterInv();
            }
            else
            {
                OffsetScroller.S.ExitInv();
            }
        }


    }

    public void switchSprite(int spriteNum)
    {
       // sr.sprite = sheet[spriteNum];
    }

    public void addAbility(string abl)
    {
        abilities.Add(abl);
        GameObject slot = GameObject.Find("Abs" + abilities.Count);
        coolDowns.Add(-1);
        GameObject tl_test = Instantiate(blink_tile, slot.transform.position, Quaternion.identity) as GameObject;
        switch (abl)
        {
            case "Blink":
                tl_test = Instantiate(blink_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(blink_cooldown_length);
                GameObject bl_reticle = Instantiate(blink_reticle, transform.position, Quaternion.identity) as GameObject;
                break;
            case "Time Slow":
                tl_test = Instantiate(time_slow_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(time_slow_cooldown_length + time_slow_active_length);
                break;
            case "Shield":
                tl_test = Instantiate(shield_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(shield_cooldown_length + shield_active_length);
                break;
            case "Invincible":
                tl_test = Instantiate(random_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(25f);
                break;
            case "Hex Fire":
                tl_test = Instantiate(hex_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(15f);
                break;
            case "Fire Trail":
                tl_test = Instantiate(fire_trail_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(15f);
                break;
            case "Invert":
                tl_test = Instantiate(invert_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(5f);
                GameObject inv_ret = Instantiate(inv_reticle, transform.position, Quaternion.identity) as GameObject;
                break;
            case "Freeze":
                tl_test = Instantiate(freeze_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(15f);
                break;
            case "Double Shot":
                tl_test = Instantiate(double_shot_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(15f);
                break;
            case "Paddle":
                tl_test = Instantiate(paddle_tile, slot.transform.position, Quaternion.identity) as GameObject;
                maxCoolDowns.Add(15f);
                break;
            default:
                break;
        }
        tl_test.transform.parent = slot.transform;
        tl_test.transform.localPosition = Vector3.back * 5f;
        //CD.S.TurnOn(coolDowns.Count - 1);
        CD.S.initialize(coolDowns.Count - 1, maxCoolDowns[coolDowns.Count - 1]);
    }

    void performAbility(string abl)
    {
        switch (abl)
        {
            case "Blink":
                blinkFirstFrame();
                break;
            case "Time Slow":
                timeSlow();
                break;
            case "Shield":
                activateShield();
                break;
            case "Hex Fire":
                UseHex();
                break;
            case "Fire Trail":
                UseFireTrail();
                break;
            case "Invert":
                Invert();
                break;
            case "Double Shot":
                ReverseFire();
                break;
            case "Freeze":
                Freeze();
                break;
            case "Paddle":
                UsePaddle();
                break;
            case "Invincible":
                UseInvincible();
                break;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.name == "TurretLaser(Clone)")
        {
            if (paddle)
            {
                Vector3 pos = coll.gameObject.transform.position;
                Vector2 norm;
                norm.x = pos.x - transform.position.x;
                norm.y = pos.y - transform.position.y;
                norm = norm.normalized;
                coll.gameObject.SendMessage("Paddle", norm);
                return;
            }
            if (!inv)
                TakeDamage(5);
            Destroy(coll.gameObject);
        }
       
        if(coll.gameObject.name == "Energy(Clone)")
        {
            EnergyController energy_contrl = coll.gameObject.GetComponent<EnergyController>();
            CollectEnergy(energy_contrl.energy_val);
            coll.gameObject.GetComponent<AudioSource>().Play();
            Destroy(coll.gameObject);
        }

        if(coll.gameObject.name == "Diver(Clone)")
        {
            
            int diverDamage = 10; //Change for balancing
            if(!inv)
            {
                TakeDamage(diverDamage);
            }
            coll.gameObject.SendMessage("Ship");
        }
        if (coll.gameObject.name == "Turret(Clone)")
        {
            int diverDamage = 10; //Change for balancing
            if (!inv)
            {
                TakeDamage(diverDamage);
            }
            coll.gameObject.SendMessage("Ship");
        }
        if(coll.gameObject.name == "orb(Clone)")
        {
            if (paddle)
            {
                Vector3 pos = coll.gameObject.transform.position;
                Vector2 norm;
                norm.x = pos.x - transform.position.x;
                norm.y = pos.y - transform.position.y;
                norm = norm.normalized;
                coll.gameObject.SendMessage("Paddle", norm);
                return;
            }
            coll.gameObject.SendMessage("Ship");
        }
        if (coll.gameObject.name == "Spinner(Clone)")
        {
            coll.gameObject.SendMessage("Ship");
        }
        if (coll.gameObject.name == "Sprinter(Clone)")
        {
            coll.gameObject.SendMessage("Ship");
            TakeDamage(10);
        }
    }

    // Teleport in the direction of movement
    void blinkFirstFrame()
    {
        //saved_dir = new Vector3(horiz_in, vert_in);
        blinking = true;
        GameObject blinkFrom = Instantiate(blink_from, transform.position, Quaternion.identity) as GameObject;
    }

    void blinkSecondFrame()
    {
        transform.position += saved_dir;
        blinking = false;
        blink_cooldown = blink_cooldown_length;
    }

    // Slow time down by half for limited time
    void timeSlow()
    {
        remaining_duration = time_slow_active_length;
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime /= 5;
    }

    // Fire basic laser weapon
    void shootLaser(float x, float y)
    {
        Vector3 input = new Vector3(x, y);
        input *= 20;
        Vector3 offset = new Vector3(transform.position.x + (.2f * x), transform.position.y + (.2f * y));
        reverseDir.x = -(input.x);
        reverseDir.y = -(input.y);

       
        //if (rev == true)
        if(false)
        {
           GameObject laser_clone2 = Instantiate(laser_prefab, offset, Quaternion.identity) as GameObject;
            laser_clone2.GetComponent<Rigidbody>().velocity = reverseDir;
        }
        else
        {

            GameObject laser_clone = Instantiate(laser_prefab, offset, Quaternion.identity) as GameObject;
            laser_clone.GetComponent<Rigidbody>().velocity = input;
        }
    }

    // Activate shield that can take a limited number of hits
    void activateShield()
    {
        shield_active = true;
        speed_factor *= 2;
        shield_instance = Instantiate(shield_prefab, rigidbody.position, Quaternion.identity) as GameObject;
        shield_instance.transform.parent = transform;
        shield_active_remaining_duration = shield_active_length;
    }

    void UseInvincible()
    {
        inv = true;
        invFrames = frames;
        OffsetScroller.S.EnterInv();
    }

    void UseHex()
    {
        hexFire= true;
        framesBetweenShots = 25;
        hexFrames = frames;
    }

    void UseFireTrail()
    {
        fireTrail = true;
        fireTrailFrames = frames;
    }

    void DropFire()
    {

        Vector3 place = transform.position;
        place.z = transform.position.z + 1;
        GameObject laser_clone = Instantiate(fireTrail_prefab, place, Quaternion.identity) as GameObject;
        fireTrailList.Add(laser_clone);
    }

    void Invert()
    {
        invx = -(transform.position.x);
        invy = -(transform.position.y);
        transform.position = new Vector3(invx, invy, 0f);
    }

    void ReverseFire()
    {
        rev = true;
        Invoke("nonrev", 5f);
    }

    void nonrev()
    {
        rev = false;
    }

    void Freeze()
    {
        freezer = true;
        Invoke("unfreeze", 1.5f);
    }

    void unfreeze()
    {
        freezer = false;
    }

    void UsePaddle()
    {
        paddle = true;
        Invoke("PaddleOff", 5);
    }

    void PaddleOff()
    {
        paddle = false;
    }



    public void TurretLaser()//This is for if the turret laser collide with you
    {
        if(!inv)
            TakeDamage(5);
    }

    void TakeDamage(int dmg)
    {
        if (!shield_active && !inv)
        {
            gameObject.GetComponent<AudioSource>().Play();
            // Health pivot full size is 0.38 wide
            health -= dmg;
            float pivot_x_scale = (health / 100f) * 0.38f;
            if (pivot_x_scale < 0f) pivot_x_scale = 0f;
            health_bar.transform.localScale = new Vector3(pivot_x_scale, health_bar.transform.localScale.y, health_bar.transform.localScale.z);
        }
    }

    void CollectEnergy(float eng)
    {
        energy_collected += eng;
        float pivot_x_scale = (energy_collected / energy_cap) * 0.38f;
        if (pivot_x_scale > 0.38) pivot_x_scale = 0.38f;
        energy_bar.transform.localScale = new Vector3(pivot_x_scale, energy_bar.transform.localScale.y, energy_bar.transform.localScale.z);
    }

    void RestartLevel()
    {
        Application.LoadLevel("splash");
    }

    public void Beam()
    {
        if (!inv)
        {
            TakeDamage(5);
        }
    }
}
