using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner_0 : MonoBehaviour
{
    public bool KillParent;
    public void OnBecameInvisible()
    {
        if (KillParent)
        {
            Destroy(transform.parent.gameObject);
        }
        else
            Destroy(gameObject);
    }
}
