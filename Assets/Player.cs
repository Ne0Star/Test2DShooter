using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public abstract class Players : MonoBehaviour
{
    public void SetController(IGroundController c) => controller = c;
    public IGroundController GetController() => controller;
    protected IGroundController controller;
}

/// <summary>
/// Управление стандартного передвижения по земле
/// </summary>
public interface IGroundController : IEnumerator
{
    IEnumerator Update();


}


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Players
{
    public PlayerControllerData Data = new PlayerControllerData();

    [Header(" Центр игрока, для расчётов")]
    public Transform playerCenter;
    [Header(" Родитель точек испускания лучей")]
    public RigParent rigParent;

    private Rigidbody2D body;
    private Collider2D bodyCollider;


    private void Awake()
    {
        

        body = gameObject.GetComponent<Rigidbody2D>();
        bodyCollider = gameObject.GetComponent<Collider2D>();

        foreach (SpriteSkin s in GetComponentsInChildren<SpriteSkin>())
        {
            s.alwaysUpdate = false;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        Debug.DrawRay(playerCenter.position, Vector2.down * Data.debugData.JumpRaydistance, Color.blue);


        RaycastHit2D center = Physics2D.Raycast(playerCenter.position, Vector2.down, Data.debugData.JumpRaydistance, Data.InteractionMask);
        if (center.collider != null)
        {
            Data.onGround = true;
        }
        else
        {
            Data.onGround = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-Data.speed, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(Data.speed, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && Data.onGround)
        {
            body.AddForce(new Vector2(0, Data.jumpPower));
        }


    }

    [System.Serializable]
    public struct PlayerControllerData
    {
        /// <summary>
        /// Слои с которыми взаимодействует игрок
        /// </summary>
        public LayerMask InteractionMask;
        /// <summary>
        /// Скорость передвижения игрока
        /// </summary>
        public float speed;
        /// <summary>
        /// Сила прыжка
        /// </summary>
        public float jumpPower;
        /// <summary>
        /// Находится ли игрок на земле
        /// </summary>
        public bool onGround;
        /// <summary>
        /// Находится ли игрок в прыжке
        /// </summary>
        public bool inJump;
        /// <summary>
        /// Куда двигается игрок или он стоит и в какую сторону смотрит
        /// </summary>
        public MoveStatus moveStatus;

        public ControllerDebugData debugData;
    }


    [System.Serializable]
    public struct ControllerDebugData
    {
        /// <summary>
        /// Скорость обновления управления
        /// </summary>
        public float ControllerUpdateSpeed;

        [SerializeField]
        private float jump_RayDistance;
        public float JumpRaydistance => jump_RayDistance;
    }


    public enum MoveStatus
    {
        stopLeft,
        stopRight,
        moveLeft,
        moveRight
    }

    [System.Serializable]
    public struct RigParent
    {
        public Transform parent;
        public Vector2 idlePos;
        public Vector2 moveMaxPos;
        public Vector2 runMaxPos;
    }


}
