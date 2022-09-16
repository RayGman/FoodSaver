using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void ToScene(int scene)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scene, LoadSceneMode.Single) ;
    }
}
