using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

    [SerializeField] private float fade;

    public void LoadGameScene()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        ScreenFade.instance.FadeToBlack();
        yield return new WaitForSeconds(fade);
        ScreenFade.instance.FadeFromBlack();
        SceneManager.LoadScene("SampleScene");
    }
    
    public void QuitApp()
    {
        Application.Quit();
    }
}
