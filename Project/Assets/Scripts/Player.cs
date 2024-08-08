using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerStatus
    {
        Waiting,
        Walk,
        Jump,
        TransitioningJump,
        Glide,
        Dead,
        Fall
    };
    public static Player instance { get; private set; }

    FSM<PlayerStatus> playerFSM = new FSM<PlayerStatus>(PlayerStatus.Waiting);
    private Vector3 accelorator;
    //public Vector3 gravity;

    private bool isJump = false;            // 현재 점프된 상태인가?
    private bool canGlide = true;           // 글라이드가 가능한 상태인가?

    public Rigidbody rigidbody;
    public Collider physicsCollider;
    public Collider triggerCollider;
    public float ForwardAccelration = 4f;
    public float deathDepth = -10f;
    public float gravity = 9.81f;
    public float horizontalSpeed;           // 물체의 횡속력
    public float glideFallSpeed;            // 물체의 글라이딩 낙하속력
    public float jumpPower;                 // 첨프의 세기
    private float jumpingTime = 0;          // 점프키를 눌리는 시간
    public float risingTime;                // 최대 상승 시간
    private HashSet<Collider> interactingColliders = new HashSet<Collider>();

    private bool transitionAtClimax = false;
    private float floatingTime = 0;

    public PlayerStatus GetPlayerStatus()
    {
        return playerFSM.currentState;
    }
    public void SetPlayerStatusWalk()
    {
        playerFSM.currentState = PlayerStatus.Walk;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerFSM = new FSM<PlayerStatus>(PlayerStatus.Waiting);

        playerFSM.transitions[PlayerStatus.Walk].Add((PlayerStatus.Jump,
            () =>
            {
                return (Input.GetButtonDown("Jump")) == true &&
                !StageManager.instance.stageRequirementsMet();
            }
        ));
        playerFSM.transitions[PlayerStatus.Walk].Add((PlayerStatus.Fall,
            () =>
            {
                return isJump;
            }
        ));

        playerFSM.transitions[PlayerStatus.Walk].Add((PlayerStatus.TransitioningJump,
            () =>
            {
                return (Input.GetButtonDown("Jump")) == true &&
                StageManager.instance.stageRequirementsMet();
            }
        ));

        playerFSM.transitions[PlayerStatus.Jump].Add((PlayerStatus.Walk,
            () => { return isJump == false; }
        ));

        playerFSM.transitions[PlayerStatus.Fall].Add((PlayerStatus.Walk,
            () => { return isJump == false; }
        ));

        playerFSM.transitions[PlayerStatus.TransitioningJump].Add((PlayerStatus.Walk,
            () => { return isJump == false; }
        ));

        playerFSM.transitions[PlayerStatus.Jump].Add((PlayerStatus.Glide,
            () => { return (Input.GetButtonDown("Jump")) == true; }
        ));

        playerFSM.transitions[PlayerStatus.Glide].Add((PlayerStatus.Walk,
            () => { return isJump == false; }
        ));

        playerFSM.transitions[PlayerStatus.Glide].Add((PlayerStatus.Jump,
            () => { return (Input.GetButtonUp("Jump")) == true && isJump == true; }
        ));

        Func<bool> deathCondition = () => { return transform.position.y < deathDepth; };
        playerFSM.transitions[PlayerStatus.Walk].Add((PlayerStatus.Dead, deathCondition));
        playerFSM.transitions[PlayerStatus.Jump].Add((PlayerStatus.Dead, deathCondition));
        playerFSM.transitions[PlayerStatus.Glide].Add((PlayerStatus.Dead, deathCondition));
        playerFSM.transitions[PlayerStatus.Fall].Add((PlayerStatus.Dead, deathCondition));
        playerFSM.transitions[PlayerStatus.TransitioningJump].Add((PlayerStatus.Dead, deathCondition));

        playerFSM.engageAction[PlayerStatus.Walk] = () => WalkEngage();
        playerFSM.engageAction[PlayerStatus.Jump] = () => JumpEngage();
        playerFSM.engageAction[PlayerStatus.Glide] = () => GlideEngage();
        playerFSM.engageAction[PlayerStatus.TransitioningJump] = () => TransitioningJumpEngage();
        playerFSM.engageAction[PlayerStatus.Dead] = () => DeathEngage();

        playerFSM.updateAction[PlayerStatus.Walk] = () => WalkState();
        playerFSM.updateAction[PlayerStatus.Jump] = () => JumpState();
        playerFSM.updateAction[PlayerStatus.Glide] = () => GlideState();
        playerFSM.updateAction[PlayerStatus.TransitioningJump] = TransitioningJumpState;



        Func<bool> a = () => true;
        accelorator = new Vector3();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
    }
    private void Update()
    {
        //isJump = isSteppingOnSomething() == null;
        //UnityEngine.Debug.Log(GetPlayerStatus() + "dt : " + Time.deltaTime);
        if (GetPlayerStatus() == PlayerStatus.Glide)
            UnityEngine.Debug.Log(GetPlayerStatus());


        if (interactingColliders.Count == 0)
        {
            floatingTime += Time.deltaTime;
            if (floatingTime > 0.1f)
                isJump = true;
        }
        else
            floatingTime = 0;

        playerFSM.UpdateState();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerFSM.currentState != PlayerStatus.Jump)
            if (Vector3.Dot(-transform.up, -transform.position + collision.GetContact(0).point) > 0)
                isJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        interactingColliders.Add(collision.collider);
        if (Vector3.Dot(-transform.up, -transform.position + collision.GetContact(0).point) > 0)
        {
            UnityEngine.Debug.Log("stepped on : " + collision.gameObject.name);
            isJump = false;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        interactingColliders.Remove(collision.collider);
    }

    void IdleState()
    {
    }

    void JumpEngage()
    {
        if (playerFSM.previousState == PlayerStatus.Walk)
            GlobalSounds.PlayRandomSound("jump");
        isJump = true;
        UnityEngine.Debug.Log("Jump됐당.");
        jumpingTime = 0;
    }

    void WalkEngage()
    {
        rigidbody.velocity = transform.forward * horizontalSpeed;
        UnityEngine.Debug.Log("Walk됐당.");
    }

    void GlideEngage()
    {
        UnityEngine.Debug.Log("Glide됐당.");
    }
    void TransitioningJumpEngage()
    {
        StartCoroutine(TransitionEngageCorutine());
    }
    IEnumerator TransitionEngageCorutine()
    {
        isJump = true;
        float verticalDistance = GlobalTransform.entityById["moon"].position.y - transform.position.y;
        float initialRisingSpeed = MathF.Sqrt(2 * verticalDistance * gravity);
        rigidbody.velocity = new Vector3(0, initialRisingSpeed, rigidbody.velocity.z);
        physicsCollider.enabled = false;

        yield return new WaitForSeconds(1f);
        physicsCollider.enabled = true;
        rigidbody.velocity -= Vector3.forward * rigidbody.velocity.z;

        StageManager.instance.Proceed();
    }
    void DeathEngage()
    {
        StartCoroutine(DeathFadeOutIn());
        GlobalSounds.PlayRandomSound("death");
    }
    IEnumerator DeathFadeOutIn()
    {
        GlobalEvent.entityById["fade out"].Invoke();
        yield return new WaitForSeconds(2f);
        transform.position = Vector3.zero;
        playerFSM.currentState = PlayerStatus.Waiting;
        StageManager.instance.ResetStageModules();
        StageManager.instance.stageRequirementsAccquired = 0;
        GlobalEvent.entityById["fade in"].Invoke();

        yield return new WaitForSeconds(1f);
        playerFSM.currentState = PlayerStatus.Walk;
    }

    void JumpState()
    {
        if (Input.GetButton("Jump"))
        {
            isJump = true;
            jumpingTime += Time.deltaTime;
            //UnityEngine.Debug.Log(jumpingTime);
            if (jumpingTime <= risingTime)
            {
                //rigidbody.AddForce(Vector3.up * (jumpPower * time), ForceMode.Acceleration);
                //rigidbody.velocity = (Vector3.forward * horizontalSpeed) + (Vector3.up * jumpPower);
                rigidbody.velocity += Vector3.up * (jumpPower - Vector3.Dot(rigidbody.velocity, Vector3.up));
            }
            else
            {
                //rigidbody.velocity +=
                //transform.forward *
                //(horizontalSpeed - Vector3.Dot(transform.forward, rigidbody.velocity));
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpingTime += risingTime;
            UnityEngine.Debug.Log("jump is done");
        }
    }
    void TransitioningJumpState()
    {
        //if (rigidbody.velocity.y < 0 && !transitionAtClimax)
        //{
        //    transitionAtClimax = true;
        //    StageManager.instance.Proceed();
        //}
    }
    void WalkState()
    {
        rigidbody.velocity +=
            transform.forward *
            Mathf.Min(ForwardAccelration * Time.deltaTime, horizontalSpeed - Vector3.Dot(transform.forward, rigidbody.velocity));
        //UnityEngine.Debug.Log(rigidbody.velocity);
    }

    void GlideState()
    {
        if (Input.GetButton("Jump") && canGlide)
        {
            //UnityEngine.Debug.Log("Glide됐당.");
            //Vector3 InitSpeed = Vector3.forward * glideFallSpeed;
            //rigidbody.velocity = InitSpeed;
            //rigidbody.velocity +=
            //    (-transform.up *
            //    (glideFallSpeed - Vector3.Dot(-transform.up, rigidbody.velocity))) + (transform.forward * 2 *
            //(horizontalSpeed - Vector3.Dot(transform.forward, rigidbody.velocity)));
            rigidbody.velocity +=
                (-transform.up *
                (glideFallSpeed - Vector3.Dot(-transform.up, rigidbody.velocity)));
            rigidbody.velocity +=
                transform.forward *
                Mathf.Min(ForwardAccelration * Time.deltaTime, horizontalSpeed - Vector3.Dot(transform.forward, rigidbody.velocity));
            //+ (transform.forward * 2 *
            //(horizontalSpeed - Vector3.Dot(transform.forward, rigidbody.velocity)));
        }
        if (Input.GetButtonUp("Jump"))
        {
            // 아래의 주석을 지우면 한번으로 바뀐다.
            //canGlide = false;
            IdleState();
        }
    }

    Collider isSteppingOnSomething()
    {
        const float steppingThreshHold = 0.1f;
        foreach (var eachCollider in interactingColliders)
        {
            UnityEngine.Debug.Log(eachCollider.gameObject.name + " : " + Vector3.Dot(eachCollider.transform.up, rigidbody.velocity));
            UnityEngine.Debug.Log("status is " + playerFSM.currentState);
            if (Vector3.Dot(eachCollider.transform.up, rigidbody.velocity) < steppingThreshHold)
                return eachCollider;
        }
        return null;
    }
    void AccelerateForward()
    {
    }
}
