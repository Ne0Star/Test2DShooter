using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public Vector3 localIdle, localMove, localRun;
    public float UpdateTime;


    private void Start()
    {
        StartCoroutine(UpdateTransform());
    }
    private IEnumerator UpdateTransform()
    {
        Player player = LevelManager.Instance?.Player;
        yield return new WaitUntil(() => player != null);
        yield return new WaitForSeconds(UpdateTime);
        switch (player.currentControllerData.moveStatus)
        {
            case Player.MoveStatus.moveLeft:
                transform.localPosition = localMove;
                break;
            case Player.MoveStatus.moveRight:
                transform.localPosition = localMove;
                break;
            case Player.MoveStatus.moveStop:
                transform.localPosition = localIdle;
                //LevelManager.Instance?.Events?.onKeysUp?.Invoke();
                break;
        }
        StartCoroutine(UpdateTransform());
        yield break;
    }
}
