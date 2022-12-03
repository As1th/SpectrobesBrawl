using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
	public TextMeshProUGUI initialDialog;
	public GameObject pauseMenu;
	public AudioSource bgm;
	public AudioSource beepLoop;
	public AudioSource UI;

	public void Start()
    {
		UI.ignoreListenerPause=true;
	}

    public void startInfiniteWaveMode()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		SceneManager.LoadScene("InfiniteWaveMode", LoadSceneMode.Single);
	}
	public void startInfiniteRandomMode()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		SceneManager.LoadScene("InfiniteRandomMode", LoadSceneMode.Single);
	}
	public void startIntro()
	{
		Time.timeScale = 1;
		AudioListener.pause = false;
		SceneManager.LoadScene("Intro", LoadSceneMode.Single);
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
