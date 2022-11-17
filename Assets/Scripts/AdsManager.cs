using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsListener
{

#if UNITY_ANDROID
    string gameId = "5017165";
    string Interstitial = "Interstitial_Android";
    string Rewarded = "Rewarded_Android";
    string Banner = "Banner_Android";
#else
    string gameId = "5017164";
    string Interstitial = "Interstitial_iOS";
    string Rewarded = "Rewarded_iOS";
    string Banner = "Banner_iOS";
#endif
    BannerOptions bannerOptions = new BannerOptions();
    ShowOptions showOptions = new ShowOptions();
    private void Start()
    {
        Advertisement.Initialize(
            gameId: gameId,
            testMode: true,
            enablePerPlacementLoad: true,
            initializationListener: this);   
        Advertisement.AddListener(listener: this);
        bannerOptions.showCallback += OnShowBanner;
        bannerOptions.hideCallback += OnHideBanner;
        bannerOptions.clickCallback += OnClickBanner;
    }
    private void OnDestroy()
    {
        bannerOptions.showCallback -= OnShowBanner;
        bannerOptions.hideCallback -= OnHideBanner;
        bannerOptions.clickCallback -= OnClickBanner;
    }
    public void ShowInterstitial()
    {
        Advertisement.Load(
            placementId: Interstitial,
            loadListener: this);
        Advertisement.Show(
            placementId: Interstitial,
            showOptions: showOptions,
            showListener: this);
    }
    public void ShowRewarded()
    {
        Advertisement.Load(
            placementId: Rewarded,
            loadListener: this);
        Advertisement.Show(
            placementId: Rewarded,
            showOptions: showOptions,
            showListener: this);
    }
    public void ShowBanner()
    {
        Advertisement.Banner.Load(Banner);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(
            placementId: Banner,
            options: bannerOptions);
    }
    public void HideBanner()
    {
        Advertisement.Banner.Hide(false);
    }
    // Banner Callbacks
    private void OnShowBanner()
    {
        Debug.Log("OnShowBanner");
    }
    private void OnHideBanner()
    {
        Debug.Log("OnHideBanner");
    }
    private void OnClickBanner()
    {
        Debug.Log("OnClickBanner");
    }
    // Initialization Callbacks
    public void OnInitializationComplete()
    {
        Debug.Log("OnInitializationComplete");
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("OnInitializationFailed: [" + error + "]" + message);
    }
    // Load Callbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded: " + placementId);
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("OnUnityAdsFailedToLoad: [" + placementId + "][" + error + "]" + message);
    }
    // Show Callbacks 
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure: [" + placementId + "][" + error + "]" + message);
    }
    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart: [" + placementId + "]");
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick: [" + placementId + "]");
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete: [" + placementId + "][" + showCompletionState + "]");
    }
    // Ads Callbacks
    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("OnUnityAdsReady");
    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("OnUnityAdsDidError");
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("OnUnityAdsDidStart");
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("OnUnityAdsDidFinish: [" + placementId + "]" + showResult);
    }
}
