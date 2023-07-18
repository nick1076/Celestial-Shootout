using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGore : MonoBehaviour
{
    public float despawnTime = 2.5f;
    public float downScaleFactor = 0.01f;

    float elapsed = 0;

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= despawnTime)
        {
            Destroy(this.gameObject);
        }

        if (this.transform.localScale.x - downScaleFactor < 0)
        {
            this.transform.localScale = new Vector3();
        }
        else
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x - (downScaleFactor * Time.deltaTime), this.transform.localScale.y - (downScaleFactor * Time.deltaTime), this.transform.localScale.z - (downScaleFactor * Time.deltaTime));
        }
    }
}
