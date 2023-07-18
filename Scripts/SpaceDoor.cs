using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDoor : MonoBehaviour
{
    public Animator anim;
    public bool active = true;
    public GameObject collision;
    public bool generationDoor;

    bool closed = false;
    bool open = false;

    public void Open()
    {
        if (!active || closed)
        {
            return;
        }

        if (generationDoor && !open)
        {
            open = true;
            Vector3 euler = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y - 180, this.transform.eulerAngles.z);
            GameObject.FindWithTag("GenerationManager").GetComponent<GenerationManager>().GenerateRoom(this.transform.position, euler);
        }

        collision.SetActive(false);
        anim.SetTrigger("Open");
    }

    public void Close()
    {
        if (!active)
        {
            return;
        }

        closed = true;
        collision.SetActive(true);
        anim.SetTrigger("Close");
    }
}
