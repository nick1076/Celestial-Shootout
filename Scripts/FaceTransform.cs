using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTransform : MonoBehaviour
{
    public Transform lookAt;

    private void Update()
    {
        transform.LookAt(lookAt);
    }
}
