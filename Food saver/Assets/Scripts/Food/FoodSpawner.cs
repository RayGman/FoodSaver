using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Transform parentPrefabs;
    [SerializeField] private GameObject foodPrefab;
    public static Dictionary<GameObject, Food> Foods;
    private Queue<GameObject> currentFoods;

    [SerializeField] private List<FoodData> foodSettings;
    [HideInInspector] public List<FoodData> FoodSettings
    {
        get { return foodSettings; }
        protected set { }
    }

    [SerializeField] private List<BonusData> bonusSettings;
    [SerializeField] private GameObject bonusPrefab;
    private Bonus bonusScript;

    [SerializeField] private int poolCount;
    [SerializeField] private float spawnTime;

    private SaveFoodShop saveFoodShop = new SaveFoodShop();
    [SerializeField] private List<FoodData> addFoodSettings;

    private Vector2 spawnPoint;

    int countArraySpawnTime, countCorrecting;
    float[] arrayMinusSpawnTime = new float[] { 0.025f, 0.01f, 0.005f, 0.06f };
    private System.Random rnd;
    int countBonus = 0;

    private void Start()
    {
        rnd = new System.Random();
        countArraySpawnTime = 0;
        countCorrecting = 0;

        bonusScript = bonusPrefab.GetComponent<Bonus>();

        spawnPoint = gameObject.GetComponent<Transform>().position;
        Foods = new Dictionary<GameObject, Food>();
        currentFoods = new Queue<GameObject>();

        for (int i = 0; i < poolCount; i++)
        {
            var prefab = Instantiate(foodPrefab);
            var script = prefab.GetComponent<Food>();
            prefab.transform.parent = parentPrefabs;
            prefab.SetActive(false);
            Foods.Add(prefab, script);
            currentFoods.Enqueue(prefab);
        }

        saveFoodShop = DataSaver.loadData<SaveFoodShop>("FoodSaver_Data");

        if (saveFoodShop.buyFoods.Count >= 1)
        {
            foreach (string addFood in saveFoodShop.buyFoods)
            {
                addFoodSettings.Add(Resources.Load<FoodData>("Food/AddFood/" + addFood));
            }
        }
        
        Food.OnFoodOverFly += ReturnFood;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        float indent = 0.5f;
        float rangeBorder = GameCamera.Border - indent;
        int randNumberBonus = Random.Range(13, 15);
        
        if (spawnTime == 0)
        { spawnTime = 1; }

        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            if (countBonus < randNumberBonus)
            {
                SpawnFood(rangeBorder);
                countBonus++;
            }
            else 
            {
                SpawnBonus(rangeBorder);
                countBonus = 0;
            }            
        }
    }

    public void SetFoodChallenge(int _food)
    {
        foodSettings[0] = foodSettings[_food];
    }

    public void SetNewLvlSettings(int typeLvl)
    {
        if (typeLvl == 1)
        {
            SetPoolCount();
        }
        if (typeLvl == 2)
        {
            SetSpawnTime();
        }
        if (typeLvl == 3)
        {
            SetSpawnTime();
            SetfoodSettings();
        }
    }

    private void SetPoolCount()
    {
        if (poolCount < 13)
        {
            int last_poolCount = poolCount;
            poolCount += rnd.Next(1, 3);
            for (int i = last_poolCount; i < poolCount; i++)
            {
                var prefab = Instantiate(foodPrefab);
                var script = prefab.GetComponent<Food>();
                prefab.transform.parent = parentPrefabs;
                prefab.SetActive(false);
                Foods.Add(prefab, script);
                currentFoods.Enqueue(prefab);
            }
        }
    }

    private void SetSpawnTime()
    {
        if (countArraySpawnTime >= arrayMinusSpawnTime.Length)
        { countArraySpawnTime = 0; }

        spawnTime -= arrayMinusSpawnTime[countArraySpawnTime];
        countArraySpawnTime++;
    }

    private void SetfoodSettings()
    {
        if (addFoodSettings.Count > 0)
        {
            int randfood = rnd.Next(0, addFoodSettings.Count);
            foodSettings.Add(addFoodSettings[randfood]);
            addFoodSettings.RemoveAt(randfood);
        }
    }

    private void SpawnFood(float rangeBorder)
    {
        if (currentFoods.Count > 0)
        {
            var food = currentFoods.Dequeue();
            var script = Foods[food];
            food.SetActive(true);

            int randFood = rnd.Next(0, foodSettings.Count);

            if (countCorrecting > 3)
            {
                randFood = 0;
                countCorrecting = 0;
            }
            countCorrecting++;

            script.Init(foodSettings[randFood]);

            float xPos = Random.Range(-rangeBorder, rangeBorder);
            food.transform.position = new Vector2(xPos, transform.position.y);
        }
    }

    private void SpawnBonus(float rangeBorder)
    {
        int randBonus = rnd.Next(0, bonusSettings.Count);

        bonusPrefab.SetActive(true);        
        bonusScript.Init(bonusSettings[randBonus]);

        float xPos = Random.Range(-rangeBorder, rangeBorder);
        bonusPrefab.transform.position = new Vector2(xPos, transform.position.y);
    }

    private void ReturnFood(GameObject _object)
    {
        _object.SetActive(false);
        _object.transform.position = spawnPoint;
        currentFoods.Enqueue(_object);
    }
}
