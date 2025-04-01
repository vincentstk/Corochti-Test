using System;
using Platformer.Mechanics;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Component Configs

    private PlayerInputCallbackSystem _inputSystem;
    private PlayerController _movementSystem;
    private Health _healthSystem;
    private AnimationSystem _animationSystem;
    private AudioSystem _audioSystem;
    private PlayerHudSystem _hudSystem;
    private CollectSystem _collectSystem;
    private GfxSystem _gfxSystem;

    #endregion

    #region Properties

    public AnimationSystem AnimationSystem
    {
        get => _animationSystem;
    }

    public PlayerController MovementSystem
    {
        get => _movementSystem;
    }

    public Health HealthSystem
    {
        get => _healthSystem;
    }

    public PlayerInputCallbackSystem InputSystem
    {
        get => _inputSystem;
    }

    public AudioSystem AudioSystem
    {
        get => _audioSystem;
    }

    public CollectSystem CollectSystem
    {
        get => _collectSystem;
    }

    public GfxSystem GfxSystem
    {
        get => _gfxSystem;
    }

    #endregion

    private void Awake()
    {
        _inputSystem = new PlayerInputCallbackSystem();
        _movementSystem = GetComponent<PlayerController>();
        _animationSystem = GetComponent<AnimationSystem>();
        _audioSystem = GetComponent<AudioSystem>();
        _healthSystem = GetComponent<Health>();
        _hudSystem = GetComponent<PlayerHudSystem>();
        _collectSystem = GetComponent<CollectSystem>();
        _gfxSystem = GetComponent<GfxSystem>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthSystem.Init(_hudSystem);
        _audioSystem.Init();
        _animationSystem.Init();
        _movementSystem.Init(_animationSystem, _audioSystem, _gfxSystem);
        _hudSystem.CreateHeart(_healthSystem.maxHP);
        _collectSystem.Init(_hudSystem);
        _gfxSystem.Init();
        _inputSystem.OnMove += _movementSystem.Movement;
        _inputSystem.OnJump += _movementSystem.Jump;
        _inputSystem.State = SystemState.Enable;
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    public void EnterFlyState()
    {
        _movementSystem.jumpState = PlayerController.JumpState.FlyWithBubble;
        _gfxSystem.ChangeColor(true);
        _movementSystem.TakeOff();
    }
}
