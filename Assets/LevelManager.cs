using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    #region Events
    private GameEvents events;
    public bool useEvents;
    public GameEvents Events { get => events; }

    #endregion

    private Player player;
    public Player Player { get => player; }

    private void Awake()
    {
        LevelManager.Instance = this;
        events = new GameEvents(useEvents);
        StartCoroutine(LevelLoad());
        // Проверить точки спавна, и полную генерацию уровня.

        Application.targetFrameRate = 60;

    }

    private IEnumerator LevelLoad()
    {
        yield return StartCoroutine(GetPlayer());
        Debug.Log("Игрок найден");


    }

    private IEnumerator GetPlayer()
    {
        int Attempts = 0;
        while (player == null || Attempts < 10)
        {
            player = GameObject.FindObjectOfType<Player>();
            Attempts++;
            yield return new WaitForSeconds(0.02f);
        }
        yield break;
    }


    private void Start()
    {
    }

    private void Update()
    {

    }

    #region Евенты


    public class GameEvents
    {
        /// <summary>
        /// Одна из управляющих кнопок была отпущена
        /// </summary>
        public OnKeysUp onKeysUp;
        public GameEvents(bool use)
        {
            if (!use)
                return;
            Debug.Log("Евенты активированы");
            onKeysUp = new OnKeysUp();
        }
        [System.Serializable]
        public class OnKeysUp : UnityEvent { }
    }
    #endregion
}
