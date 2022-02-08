//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Experimental.U2D.IK;

//public class PlayerController : Player
//{
//    public LayerMask mask;
//    private Rigidbody2D body;
//    public Transform raycastParent;

//    private GameObject header;

//    private void Start()
//    {
//        header = transform.GetComponentInChildren<IKManager2D>().gameObject;
//        body = gameObject.GetComponent<Rigidbody2D>();
//    }
//    private Vector2 lastPos;

//    private void FixedUpdate()
//    {
//        RaycastHit2D center = Physics2D.Raycast((Vector2)transform.position + new Vector2(0, 2f), Vector2.down, 3f, mask);
//        if (center.collider != null)
//        {
//            currentControllerData.onGround = true;
//            currentControllerData.inJump = false;
//        }
//        else
//        {
//            currentControllerData.onGround = false;
//            currentControllerData.inJump = true;
//        }
//        if (lastPos != (Vector2)transform.position)
//        {
//            if (transform.position.x < lastPos.x)
//            {
//                currentControllerData.moveStatus = MoveStatus.moveLeft;
//            }
//            if (transform.position.x > lastPos.x)
//            {
//                currentControllerData.moveStatus = MoveStatus.moveRight;
//            }
//            if (Vector2.Distance(transform.position, lastPos) < 0.03f)
//            {

//                currentControllerData.moveStatus = MoveStatus.moveStop;
//            }
//            lastPos = transform.position;
//        }
//        switch (currentControllerData.moveStatus)
//        {
//            case MoveStatus.moveLeft:

//                transform.localScale = new Vector3(1, 1, 1);//Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 16f);
//                break;
//            case MoveStatus.moveRight:
//                transform.localScale = new Vector3(-1, 1, 1);// Vector3.Lerp(transform.localScale, new Vector3(-1, 1, 1), Time.deltaTime * 16f);
//                break;
//            case MoveStatus.moveStop:

//                break;
//        }


//    }
//    private void Update()
//    {
//        if (body.velocity.x > currentControllerData.speed)
//        {
//            Debug.Log("Вася давай помедленнее ");
//            body.velocity -= new Vector2(Time.deltaTime * currentControllerData.speed, 0f);
//        }
//        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W))
//        {
//            LevelManager.Instance?.Events?.onKeysUp?.Invoke();
//        }
//        //Debug.DrawRay((Vector2)transform.position + new Vector2(0, 2f), Vector2.down * 2.2f, Color.red);

//        if (Input.GetKey(KeyCode.A))
//        {
//            currentControllerData.directionLeft = true;
//            body.velocity = new Vector2(-currentControllerData.speed, body.velocity.y);
//        }

//        if (Input.GetKey(KeyCode.D))
//        {
//            currentControllerData.directionLeft = false;
//            body.velocity = new Vector2(currentControllerData.speed, body.velocity.y);
//        }

//        if (Input.GetKeyUp(KeyCode.Space))
//        {
//            if (currentControllerData.onGround)
//            {
//                body.AddForce(new Vector2(body.velocity.x, currentControllerData.jumpPower), ForceMode2D.Impulse);
//            }
//        }
//    }
//}
