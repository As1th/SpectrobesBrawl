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

	public void startInfiniteWaveMode()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("InfiniteWaveMode", LoadSceneMode.Single);
	}
	public void startIntro()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Intro", LoadSceneMode.Single);
	}
    public void pause()
    {
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
		bgm.Pause();
	}
	public void resume()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		bgm.UnPause();
	}
	public void initInfiniteWaveDialogText()
	{
		initialDialog.text = "BATTLE INFINITE KRAWL OF INCREASING DIFFICULTY AND GET THE HIGHEST SCORE!";
	}

	public void initDefaultDialogText()
	{
		initialDialog.text = "^ SELECT AN OPTION ^";
	}
}
