using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private int coinPoint;

    [SerializeField] private Text coinText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("coinPP"))
        {
            PlayerPrefs.SetInt("coinPP", 0);
            coinPoint = 0;
        }
        else { coinPoint = PlayerPrefs.GetInt("coinPP"); }   

        coinText.text = coinPoint.ToString();
    }

    public void ChangeCoin(int setCoin)
    {
        ChangeCoinPoint(setCoin);
    }

    private void ChangeCoinPoint(int setCoin)
    {
        coinPoint = PlayerPrefs.GetInt("coinPP");

        coinPoint += setCoin;

        coinText.text = coinPoint.ToString();

        PlayerPrefs.SetInt("coinPP", coinPoint);
    }
}
