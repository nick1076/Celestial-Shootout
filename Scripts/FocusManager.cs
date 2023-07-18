using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages focus system with all focus objects
public class FocusManager : MonoBehaviour
{
    //Overarching focus variable - controls focus objects
    [HideInInspector] public bool focused;
    [HideInInspector] public bool lockFocus;

    [Header("Focus Components")]
    //Player look camera script
    public CameraController cameraController;
    public Animator pauseMenu;
    public GameObject settingsMenu;

    //Gun control script
    public GunController gunController;

    bool clickedIn = false;

    private void Start()
    {
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (!lockFocus)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (focused)
                {
                    pauseMenu.SetTrigger("Toggle");
                }
                focused = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && !clickedIn)
            {
                clickedIn = true;
                focused = true;
            }
        }

        cameraController.focused = focused;
        gunController.focused = focused;
    }

    public void ReFocus()
    {
        pauseMenu.SetTrigger("Toggle");
        focused = true;
    }

    public void UnFocus()
    {
        focused = false;
    }
}
