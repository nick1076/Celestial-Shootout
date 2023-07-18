using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ComboManager : MonoBehaviour
{
    [Header("Combo GUI Elements")]

    //Image of combo shape (circle, semi, triangle, etc., but the actual image)
    public Image comboShapeBackground;

    //Image of combo shape (circle, semi, triangle, etc., but the actual image) with the fill
    public Image comboShapeFill;

    //Combo number text
    public TextMeshProUGUI comboText;

    [Header("Combo Icons & Colors")]
    
    //List of combo shape sprites
    public List<Sprite> comboSprites = new List<Sprite>();

    //List of combo colors
    public List<Color> comboColors = new List<Color>();

    //List of combo colors background
    public List<Color> comboColorsBackground = new List<Color>();

    [Header("Combo GameObjects")]

    //Overall Combo UI
    public GameObject comboUIParent;

    //Animator for combo GUI
    public Animator comboAnim;

    //Player manager object
    public PlayerManager playMan;

    //Hidden

    //Int to track current combo
    private int currentCombo = 0;

    //Tells if combo can be reset, set to false when a room is cleared and player hasn't entered the next room
    private bool ableToReset = true;

    //SpaceRoom for the current room that the player is in
    SpaceRoom currentRoom;

    private void Update()
    {
        if (currentRoom != null)
        {
            ableToReset = !currentRoom.cleared;
        }
    }


    //Called when a room is entered
    public void OnRoomEntered(SpaceRoom room)
    {
        currentRoom = room;
    }


    //Increments combo count
    public void IncrimentCombo()
    {
        currentCombo++;

        if (currentCombo - 1 == 0)
        {
            comboAnim.SetTrigger("Show");
        }

        if (currentCombo == 4 || currentCombo == 7)
        {
            comboAnim.SetTrigger("Progress");
        }

        UpdateComboGUI();
    }

    //Resets combo count if able
    public void ResetCombo()
    {
        if (!ableToReset)
        {
            return;
        }
        if (currentCombo > 0)
        {
            comboAnim.SetTrigger("Reset");
        }
        currentCombo = 0;
        UpdateComboGUI();
    }

    //Updates combo GUI
    private void UpdateComboGUI()
    {
        if (currentCombo == 0)
        {
            return;
        }

        if (currentCombo - 1 >= comboSprites.Count)
        {
            comboShapeFill.sprite = comboSprites[comboSprites.Count - 1];
            comboShapeFill.color = comboColors[comboSprites.Count - 1];
            comboShapeBackground.sprite = comboSprites[comboSprites.Count - 1];
            comboShapeBackground.color = comboColorsBackground[comboSprites.Count - 1];
            comboText.text = currentCombo.ToString();
        }
        else
        {
            comboShapeFill.sprite = comboSprites[currentCombo - 1];
            comboShapeFill.color = comboColors[currentCombo - 1];
            comboShapeBackground.sprite = comboSprites[currentCombo - 1];
            comboShapeBackground.color = comboColorsBackground[currentCombo - 1];
            comboText.text = currentCombo.ToString();
        }

        if (currentCombo < 5)
        {
            comboShapeFill.fillAmount = (float)currentCombo / 5.0f;
        }
        else if (currentCombo >= 5)
        {
            HealPlayer();
            comboShapeFill.fillAmount = 1;
        }
    }

    public void HealPlayer()
    {
        playMan.Heal();
    }
}
