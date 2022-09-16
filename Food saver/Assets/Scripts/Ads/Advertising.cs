using UnityEngine;
using GoogleMobileAds.Api;

public class Advertising : MonoBehaviour
{
    //private const string appID = "ca-app-pub-3094727033726561~6503994050";
    //private const string bannerID = "ca-app-pub-3094727033726561/6224792457";
    //private const string interstitialID = "ca-app-pub-3094727033726561/5813728918";

    //private BannerView banner;
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
        /*float randAd = Random.Range(0, 1);
        if (randAd <= 0.3f)
        {
            BannerAdd();
        }
        else //if (randAd > 0.4f && randAd <= 1f)
        {
            InterstitialAdd();
            shown = false;
        } */
    }

    void Update()
    {
        if (shown == false)
        {
            ShowInterstitial();
        }
    }

    /*private void BannerAdd()
    {
        string bannerID = "ca-app-pub-3094727033726561/6224792457";

        banner = new BannerView(bannerID, AdSize.MediumRectangle, AdPosition.Center);
        AdRequest adRequest = new AdRequest.Builder().Build();
        banner.LoadAd(adRequest);
    } */

    private void InterstitialAdd()
    {
        string interstitialID = "ca-app-pub-3094727033726561/5813728918";

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
