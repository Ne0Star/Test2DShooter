using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public PlayerControllerData currentControllerData = new PlayerControllerData();

    private void Start()
    {

    }

    [System.Serializable]
    public class PlayerControllerData
    {
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
        /// Куда двигается игрок или он стоит
        /// </summary>
        public MoveStatus moveStatus;
        /// <summary>
        /// Скорость обновления положения ног
        /// </summary>
        public float RigUpdateSpeed;
        /// <summary>
        /// Куда смотри игрок
        /// </summary>
        public bool directionLeft;
    }

    public enum MoveStatus
    {
        moveLeft,
        moveRight,
        moveStop
    }

}
//public class LevelInitialization
//{
//    private string playerPrefabPath = "Prefab/Player";
//    public LevelInitialization(bool use)
//    {
//        if (!use) return;
//        GameObject player = MonoBehaviour.Instantiate("", );


//        return;
//    }
//}
