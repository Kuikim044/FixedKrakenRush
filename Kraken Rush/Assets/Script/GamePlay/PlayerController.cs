using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum SIDE { Left, Mid, Right }
public enum HitX { Left, Mid, Right, None }
public enum HitY { Up, Mid, Down, Low, None }
public enum HitZ { Forward, Mid, Backward, None }

public class PlayerController : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;

    public bool swipeLeft, swipeRight, swipeUp, swipeDown;

    private float newXPos = 0f;
    public float xValue;

    public Animator playerAnim;

    private CharacterController controller;

    private float x;
    public float speedDodge;
    public float jumpPower = 7f;
    public  static float forwardSpeed;
    //public float speedcooldown;
    private float y;

    public bool inJump;
    public bool inRoll;
    private float ColHeight;
    private float ColCenterY;

    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;


    public Vector3 jumpDirection;
    public  int life = 3;
    public Image[] lifeImage;

    public static bool isGameover;
    public static bool isProtection;


    private bool isCooldown = false;
    private float cooldownDuration = 2f;
    private float cooldownTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        isGameover = false;
        isProtection = false;
        transform.position = Vector3.zero;

        controller = GetComponent<CharacterController>();
        ColHeight = controller.height;
        ColCenterY = controller.center.y;

    }

    // Update is called once per frame
    void Update()
    {
        #region Life Player

        if (life <= 0)
            life = 0;

        if (isGameover)
        {
            forwardSpeed = 0f;
            return;
        }
        if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= cooldownDuration)
            {
                isCooldown = false;
            }
        }

        #endregion
        swipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        swipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        swipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        swipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float touchDeltaX = touch.deltaPosition.x;
            float touchDeltaY = touch.deltaPosition.y;

            if (touch.phase == TouchPhase.Began)
            {
                swipeLeft = false;
                swipeRight = false;
                swipeUp = false;
                swipeDown = false;
            }
            else if (touch.phase == TouchPhase.Ended)
            {


                if (Mathf.Abs(touchDeltaX) > Mathf.Abs(touchDeltaY))
                {
                    if (touchDeltaX < 0)
                        swipeLeft = true;
                    else if (touchDeltaX > 0)
                        swipeRight = true;
                }     
            }
            else if(touch.phase == TouchPhase.Moved)
            {
             
                if (Mathf.Abs(touchDeltaX) < Mathf.Abs(touchDeltaY))
                {
                    if (touchDeltaY > 0)
                        swipeUp = true;
                    else if (touchDeltaY < 0)
                        swipeDown = true;
                }
            }
        }
        if (swipeLeft && !inRoll)
        {
            if (m_Side == SIDE.Mid)
            {
                newXPos = -xValue;
                m_Side = SIDE.Left;
                playerAnim.CrossFadeInFixedTime("MoveLeft", 0f);

            }
            else if (m_Side == SIDE.Right)
            {
                newXPos = 0;
                m_Side = SIDE.Mid;
                playerAnim.CrossFadeInFixedTime("MoveLeft", 0f);


            }
        }
        if (swipeRight && !inRoll)
        {
            if (m_Side == SIDE.Mid)
            {
                newXPos = xValue;
                m_Side = SIDE.Right;
                playerAnim.CrossFadeInFixedTime("MoveRight", 0f);

            }
            else if (m_Side == SIDE.Left)
            {
                newXPos = 0;
                m_Side = SIDE.Mid;
                playerAnim.CrossFadeInFixedTime("MoveRight", 0f);

            }
        }

        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, forwardSpeed * Time.deltaTime);

        x = Mathf.Lerp(x, newXPos, Time.deltaTime * speedDodge);
        controller.Move(moveVector);
        Jump();
        Roll();

        Debug.Log(life);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            if (swipeUp)
            {
                jumpDirection = transform.forward;
                y = jumpPower;
                playerAnim.CrossFadeInFixedTime("Jump", 0f);
                inJump = true;
            }
        }
        else
        {
            y -= jumpPower * 3.5f * Time.deltaTime;

            if (controller.velocity.y < -0.1f)
                inJump = false;
        }

       
    }

    internal float RollCounter;
    private void Roll()
    {
        RollCounter -= Time.deltaTime;

        if (RollCounter <= 0f)
        {
            RollCounter = 0f;
            controller.center = new Vector3(0, ColCenterY, 0);
            controller.height = ColHeight;
            inRoll = false;
        }

        if (swipeDown)
        {
            RollCounter = 0.2f;
            playerAnim.CrossFadeInFixedTime("Duck", 0.1f);

            inRoll = true;

            controller.center = new Vector3(0, ColCenterY / 2, 0);
            controller.height = ColHeight / 2;

            y -= 10f;
            inJump = false;

           
        }
        
    }

    public void OnCharactorColliderHit(Collider col)
    {
        hitX = GetHitX(col);
        hitY = GetHitY(col);
        hitZ = GetHitZ(col);

        if(hitZ == HitZ.Forward && hitX == HitX.Mid)
        {
            if(hitY == HitY.Low)
            {
                TakeDmgPlayer();

            }
            else if(hitY == HitY.Down)
            {
                TakeDmgPlayer();


            }
            else if (hitY == HitY.Mid)
            {
                TakeDmgPlayer();

            }
            else if(hitY == HitY.Up)
            {
                TakeDmgPlayer();


            }
        }
        else if (hitX == HitX.Mid)
        {
            if(hitX == HitX.Right)
            {
                TakeDmgPlayer();


            }
            else if (hitX == HitX.Left)
            {
                TakeDmgPlayer();

            }
        }
        else
        {
            if(hitX == HitX.Right)
            {
                TakeDmgPlayer();


            }
            else if(hitX == HitX.Left)
            {
                TakeDmgPlayer();

            }
        }

    }

    private void TakeDmgPlayer()
    {
        if (isProtection)
            return;

        if (isCooldown)
            return;

        life -= 1;
        CheckImageLife();

        // ????????????????
        StartCooldown();
    }

    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;

        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = (min_x + max_x) / 2f - col.bounds.min.x;

        HitX hit;
        if (average > col.bounds.size.x - 0.33f)
            hit = HitX.Right;
        else if (average < 0.33f)
            hit = HitX.Left;
        else
            hit = HitX.Mid;
        return hit;
    }

    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;

        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y) / char_bounds.size.y;

        HitY hit;
        if (average < 0.17f)
            hit = HitY.Low;
        else if (average < 0.33f)
            hit = HitY.Down;
        else if (average < 0.66f)
            hit = HitY.Mid;
        else
            hit = HitY.Up;
        return hit;
    }

    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;

        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - char_bounds.min.z) / char_bounds.size.z;

        HitZ hit;
        if (average < 0.33f)
            hit = HitZ.Backward;
        else if (average < 0.66f)
            hit = HitZ.Mid;
        else
            hit = HitZ.Forward;
        return hit;
    }


    public void CheckImageLife()
    {        

        int numlife = 3;
        for (int i = numlife - 1; i >= life; i--)
        {
            lifeImage[i].gameObject.SetActive(false);
        }

        if (life <= 0)
            isGameover = true;         
               

    }
    private void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = 0f;
    }
}
