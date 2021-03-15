using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Object LongSwordPre;
    private Object SpearPre;
    private Object DaggerPre;
    private Object HarmPre;
    private Object HealPre;
    private Object EnemyPre;
    private Object PlayerPre;

    [Header("SpawnPoints")]
    public Transform[] spearSpawn;
    public Transform[] longSwordSpawn;
    public Transform[] daggerSpawn;
    public Transform[] harmSpawn;
    public Transform[] healSpawn;
    public Transform[] enemySpawn;
    public Transform playerSpawn;

    [Header("Spawn Timing")]
    public float playerSpawnDelay = 5;
    [SerializeField]
    private float nextPlayerSpawn;
    public float enemySpawnDelay = 5;
    [SerializeField]
    private float nextEnemySpawn;
    public float weaponSpawnDelay = 5;
    [SerializeField]
    private float nextWeaponSpawn;
    public float buffSpawnDelay = 5;
    [SerializeField]
    private float nextBuffSpawn;
    public float debuffSpawnDelay = 5;
    [SerializeField]
    private float nextDebuffSpawn;

    [Header("Spawn max numbers")]
    public float maxSpears = 1;
    public float maxSwords = 1;
    public float maxDaggers = 1;
    public float maxEnemies = 1;
    public float maxBuffs = 1;
    public float maxDebuffs = 1;


    //arrays to hold object counts
    GameObject[] spears;
    GameObject[] longSwords;
    GameObject[] daggers;
    GameObject[] harms;
    GameObject[] heals;
    GameObject[] enemies;
    GameObject[] players;

    private int rnd;

    public static GameManager instance = null;

    //Singleton  only one instance
    void Awake()
    {
        if (GameManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        //load prefabs into objects
        LongSwordPre = Resources.Load("Prefabs/PULongsword");
        DaggerPre = Resources.Load("Prefabs/PUDagger");
        SpearPre = Resources.Load("Prefabs/PUSpear");
        HealPre = Resources.Load("Prefabs/HealPre");
        HarmPre = Resources.Load("Prefabs/HarmPre");
        EnemyPre = Resources.Load("Prefabs/Vampire");
        PlayerPre = Resources.Load("Prefabs/Player");


    }

    // Update is called once per frame
    private void Update()
    {

    }


    void FixedUpdate()
    {
        //get a count of objects
        spears = GameObject.FindGameObjectsWithTag("Spear");
        daggers = GameObject.FindGameObjectsWithTag("Dagger");
        longSwords = GameObject.FindGameObjectsWithTag("LongSword");
        harms = GameObject.FindGameObjectsWithTag("Harm");
        heals = GameObject.FindGameObjectsWithTag("Heal");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Player");

        //spawn the player
        if (players.Length == 0)//if there is not a player
        {
            GameObject Player = (GameObject)Instantiate(PlayerPre, playerSpawn.position, playerSpawn.rotation);
        }
        //weapons
        if (Time.time > nextWeaponSpawn)
        {
            //Spear
            if (spears.Length < maxSpears)
            {
                rnd = Random.Range(0, spearSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new spear at random spear spawn point
                GameObject Spear = (GameObject)Instantiate(SpearPre, spearSpawn[rnd].position, spearSpawn[rnd].rotation);
            }
            //Dagger
            if (daggers.Length < maxDaggers)
            {
                rnd = Random.Range(0, daggerSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new dagger at random dagger spawn point
                GameObject Dagger = (GameObject)Instantiate(DaggerPre, daggerSpawn[rnd].position, daggerSpawn[rnd].rotation);
            }
            //Sword
            if (longSwords.Length < maxSwords)
            {
                rnd = Random.Range(0, longSwordSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new sword at random sword spawn point
                GameObject LongSword = (GameObject)Instantiate(LongSwordPre, longSwordSpawn[rnd].position, longSwordSpawn[rnd].rotation);
            }

            nextWeaponSpawn = Time.time + weaponSpawnDelay;//reset timer for next spawn
        }

        //debuffs
        if (Time.time > nextDebuffSpawn)
        {
            if (harms.Length < maxDebuffs)
            {
                rnd = Random.Range(0, harmSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new harm pickup at the harm spawn point
                GameObject Harm = (GameObject)Instantiate(HarmPre, harmSpawn[rnd].position, harmSpawn[rnd].rotation);
            }
            nextDebuffSpawn = Time.time + debuffSpawnDelay;//reset timer for next spawn
        }
        //buffs
        if (Time.time > nextBuffSpawn)
        {
            if (heals.Length < maxBuffs)
            {
                rnd = Random.Range(0, healSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new heal pickup at random heal spawn point
                GameObject Heal = (GameObject)Instantiate(HealPre, healSpawn[rnd].position, healSpawn[rnd].rotation);
            }
            nextBuffSpawn = Time.time + buffSpawnDelay;//reset timer for next spawn
        }
        //enemies
        if (Time.time > nextEnemySpawn)
        {
            if (enemies.Length < maxEnemies)
            {
                rnd = Random.Range(0, enemySpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                GameObject Enemy = (GameObject)Instantiate(EnemyPre, enemySpawn[rnd].position, enemySpawn[rnd].rotation);//spawn enemy at enemy spawn point
            }
            nextEnemySpawn = Time.time + enemySpawnDelay;//reset timer for next spawn
        }

    }
}