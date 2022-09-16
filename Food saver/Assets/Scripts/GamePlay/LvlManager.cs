using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private Text lvlText;

    [SerializeField] private Health healthPoint;
    [SerializeField] private FoodChallenge foodChallenge;  
    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private EndGame endGameScript;

    private System.Random rnd;

    int typeLvl;
    private int maxLvl;
    List<int> defender = new List<int>() { 0, 0, 0, 0 };

    private void Start()
    {       
        lvlText.text = "1";
        rnd = new System.Random();

        healthPoint.GetComponent<Health>().healthPointInfo += CheckEndGame;
        foodChallenge.GetComponent<FoodChallenge>().changeChallenge += LvLUp;
    }

    private void LvLUp(int Lvl)
    {
        if (Lvl > 0)
        {
            typeLvl = 0;
            lvlText.text = Lvl.ToString();

            // генерация уровня сложности = 1-легкий, 2-средний, 3-сложный
            FixRandom();
            RepeatFixer();

            if (Lvl > 1)
            {
                foodSpawner.SetNewLvlSettings(typeLvl);
            }

            maxLvl = Lvl;
        }
    }

    private void FixRandom()
    {
        int randType = rnd.Next(100, 500);
        if (randType < 250)
        { typeLvl = 1; }
        if (randType >= 250 && randType < 400)
        { typeLvl = 2; }
        if (randType >= 400)
        { typeLvl = 3; }
    }

    private void RepeatFixer()
    {
        if (defender[defender.Count - 1] == typeLvl && defender[defender.Count - 2] == typeLvl)
        {
            if (typeLvl == 1)
            {
                int randType = rnd.Next(50, 100);
                if (randType < 70)
                    typeLvl = 2;
                if (randType >= 70)
                    typeLvl = 3;
            }
            if (typeLvl == 2)
            {
                int randType = rnd.Next(50, 100);
                if (randType < 75)
                    typeLvl = 1;
                if (randType >= 75)
                    typeLvl = 3;
            }
            if (typeLvl == 3)
            {
                int randType = rnd.Next(1, 2);
                typeLvl = randType;
            }
        }

        if (defender.Count <= 10)
        { defender.Add(typeLvl); }
        else
        {
            defender.Clear();
            defender = new List<int>() { 0, 0, 0, 0 };
        }
    }

    private void CheckEndGame(int health)
    {
        if (health <= 0)
        {
            if (maxLvl > PlayerPrefs.GetInt("lvlrecordPP"))
            {
                PlayerPrefs.SetInt("lvlrecordPP", maxLvl);
            }         
            endGameScript.TheEnd();
        }       
    }
}
