using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
	public bool introScene;
	public GameObject sceneDataPrefab;
	public SceneDataSaver data;
	public TextMeshProUGUI initialDialog;
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
		}
	}

	public void resummonSpectrobeIntro(int i)
	{
		var trobe = Instantiate(data.SpectrobeList[i], gm.player.transform.position, gm.player.transform.rotation);
		Camera.main.transform.parent.transform.parent = trobe.transform;
		trobe.GetComponent<SpectrobeController>().scripts = this.gameObject;
		Destroy(gm.player);
		gm.player = trobe;
		gm.player.GetComponent<SpectrobeController>().enabled = true;
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
