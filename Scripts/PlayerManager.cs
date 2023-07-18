using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]

    //Max health of player
    public int playerTotalHealthPoints = 10;

    [Header("Player Objects")]

    //Individual health indicator objects
    public List<GameObject> playerHealthIndicators = new List<GameObject>();
    public List<PostProcessProfile> playerDamageProfiles = new List<PostProcessProfile>();
    public PostProcessProfile playerHealProfile;
    public PostProcessVolume globalVolume;
    public FocusManager focusMan;
    public Animator fadeCanvasAnim;
    float damageTimer;
    float healTimer;

    //Hidden

    //Current health of player
    private int playerCurrentHealthPoints = 10;

    private void Start()
    {
        playerCurrentHealthPoints = playerTotalHealthPoints;
        globalVolume.profile = playerDamageProfiles[0];

        foreach (GameObject obj in playerHealthIndicators)
        {
            obj.SetActive(true);
        }
    }

    public void Damage(int damageAmount = 1)
    {
        damageTimer = 3;

        playerCurrentHealthPoints -= damageAmount;

        if (playerCurrentHealthPoints > 2)
        {
            globalVolume.profile = playerDamageProfiles[1];
        }
        else
        {
            damageTimer = 0;
        }

        foreach (GameObject obj in playerHealthIndicators)
        {
            obj.SetActive(true);
        }

        for (int i = 0; i < playerHealthIndicators.Count - playerCurrentHealthPoints; i++)
        {
            playerHealthIndicators[i].SetActive(false);
        }

        if (playerCurrentHealthPoints == 2)
        {
            globalVolume.profile = playerDamageProfiles[1];
        }
        else if (playerCurrentHealthPoints == 1)
        {
            globalVolume.profile = playerDamageProfiles[2];
        }
        else if (playerCurrentHealthPoints <= 0)
        {
            globalVolume.profile = playerDamageProfiles[3];

            OnDeath();
        }
    }

    public void Heal()
    {
        healTimer = 3;

        playerCurrentHealthPoints += 1;

        if (playerCurrentHealthPoints > playerTotalHealthPoints)
        {
            playerCurrentHealthPoints = playerTotalHealthPoints;
        }

        foreach (GameObject obj in playerHealthIndicators)
        {
            obj.SetActive(true);
        }

        for (int i = 0; i < playerHealthIndicators.Count - playerCurrentHealthPoints; i++)
        {
            playerHealthIndicators[i].SetActive(false);
        }
    }

    private void OnDeath()
    {
        focusMan.lockFocus = true;
        focusMan.focused = false;
        StartCoroutine(WaitFade());
    }

    IEnumerator WaitFade()
    {
        yield return new WaitForSeconds(1.5f);
        fadeCanvasAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (damageTimer > 0 && playerCurrentHealthPoints > 2)
        {
            globalVolume.profile = playerDamageProfiles[1];
        }
        else if (healTimer > 0)
        {
            globalVolume.profile = playerHealProfile;
        }

        if (healTimer <= 0 && damageTimer <= 0)
        {
            if (playerCurrentHealthPoints > 2)
            {
                healTimer = 0;
                damageTimer = 0;
                globalVolume.profile = playerDamageProfiles[0];
            }
            else if (playerCurrentHealthPoints == 2)
            {
                globalVolume.profile = playerDamageProfiles[1];
            }
            else if (playerCurrentHealthPoints == 1)
            {
                globalVolume.profile = playerDamageProfiles[2];
            }
            else if (playerCurrentHealthPoints <= 0)
            {
                globalVolume.profile = playerDamageProfiles[3];
            }
        }

        damageTimer -= Time.deltaTime;
        healTimer -= Time.deltaTime;
    }
}
