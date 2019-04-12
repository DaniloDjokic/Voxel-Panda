using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject soundOnIcon;
	public GameObject soundOffIcon;
	private const string isSoundOnKey = "IsSoundOn";

	public void StartGame() {
		SceneManager.LoadScene(1);
	}

	public void ToggleSound() {
		bool currentSoundOn = GetSoundOn();
		bool newSoundOn = !currentSoundOn;
		ChangeSound(newSoundOn);
		soundOnIcon.SetActive(newSoundOn);
		soundOffIcon.SetActive(!newSoundOn);
	}

	public bool GetSoundOn() {
		return PlayerPrefs.GetInt(isSoundOnKey) == 1;
	}

	public void ChangeSound(bool isSoundOn) {
		PlayerPrefs.SetInt(isSoundOnKey, isSoundOn ? 1 : 0);
	}
}
