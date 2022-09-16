using UnityEngine;
using GoogleMobileAds.Api;

public class Advertising : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private bool shown;
    private int lose;

    public void Init()
    {
        lose = 0;
        shown = true;
        MobileAds.Initialize(initStatus => { });
    }

    public void AdActivated()
    {
        if (lose < 0 || lose > 2)
        {
            lose = 0;
        }

        if (lose == 0)
        {
            InterstitialAdd();
            shown = false;
        }

        lose++;
    }

    void Update()
    {
        if (shown == false)
        {
            ShowInterstitial();
        }
    }

    private void InterstitialAdd()
    {
        string interstitialID = "code";

        interstitialAd = new InterstitialAd(interstitialID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    private void ShowInterstitial()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
            shown = true;
        }
    }
} 
