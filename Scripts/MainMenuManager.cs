using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject baseUI;

    public Animator baseUIanim;
    public Animator settingsUIanim;

    //ADD SETTINGS ANIM TOGGLE

    private void Start()
    {
        baseUI.SetActive(true);
        settingsUI.SetActive(false);
        StartCoroutine(DelayToggle(-1));
    }

    public void Play()
    {
        StartCoroutine(PostFadeAction(0));
    }

    public void ToggleSettings(int id)
    {
        StartCoroutine(DelayToggle(id));
    }

    public void Quit()
    {
        StartCoroutine(PostFadeAction(1));
    }

    IEnumerator PostFadeAction(int id)
    {
        yield return new WaitForSeconds(1.5f);
        if (id == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else if (id == 1)
        {
            Application.Quit();
        }
    }

    IEnumerator DelayToggle(int id)
    {
        yield return new WaitForSeconds(1.5f);
        if (id == 0)
        {
            settingsUI.SetActive(!settingsUI.activeInHierarchy);
            settingsUIanim.SetTrigger("Toggle");
        }
        else if (id == -1)
        {
            baseUIanim.SetTrigger("Toggle");
        }
        else
        {
            settingsUI.SetActive(!settingsUI.activeInHierarchy);
            baseUIanim.SetTrigger("Toggle");
        }
    }
}
