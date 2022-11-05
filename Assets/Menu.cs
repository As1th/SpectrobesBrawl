using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
	public TextMeshProUGUI initialDialog;


	public void startInfiniteWaveMode()
	{
		SceneManager.LoadScene("InfiniteWaveMode", LoadSceneMode.Single);
	}
	public void initInfiniteWaveDialogText()
	{
		initialDialog.text = "BATTLE INFINITE KRAWL OF INCREASING DIFFICULTIES AND GET THE HIGHEST SCORE!";
	}

	public void initDefaultDialogText()
	{
		initialDialog.text = "^ SELECT AN OPTION ^";
	}
}
