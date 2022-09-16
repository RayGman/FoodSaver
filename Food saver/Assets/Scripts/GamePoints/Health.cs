using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public delegate void HealthPointInfo(int health);
    public event HealthPointInfo healthPointInfo; // подписка для чека хп в LvlManager

    private int healthPoint;
    public int HealthPoint
    {
        get { return healthPoint; }
        private set { }
    }

    [SerializeField] private Text textHP;
    private Slider sliderHPBar;   

    [SerializeField] private FoodChallenge challenge;
    [SerializeField] private Bonus bonus;

    private void Start()
    {
        healthPoint = 100;
        sliderHPBar = gameObject.GetComponent<Slider>();
        sliderHPBar.value = healthPoint;
      
        SetHealthPoint(healthPoint);

        challenge.GetComponent<FoodChallenge>().changeHealth += ChangeHealthPoint;
        bonus.GetComponent<Bonus>().bonusHealth += ChangeHealthPoint;
    }

    private void ChangeHealthPoint(int setHealth)
    {
        healthPoint += setHealth;

        if (healthPoint > 100)
        { healthPoint = 100; }

        if (healthPoint <= 0)
        {
            healthPoint = 0;
            healthPointInfo?.Invoke(healthPoint);
        }

        SetHealthPoint(healthPoint);       
    }

    private void SetHealthPoint(int setHealth)
    {
        sliderHPBar.value = setHealth;
        textHP.text = sliderHPBar.value.ToString();
    }
}
