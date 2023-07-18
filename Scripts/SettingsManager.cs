using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider sensSlider;
    public TextMeshProUGUI sensText;
    public bool overrideOnStart = true;

    private void Start()
    {
        float sens = PlayerPrefs.GetFloat("Sensitivity");

        if (sens == 0)
        {
            sens = 1;
        }

        PlayerPrefs.SetFloat("Sensitivity", sens);

        sensSlider.value = sens;
        sensText.text = (Mathf.Round(sensSlider.value * 100) / 100).ToString();
    }

    public void OnSensitivityUpdated()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensSlider.value);
        sensText.text = (Mathf.Round(sensSlider.value * 100) / 100).ToString();
    }

    public void LoadMainMenu()
    {
        StartCoroutine(WaitLoad());
    }

    public void ReloadLevel()
    {
        StartCoroutine(WaitLoad2());
    }

    IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator WaitLoad2()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
