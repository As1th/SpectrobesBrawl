using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
	public GameObject BasicDmgBar;
	public GameObject CHDmgBar;
	public GameObject speedBar;
	public Image styleIcon;
	public bool introScene;
	public GameObject sceneDataPrefab;
	public SceneDataSaver data;
	public TextMeshProUGUI initialDialog;
	public TextMeshProUGUI trobeName;
	public GameObject pauseMenu;
	public AudioSource bgm;
	public AudioSource beepLoop;
	public AudioSource UI;
	GameManager gm;

	public void Start()
    {
		gm = GetComponent<GameManager>();
		var dataHolder = GameObject.FindGameObjectWithTag("Data");

		if (dataHolder != null)
		{
			data = dataHolder.GetComponent<SceneDataSaver>();
		}
		else
		{ 
			data = Instantiate(sceneDataPrefab,transform.position,Quaternion.identity).GetComponent<SceneDataSaver>();
		}
		
		UI.ignoreListenerPause=true;

		if (introScene)
		{
			if (data.playerSpectrobe == 1)
			{
				resummonSpectrobeIntro(data.playerSpectrobe);
			}
			UpdateStats();
		}
	}
	public void UpdateStats()
	{
		trobeName.text = data.SpectrobeList[data.playerSpectrobe].name;
		styleIcon.sprite = data.SpectrobeList[data.playerSpectrobe].GetComponent<DisplayStats>().style;
		for (int i = 0; i < 6; i++)
		{
			BasicDmgBar.transform.GetChild(i).gameObject.SetActive(false);
			CHDmgBar.transform.GetChild(i).gameObject.SetActive(false);
			speedBar.transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = 0; i < data.SpectrobeList[data.playerSpectrobe].GetComponent<DisplayStats>().BasicDmg; i++)
		{
			BasicDmgBar.transform.GetChild(i).gameObject.SetActive(true);
		}
		for (int i = 0; i < data.SpectrobeList[data.playerSpectrobe].GetComponent<DisplayStats>().CHDmg; i++)
		{
			CHDmgBar.transform.GetChild(i).gameObject.SetActive(true);
		}
		for (int i = 0; i < data.SpectrobeList[data.playerSpectrobe].GetComponent<DisplayStats>().Speed; i++)
		{
			speedBar.transform.GetChild(i).gameObject.SetActive(true);
		}

	}
	public void resummonSpectrobeIntro(int i)
	{
		var trobe = Instantiate(data.SpectrobeList[i], gm.player.transform.position, gm.player.transform.rotation);
		Camera.main.transform.parent.transform.parent = trobe.transform;
		trobe.GetComponent<SpectrobeController>().scripts = this.gameObject;
		Destroy(gm.player);
		gm.player = trobe;
		gm.player.GetComponent<SpectrobeController>().EVCost = 0;
		gm.player.GetComponent<SpectrobeController>().CHCost = 0;
		gm.player.GetComponent<SpectrobeController>().enabled = true;
		UpdateStats();
	}

	public void selectNextSpectrobe()
	{
		data.playerSpectrobe++;
		if (data.playerSpectrobe >= data.SpectrobeList.Length)
		{
			data.playerSpectrobe = 0;
		}
		
		resummonSpectrobeIntro(data.playerSpectrobe);
	}

	public void selectPreviousSpectrobe()
	{
		data.playerSpectrobe--;
		if (data.playerSpectrobe <=-1)
		{
			data.playerSpectrobe = data.SpectrobeList.Length-1;
		}
		
		resummonSpectrobeIntro(data.playerSpectrobe);
	}

	public void startInfiniteWaveMode()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		data.gameMode = 0;
		SceneManager.LoadScene("InfiniteWaveMode", LoadSceneMode.Single);
	}
	public void startInfiniteRandomMode()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		data.gameMode = 1;
		SceneManager.LoadScene("InfiniteWaveMode", LoadSceneMode.Single);
	}
	public void startIntro()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		SceneManager.LoadScene("Intro", LoadSceneMode.Single);
	}
	public void restart()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
    public void pause()
    {
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
		//beepLoop.volume=0;
	//	bgm.Pause();
		AudioListener.pause = true;
		
	}
	public void resume()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		//bgm.UnPause();
		//beepLoop.volume = 1;
		AudioListener.pause = false;
	}
	public void initInfiniteWaveDialogText()
	{
		initialDialog.text = "BATTLE INFINITE WAVES OF INCREASINGLY DIFFICULT KRAWL AND GET THE HIGHEST SCORE!";
	}
	public void initQuitDialogText()
	{
		initialDialog.text = "EXIT THE GAME AND RETURN TO THE DESKTOP.";
	}
	public void initInfiniteRandomDialogText()
	{
		initialDialog.text = "BATTLE INFINITE WAVES OF COMPLETELY RANDOM KRAWL AND GET THE HIGHEST SCORE!";
	}
	public void initDefaultDialogText()
	{
		initialDialog.text = "^ SELECT AN OPTION ^";
	}
}
