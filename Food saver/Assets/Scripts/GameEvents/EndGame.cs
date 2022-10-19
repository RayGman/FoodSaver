using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject audioObject;
    [SerializeField] private Transform foodPrefabs;   
    [SerializeField] private Transform panelMenu;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Advertising advertising;

    private void Start()
    {
        buttonPlay.interactable = true;
        panelMenu.gameObject.SetActive(false);

        if (GameObject.FindGameObjectWithTag("AdUnit") == null)
        {
            DontDestroyOnLoad(advertising.gameObject);
            advertising.gameObject.tag = "AdUnit";
            advertising.Init();
        }
        else
        {
            advertising.enabled = false;
            advertising = GameObject.FindGameObjectWithTag("AdUnit").GetComponent<Advertising>();
        }
    }

    public void TheEnd()
    {
        panelMenu.gameObject.SetActive(true);
        foodPrefabs.gameObject.SetActive(false);
        audioObject.SetActive(false);
        buttonPlay.interactable = false;
        Time.timeScale = 0f;
        advertising.gameObject.SetActive(true);
        advertising.GetComponent<Advertising>().AdActivated();
    }
}
