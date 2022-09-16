using UnityEngine;
using UnityEngine.UI;

public class LvlRecord : MonoBehaviour
{
    private Text lvlrecordText;
    private int lvlrecord;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("lvlrecordPP"))
        {
            PlayerPrefs.SetInt("lvlrecordPP", 0);
            lvlrecord = 0;
        }
        else { lvlrecord = PlayerPrefs.GetInt("lvlrecordPP"); }

        lvlrecordText = GetComponent<Text>();
        lvlrecordText.text = "MAX LVL " + lvlrecord.ToString();
    }
}
