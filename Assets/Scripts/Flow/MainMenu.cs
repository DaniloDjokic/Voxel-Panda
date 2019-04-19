using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxelPanda.Flow;
using VoxelPanda.Score;

public class MainMenu : MonoBehaviour {
	public GameObject soundOnIcon;
	public GameObject soundOffIcon;
    public UISFX uiSFX;
    private const string isSoundOnKey = "IsSoundOn";
    private const string muteAllEvent = "Mute_All";
    private const string unmuteAllEvent = "Unmute_All";
	private const string stopMenuMusic = "Stop_MenuMusic";

    private void Start()
    {
        ChangeSound(GetSoundOn());
    }

    public void StartGame() {
		AkSoundEngine.PostEvent(stopMenuMusic, Camera.main.gameObject);
        uiSFX.PlayUIClick();

		SceneManager.LoadScene(1);
	}

	public void ToggleSound() {
        uiSFX.PlayUIClick();
        bool currentSoundOn = GetSoundOn();
		bool newSoundOn = !currentSoundOn;
		ChangeSound(newSoundOn);
	}

	public bool GetSoundOn() {
		return PlayerPrefs.GetInt(isSoundOnKey) == 1;
	}

	public void ChangeSound(bool isSoundOn) {
		soundOnIcon.SetActive(isSoundOn);
		soundOffIcon.SetActive(!isSoundOn);
		if (isSoundOn)
        {
            AkSoundEngine.PostEvent(unmuteAllEvent, gameObject);
        } else
        {
            AkSoundEngine.PostEvent(muteAllEvent, gameObject);
        }
        PlayerPrefs.SetInt(isSoundOnKey, isSoundOn ? 1 : 0);
	}

	public void ResetTutorial()
	{
        uiSFX.PlayUIClick();
        Tutorial.ResetTutorial();
	}

	public void ResetHighScore()
	{
        uiSFX.PlayUIClick();
        ScoreCalculator.ResetHighScore();
	}

}
