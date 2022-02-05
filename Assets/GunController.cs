﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform header;
    private Vector3 mousePos, startPos;
    private Player player;
    public float angle, minAngle, maxAngle, current, radius, headerOffset;//, weight, l;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        if (!player)
            return;
        startPos = transform.localPosition;

    }

    void FixedUpdate()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = mousePos;
        current = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg - angle;

        if (player.currentControllerData.moveStatus == Player.MoveStatus.moveLeft)
        {
            angle = 180f;
            header.localScale = new Vector3(1, 1, 1);
        }
        else if (player.currentControllerData.moveStatus == Player.MoveStatus.moveRight)
        {
            header.localScale = new Vector3(-1, -1, 1);
            angle = 0f;
        }

        Vector3 pos = mousePos - transform.position;
        transform.localPosition = startPos + (LevelManager.Instance.Player.currentControllerData.directionLeft ? (pos.normalized * radius) : (pos.normalized * -radius)); //Vector3.ClampMagnitude(startPos + mousePos.normalized, l);

        transform.rotation = Quaternion.AngleAxis(current, Vector3.forward);

        //transform.rotation =
        //    Quaternion.Euler(
        //        transform.rotation.eulerAngles.x,
        //        transform.rotation.eulerAngles.y,
        //        current);
        header.transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                current + headerOffset);

    }
}