﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using VoxelPanda.Flow;

public class AdManager : MonoBehaviour
{
	public string appID = "ca-app-pub-4430923652609758~6882662181";
	public string interstitialAdUnitID = "ca-app-pub-4430923652609758/1794110741";
	public string rewardAdUnitID = "ca-app-pub-4430923652609758/3653987329";
	public string testDeviceID = "D3E3C92E3D91C65EF00DD3BB74883BB9";
	public int minplaySessionWithoutAd = 5;
	public int maxPlaySessionsWithoutAd = 10;
	
	private int currentPlaySessionsWithoutAd = 0;
	private InterstitialAd interstitial;
	private RewardBasedVideoAd rewardBasedVideo;
	public static AdManager instance;
	public DeathController deathController;

	private void Start()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
		MobileAds.Initialize(appID);
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		OpenRewardVideo();
		RegisterRewardEvents();
		InitInterstitial();
	}

	public void OpenRewardVideo()
	{
		AdRequest request = new AdRequest.Builder().AddTestDevice(testDeviceID).Build();
		this.rewardBasedVideo.LoadAd(request, rewardAdUnitID);
	}

	public bool TryToShowRewardVideo()
	{
		if(rewardBasedVideo.IsLoaded())
		{
			rewardBasedVideo.Show();
			return true;
		}
		return false;
	}

	public void TryToShowDeathAd()
	{
		int random = UnityEngine.Random.Range(minplaySessionWithoutAd, maxPlaySessionsWithoutAd);
		if (random < currentPlaySessionsWithoutAd)
		{
			ShowInterstitial();
			currentPlaySessionsWithoutAd = 0;
		} else
		{
			currentPlaySessionsWithoutAd++;
		}

	}

	private void InitInterstitial()
	{
		CreateInterstitial();
		RegisterInterstitialEvents();
	}


	private void ShowInterstitial()
	{
		if(this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
	}

	private void CreateInterstitial()
	{
		if(this.interstitial != null)
		{
			this.interstitial.Destroy();
		}
		this.interstitial = new InterstitialAd(interstitialAdUnitID);
		AdRequest request = new AdRequest.Builder().AddTestDevice(testDeviceID).Build();
		this.interstitial.LoadAd(request);
	}

	private void RegisterInterstitialEvents()
	{
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

	}
	private void RegisterRewardEvents()
	{
		// Called when an ad request has successfully loaded.
		rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		// Called when an ad request failed to load.
		rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		// Called when an ad is shown.
		rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		// Called when the ad starts to play.
		rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		// Called when the user should be rewarded for watching a video.
		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		// Called when the ad is closed.
		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		// Called when the ad click caused the user to leave the application.
		rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
	}

	private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
	{
	}

	private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
	{
		OpenRewardVideo();
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward e)
	{
		deathController.RevivalApproved();
	}

	private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
	{
	}

	private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
	{
	}

	private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
	{
	}

	private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
	{
	}

	private void HandleOnAdLeavingApplication(object sender, EventArgs e)
	{
	}

	private void HandleOnAdClosed(object sender, EventArgs e)
	{
		InitInterstitial();
	}

	private void HandleOnAdOpened(object sender, EventArgs e)
	{
	}

	private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
	{
		//AddText("Ad Failed To Load:" + e.Message);
	}

	private void HandleOnAdLoaded(object sender, EventArgs e)
	{
	}


}