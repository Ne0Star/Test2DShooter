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


public class Player : Players
{
    public PlayerControllerData Data = new PlayerControllerData();

    [Header(" Центр игрока, для расчётов")]
    public Transform playerCenter;
    [Header(" Родитель точек испускания лучей")]
    public RigParent rigParent;

    private Rigidbody2D body;
    private Collider2D bodyCollider;
    private Vector2 LastPos;

    private void Awake()
    {
        rigParent.startPos = rigParent.parent.localPosition;

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
            Data.debugData.CenterPoint = center.point;
        }
        else
        {
            Data.onGround = false;
        }
        //if (!Data.onGround)
        //{
        //    body.gravityScale = 1;
        //}
        //else
        //{
        //    body.gravityScale = 0;
        //}
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Data.speed = Mathf.Clamp(Data.speed + Time.fixedDeltaTime * Data.accelerationSpeed, 0, Data.maxSpeed);
            if (Input.GetKey(KeyCode.A))
            {
                Data.moveStatus = MoveStatus.moveLeft;
                body.velocity = new Vector2(-Data.speed, body.velocity.y);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Data.moveStatus = MoveStatus.moveRight;
                body.velocity = new Vector2(Data.speed, body.velocity.y);
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            Data.speed = 0f;
            if (Input.GetKeyUp(KeyCode.D))
            {
                Data.moveStatus = MoveStatus.stopRight;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                Data.moveStatus = MoveStatus.stopLeft;
            }
        }

        if (Input.GetKey(KeyCode.Space) && Data.onGround)
        {
            body.AddForce(new Vector2(0, Data.jumpPower));
        }
        if (Data.moveStatus == Player.MoveStatus.moveLeft || Data.moveStatus == Player.MoveStatus.stopLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Data.moveStatus == Player.MoveStatus.moveRight || Data.moveStatus == Player.MoveStatus.stopRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rigParent.parent.localPosition = new Vector3(-((Data.maxSpeed / 25f) * Data.speed), rigParent.parent.localPosition.y, 0);
        body.velocity = Vector2.Lerp(body.velocity, new Vector2(0, body.velocity.y), Data.stopCurve.Evaluate(Data.stopSpeed * Time.fixedDeltaTime));
        LastPos = transform.position;
    }

    [System.Serializable]
    public struct PlayerControllerData
    {
        public AnimationCurve stopCurve;
        public float stopSpeed;
        /// <summary>
        /// Скорость ускорения
        /// </summary>
        public float accelerationSpeed;
        public float maxSpeed;
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
        /// Точка соприкосновения с поверхностью в центре
        /// </summary>
        public Vector2 CenterPoint;
        /// <summary>
        /// Скорость обновления управления
        /// </summary>
        public float ControllerUpdateSpeed;
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Vector3 moveHeight_RayOffset;
        [SerializeField]
        private float jump_RayDistance;
        [SerializeField]
        private float player_YHeight;
        [SerializeField]
        private float player_YHeightOffset;
        public float HeightRayOffset => player_YHeightOffset;
        public float Height => player_YHeight;
        public float JumpRaydistance => jump_RayDistance;
        public Vector3 MoveHeight_RayOffset => moveHeight_RayOffset;
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
        public Vector2 startPos;
        public Vector2 idlePos;
        public Vector2 moveMaxPos;
        public Vector2 runMaxPos;
    }


}
