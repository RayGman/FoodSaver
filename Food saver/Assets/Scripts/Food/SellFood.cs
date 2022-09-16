using UnityEngine;
using UnityEngine.UI;

public class SellFood : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyButton;

    private FoodData data;
    private FoodShop foodShop;

    private int price;
    [SerializeField]private int coefficientfPrice;

    public void Init(FoodData _data)
    {
        foodShop = GetComponentInParent<FoodShop>();

        data = _data;

        gameObject.name = data.name;

        price = data.Damage * coefficientfPrice;
        priceText.text = price.ToString();

        GetComponent<Image>().sprite = data.MainSprite;
    }

    public void Buy()
    {
        int coins = PlayerPrefs.GetInt("coinPP"); ;

        if (price <= coins)
        {
            foodShop.BuyFood(price, data);
            buyButton.interactable = false;
        }      
    }
}
