using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f; 
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    public float JumpHeight = 1.2f;
    public float Gravity = -9.81f;
    public float GravityMultiplier = 3.0f;
    public float Velocity;

    [Space(10)]
    public float JumpTimeout = 0.50f;
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers; 
    
    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // Component
    private CharacterController _controller;

    // playerClass
    private Player player; 
    private PlayerInputs playerinputs;
    private PlayerAnimController playerAnimController;
    private State currentState;

    // player Type
    public PlayerToolType playerToolType;
    public PlayerStateType playerState;


    private GameObject _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        player = GetComponent<Player>();
        playerinputs = GetComponent<PlayerInputs>();
        playerAnimController = GetComponent<PlayerAnimController>();

        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;

        playerAnimController.Init();

        //Input Callback
        playerinputs.OnChangeStateInputValue += OnChangeStateInputValue;
        playerinputs.OnChangeNumberInputValue += OnChangeNumberInputValue;

        TransitionToState(gameObject.AddComponent<Idle>());
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    TransitionToState(gameObject.AddComponent<Fishing>());
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    TransitionToState(new Ground());
        //}
        //else if (Input.GetKeyDown(KeyCode.R))
        //{
        //    TransitionToState(new Water());
        //}

        currentState.UpdateState();

        if (!GameManager.Instance.isPlayerStop)
        {
            JumpAndGravity();
            Move();
        }
    }

    private void TransitionToState(State newState)
    {
        Debug.Log("Current State : " + currentState);
        Debug.Log("New State : " + newState);

        if (newState != currentState)
        {
            ExitState();

            Destroy(currentState);
            currentState = newState;
            EnterState();
        }
    }

    public void EnterState()
    {
        StateData stateData = new()
        {
            anim = playerAnimController.anim,

            onActionEnd = OnActionEnd
        };

        currentState.Init(stateData);
        currentState.EnterState();

        playerAnimController.PlayAnimCrossFade(currentState.GetType().Name, 0.3f);
    }

    public void ExitState()
    {
        currentState?.ExitState();
    }

    public void OnActionEnd()
    {
        playerinputs.isActioning = false;
        OnChangeStateInputValue();
    }

    private RaycastHit hit;
    private float maxDistance = 500f;

    public void OnChangeStateInputValue()
    {
        if (playerinputs.isActioning)
        {
            switch (playerToolType)
            {
                case PlayerToolType.Idle:

                    playerinputs.isActioning = false;

                    if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, maxDistance))
                    {
                        Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
                        Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
                        
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Crops"))
                        {
                            Debug.Log("Hit Crops !");
                            hit.collider.gameObject.GetComponentInParent<Crops>().HarvestCrops();
                        }
                    }

                    break;
                case PlayerToolType.Hoe:

                    TransitionToState(gameObject.AddComponent<Fishing>());

                    break;
                case PlayerToolType.WaterCan:

                    TransitionToState(gameObject.AddComponent<Water>());

                    break;
            }
        }
        else
        {

            if (playerinputs.move == Vector2.zero)
            {
                TransitionToState(gameObject.AddComponent<Idle>());
            }
            else
            {
                if (!playerinputs.sprint)
                {
                    TransitionToState(gameObject.AddComponent<Walk>());
                }
                else
                {
                    TransitionToState(gameObject.AddComponent<Run>());
                }
            }
        }
    }

    public void OnChangeNumberInputValue()
    {
        switch (playerinputs.number)
        {
            case 1:
                playerToolType = PlayerToolType.Idle;
                break;
            case 2:
                playerToolType = PlayerToolType.Hoe;
                break;
            case 3:
                playerToolType = PlayerToolType.WaterCan;
                break;
        }

        player.OnTool(playerToolType);
    }

    private void Move()
    {
        float targetSpeed = playerinputs.sprint ? SprintSpeed : MoveSpeed;

        if (playerinputs.move == Vector2.zero || playerinputs.isActioning) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = playerinputs.analogMovement ? playerinputs.move.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

        Vector3 inputDirection = new Vector3(playerinputs.move.x, 0.0f, playerinputs.move.y).normalized;

        if (playerinputs.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        //if (_controller.isGrounded && Velocity < 0.0f)
        //{
        //    Velocity = -1.0f;
        //}
        //else
        //{
        //    Velocity += Gravity * GravityMultiplier * Time.deltaTime;
        //    targetDirection.y = Velocity;
        //}

        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

}
