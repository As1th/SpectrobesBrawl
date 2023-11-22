using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject KrawlSpawner;
    public GameObject SpawnLociArray;
    public List<GameObject> spawnLoci = new List<GameObject>();
    public List<GameObject> powerupLoci = new List<GameObject>();
    public List<GameObject> PowerupsList = new List<GameObject>();
    public List<GameObject> currentPowerups = new List<GameObject>();
   
    public float score = 0;
    public float ch = 0;
    public float ev = 0;
    public float health;
    public float healthStore;
    public float scoreIncrement;
    public bool upgradeKrawl;
    public int minKrawl;
    public int maxKrawl;
    public bool lost = false;
    public UIBarScript chBar;
    public UIBarScript healthBar;
    public RawImage shieldIcon;
    public RawImage X2Icon;
    public RawImage ultIcon;
    public UIBarScript evBar;
    public GameObject pointCounter;
    public GameObject[] KrawlList;
    public TextMeshProUGUI countdown;
    public AudioSource defeat;
    public AudioSource bgm;
    public List<GameObject> currentKrawl = new List<GameObject>();
    public GameObject defeatPopup;
    public GameObject victoryPopup;
    public TextMeshProUGUI scoreTextDefeatMenu;
    public TextMeshProUGUI wavesTextDefeatMenu;
    public TextMeshProUGUI scoreTextVictoryMenu;
    public AudioSource beepLoop;
    public bool swarm = false;
    public TextMeshProUGUI warningText;
    public GameObject warningIcon;
    int swarmCount;
    public AudioSource beepCountdown;
    public AudioSource alarm;
    int counter = 0;
    public GameObject difficultyMenu;
    public int maxKrawlPerWave;
    public GameObject player;
    public bool randomWaveMode;
    public bool spectrobeSwitchMode;
    public SceneDataSaver data;
    Menu menu;
    public bool displayNPCStats;
    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<Menu>();
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<SceneDataSaver>();
        if (data.gameMode == 1)
        {
            randomWaveMode = true;
            spectrobeSwitchMode = false;
        }
        else if (data.gameMode == 2)
        {
            randomWaveMode = true;
            spectrobeSwitchMode = true;
        }


        if (data.playerSpectrobe != 0)
        {
            var trobe = Instantiate(data.SpectrobeList[data.playerSpectrobe], player.transform.position, player.transform.rotation);

            Camera.main.transform.parent = trobe.transform;
            
            trobe.GetComponent<SpectrobeController>().scripts = this.gameObject;
            Destroy(player);
            player = trobe;
        }
        health = 300;
        for (int i = 0; i < SpawnLociArray.transform.childCount; i++)
        {
            
            spawnLoci.Add(SpawnLociArray.transform.GetChild(i).gameObject);
        }
        minKrawl = 0;
        maxKrawl = 4;
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("l"))
        {
            if (!displayNPCStats)
            {
                displayNPCStats = true;
            }
            else
            {
                displayNPCStats = false;
            }
        }
            if (ch > 50)
        { ch = 50; }

        if (health < 0)
        {
            health = 0;
        }

        if (ev >= 400)
        {
            ev = 400;
            if (!beepLoop.isPlaying)
            {
                beepLoop.Play();
            }
        }
        else
        {
            beepLoop.Stop();
        }
        //evBar.UpdateValue((int)ev, 400);
        chBar.UpdateValue((int)ch, 50);
        healthBar.UpdateValue((int)health, 300);
        if (player.GetComponent<SpectrobeController>().shield)
        {
            shieldIcon.enabled = true;
        }
        else
        {
            shieldIcon.enabled = false;
        }
        if (player.GetComponent<SpectrobeController>().ultimate)
        {
           ultIcon.enabled = true;
        }
        else
        {
            ultIcon.enabled = false;
        }
        if (player.GetComponent<SpectrobeController>().hurtbox.gameObject.GetComponent<TailSwingDamage>().guaranteedCriticalHit)
        {
            X2Icon.enabled = true;
        }
        else
        {
            X2Icon.enabled = false;
        }
        pointCounter.GetComponent<Text>().text = score.ToString();

        if (Input.GetButtonDown("Pause") && !lost)
        {
            if (Time.timeScale == 1)
            {
               menu.pause();
            }
            else
            {
               menu.resume();
            }
        }

        /*
        if (Input.GetButtonDown("1") && !lost)
        { 
        
        }
        */

    }

    void beginGame()
    {
        difficultyMenu.SetActive(false);
        StartCoroutine(
                countDownAndBegin());
       
        player.GetComponent<SpectrobeController>().enabled = true;
    }

    public void startEasy()
    {
        maxKrawlPerWave = 3;
        swarmCount = 4;
        beginGame();

    }
    public void startNormal()
    {
        maxKrawlPerWave = 4;
        swarmCount = 5;
        beginGame();
    }
    public void startHard()
    {
        maxKrawlPerWave = 5;
        swarmCount = 6;
        beginGame();
    }
    IEnumerator setNewWarning(string w, bool permanent)
    {
       
        warningText.text = w;
        warningIcon.SetActive(true);
       

        if (!permanent)
        {
            alarm.Play();
            yield return new WaitForSeconds(5);
            warningText.text = "";
            warningIcon.SetActive(false);
           
        }
        else {
            if (counter < 2)
            {
                alarm.Play();
                counter++;
            }
           
        }
    }

    IEnumerator countDownAndBegin()
    {
        yield return new WaitForSeconds(1f);
        beepCountdown.Play();
        countdown.text = "3";
        yield return new WaitForSeconds(1f);
        beepCountdown.Play();
        countdown.text = "2";
    

        yield return new WaitForSeconds(1f);
        beepCountdown.Play();
        countdown.text = "1";
       
        yield return new WaitForSeconds(1f);
        beepCountdown.pitch = 1.5f;
        beepCountdown.Play();
        countdown.text = "GO!";
       
        yield return new WaitForSeconds(1f);
        countdown.text = "";
        beepCountdown.pitch = 1f;
        InvokeRepeating("spawn", 0, 2);
    }


    public void defeatSequence()
    {
        defeat.Play();
        bgm.Stop();
        beepLoop.enabled = false;
        defeatPopup.SetActive(true);
        scoreTextDefeatMenu.text = score.ToString();
        int waves = (int)(score / 100);
        wavesTextDefeatMenu.text = waves.ToString();
        menu.resume();
    }
    public void victorySequence()
    {
       
        
        beepLoop.enabled = false;
        victoryPopup.SetActive(true);
        scoreTextVictoryMenu.text = score.ToString();
      
        menu.resume();
    }
    public void AddScore()
    {
        score += 10;
        scoreIncrement += 1;
        if (scoreIncrement == 10)
        {
            scoreIncrement = 0;
            upgradeKrawl = true;
        }
    }

    //maybe move this to update instead of ondeath
    //if score <50 and currentkrawl < 2 summon()
    public void spawnKrawl()
    {
      int i = 0;
       //removed
    }

    public void generateAndSummonSetAmountOfTimes(int x)
    {
        int y;
      //  print(x);
        for (y = x; y > 0; y--)
        {
            int i = Random.Range(minKrawl, maxKrawl-2);
            if (randomWaveMode)
            {
                i = Random.Range(0, KrawlList.Length);
            }
            summon(i);
        }
    }

    public void winCheck()
    {
         if (spawnLoci.Count == 0)
        {
            Camera.main.gameObject.transform.parent = null;
            Camera.main.gameObject.GetComponent<Rotate>().enabled = true;
            Camera.main.gameObject.GetComponent<CameraControl>().enabled = false;
           victorySequence();

            player.GetComponent<SpectrobeController>().enabled = false;
            player = null;
        }
    }
    public void spawn()
    {
        int i = 0;
        if (swarm)
        {
            StartCoroutine(setNewWarning("THE KRAWL ARE SWARMING!", true));
            generateAndSummonSetAmountOfTimes(swarmCount-currentKrawl.Count);
            if (upgradeKrawl)
            {
                swarmCount++;
            }
        }
        else if (scoreIncrement < 5)
        {
            
                generateAndSummonSetAmountOfTimes((maxKrawlPerWave-1)-currentKrawl.Count);
            
           
        }
        else {

            generateAndSummonSetAmountOfTimes(maxKrawlPerWave - currentKrawl.Count);

        }
        //if (upgradeKrawl)
        if(false)
        {
            if (maxKrawl <= 18)
            {
                if (!randomWaveMode)
                {
                    StartCoroutine(setNewWarning("STRONGER KRAWL APPROACHING!", false));
                }
                maxKrawl += 2;
                minKrawl += 2;
            }
            else if (maxKrawl != 21)
            {
                if (!randomWaveMode)
                {
                    StartCoroutine(setNewWarning("STRONGER KRAWL APPROACHING!", false));
                }
                maxKrawl = 21;
                minKrawl += 1;
            }
            else
            {
                minKrawl = 6;
                swarm = true;
            }
                

            
            upgradeKrawl = false;
        }

       
    }

    public void summon(int i)
    {
       
        int random = Random.Range(0, spawnLoci.Count);
        var var = Instantiate(KrawlList[i], spawnLoci[random].transform.position, Quaternion.identity);
        currentKrawl.Add(var);

        
    }


}
