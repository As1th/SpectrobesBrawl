using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
	public GameObject sceneDataPrefab;
	public SceneDataSaver data;
	public TextMeshProUGUI initialDialog;
	public GameObject pauseMenu;
	public AudioSource bgm;
	public AudioSource beepLoop;
	public AudioSource UI;

	public void Start()
    {
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
		
	}

	public void selectNextSpectrobe()
	{ 
	
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
