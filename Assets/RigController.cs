using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigController : MonoBehaviour
{
    public Transform weapon;
    void Start()
    {

    }
    private void Update()
    {
        transform.position = weapon.position;
    }
}
