using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodChallenge : MonoBehaviour
{
    public static FoodChallenge foodChallenge { get; private set; }
    public delegate void ChangeGame(int value);
    public event ChangeGame changeHealth;
    public event ChangeGame changeChallenge;

    [SerializeField] private FoodSpawner foodSpawner;

    private List<FoodData> foodList;
    private Image spriteChallenge;
    private Slider progresBar;

    private int lvlGame, countProgress, randFoodChallenge, randCountProgress;
    private float progres;
    private System.Random rnd;

    private void Awake()
    {
        foodChallenge = this;
    }

    private void Start()
    {
        lvlGame = 0;
        rnd = new System.Random();

        foodList = foodSpawner.FoodSettings;
        spriteChallenge = gameObject.GetComponent<Image>();
        progresBar = gameObject.transform.GetComponentInChildren<Slider>();

        NewChallenge();
    }

    public void DestroyedFood(string name, int _damage, int typeDestroyed)
    {
        int damage = -_damage;
        if (typeDestroyed == 1)
        {
            if (foodList[randFoodChallenge].name == name)
            {
                changeHealth?.Invoke(damage);
            }
        }

        if (typeDestroyed == 2)
        {
            if (foodList[randFoodChallenge].name != name)
            {
                changeHealth?.Invoke(damage);
            }
            if (foodList[randFoodChallenge].name == name)
            {
                countProgress++;
                PerfomChallenge(countProgress);
            }
        }
    }

    private void NewChallenge()
    {
        lvlGame++;
        progres = 0f;
        countProgress = 0;
        randFoodChallenge = rnd.Next(2, foodList.Count);
        randCountProgress = rnd.Next(2, 10);

        changeChallenge?.Invoke(lvlGame);
        foodSpawner.SetFoodChallenge(randFoodChallenge);

        DestroyedFood("tester", 0, 0);
        ChangeImage();
    }

    private void PerfomChallenge(int count)
    {
        if (count >= randCountProgress || count < 0)
        {
            NewChallenge();
        }
        else 
        {          
            progres = (((float)count / (float)randCountProgress) * 100);           
        }

        progresBar.value = progres;
    }

    private void ChangeImage()
    {       
        spriteChallenge.sprite = foodList[randFoodChallenge].MainSprite;
    }
}
