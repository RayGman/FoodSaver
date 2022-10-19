using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class FoodShop : MonoBehaviour
{
    [SerializeField] private GameObject sellFoodPrefab;
    [SerializeField] private Transform parentPrefabs;
    [SerializeField] private List<FoodData> sellFoodDataList;
    [SerializeField] private List<FoodData> buyFoodDataList;

    [SerializeField] private Coin coinScript;

    private SaveFoodShop saveFoodShop = new SaveFoodShop();

    [SerializeField] private GameObject Shop;

    private void Start()
    {
        CheckSaveShopFile();

        Shop.SetActive(false);
    }

    public void BuyFood(int price, FoodData _data)
    {
        coinScript.ChangeCoin(-price);
        for (int i = 0; i < sellFoodDataList.Count; i++)
        {
            if (_data.name == sellFoodDataList[i].name)
            {
                saveFoodShop.buyFoods.Add(sellFoodDataList[i].name);
                if (saveFoodShop.sellFoods.Count > 1)
                {
                    saveFoodShop.sellFoods.RemoveAt(i);
                }
                else
                {
                    saveFoodShop.sellFoods.Clear();
                }               
            }
        }
        
        DataSaver.saveData(saveFoodShop, "FoodSaver_Data");
    }

    private void CheckSaveShopFile()
    {
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, "FoodSaver_Data.txt");
        bool reloadSave = false;
        if (File.Exists(tempPath))
        {
            saveFoodShop = DataSaver.loadData<SaveFoodShop>("FoodSaver_Data");

            sellFoodDataList.Clear();
            foreach (string item in saveFoodShop.sellFoods)
            {
                sellFoodDataList.Add(Resources.Load<FoodData>("Food/AddFood/" + item));
            }

            if (saveFoodShop.sellFoods.Count < 1 && saveFoodShop.buyFoods.Count < 4)
            {
                reloadSave = true;
            }         
        }
        else
        {
            reloadSave = false;

            sellFoodDataList.Clear();

            saveFoodShop.sellFoods = new List<string>();
            saveFoodShop.sellFoods.Clear();

            buyFoodDataList.Clear();

            saveFoodShop.buyFoods = new List<string>();
            saveFoodShop.buyFoods.Clear();

            var foodData = Resources.LoadAll("Food/AddFood/");
            foreach (FoodData data in foodData)
            {
                sellFoodDataList.Add(data);                            
                saveFoodShop.sellFoods.Add(data.name);
            }
            DataSaver.saveData(saveFoodShop, "FoodSaver_Data");
        }

        if (reloadSave == true)
        {
            reloadSave = false;
            DataSaver.deleteData("FoodSaver_Data");
            CheckSaveShopFile();
        }
        else
        {
            SetFoodShop();
        }
    }

    private void SetFoodShop()
    {
        for (int i = 0; i < sellFoodDataList.Count; i++)
        {
            var prefab = Instantiate(sellFoodPrefab, parentPrefabs);
            var script = prefab.GetComponent<SellFood>();
            if (sellFoodDataList[i] != null)
            {
                script.Init(sellFoodDataList[i]);
            }
            else
            {
                foreach (Transform childPrefab in gameObject.transform)
                {
                    Destroy(childPrefab.gameObject);
                }

                DataSaver.deleteData("FoodSaver_Data");
                CheckSaveShopFile();
                break;
            }
        }
        DataSaver.saveData(saveFoodShop, "FoodSaver_Data");
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause) DataSaver.saveData(saveFoodShop, "FoodSaver_Data"); 
    }
#endif
    private void OnApplicationQuit()
    {
        DataSaver.saveData(saveFoodShop, "FoodSaver_Data");
    }
}

[Serializable]
public class SaveFoodShop
{
    public List<string> sellFoods;
    public List<string> buyFoods;
}