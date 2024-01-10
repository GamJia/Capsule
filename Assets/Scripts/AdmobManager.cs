using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread=true;
        MobileAds.Initialize(initStatus=>{
            print("Ads Initialised!");
        });

        LoadBannerAd();
    }

    
    #region Banner
    const string testBannerID="ca-app-pub-3940256099942544/6300978111";
    const string capsuleBannerID="ca-app-pub-9726670934205610/3558755417";
    BannerView _bannerView;

    void LoadBannerAd()
    {
        CreateBanerView();
        ListenToAdEvents();

        if(_bannerView==null)
        {
            CreateBanerView();
        }

        var adRequest=new AdRequest();
        adRequest.Keywords.Add("Capsule Banner");

        _bannerView.LoadAd(adRequest);
    }

    void CreateBanerView()
    {
        if(_bannerView!=null)
        {
            DestroyBannerView();
        }

        _bannerView=new BannerView(testBannerID,AdSize.Banner,AdPosition.Bottom);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    
    #endregion
}
