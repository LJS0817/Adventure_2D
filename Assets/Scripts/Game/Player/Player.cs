using NUnit.Framework;
using UnityEngine;

public enum PLAYER_STATE { 
    E_IDLE,
    E_READY,
    E_ANIMATED,

    E_JUMPING,
}

public class Player : MonoBehaviour
{
    Animator _ani;
    Rigidbody2D _rig;
    SpriteRenderer _renderer;

    public float Speed;
    public float JumpForce;
    Vector2 _velocity;

    PLAYER_STATE _state;

    PlayerInputController _inputController;
    SkillController _skill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _state = PLAYER_STATE.E_IDLE;
        _velocity = Vector2.zero;
        
        _ani = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        _inputController = GetComponent<PlayerInputController>();

        _skill.GetComponent<SkillController>();
        _skill.Init(new FastMovement());
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    private void FixedUpdate()
    {
        physicsMove();
        physicsJumpMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            setState(PLAYER_STATE.E_IDLE);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            _ani.SetTrigger("Land");
        }
    }

    bool compareState(params PLAYER_STATE[] state)
    {
        bool rst = false;

        for(int i = 0; i < state.Length; i++)
        {
            rst = rst || _state == state[i];
        }

        return rst;
    }

    void setState(PLAYER_STATE st)
    {
        _state = st;
    }

    void jump()
    {
        if(compareState(PLAYER_STATE.E_IDLE) && Input.GetKeyDown(KeyCode.Space))
        {
            setState(PLAYER_STATE.E_READY);
            _ani.SetTrigger("Jump");
        }
    }

    public void jumpTrigger()
    {
        setState(PLAYER_STATE.E_ANIMATED);
    }

    void move()
    {
        Vector2 vel = _inputController.GetHorizontalMovement(Speed);

        changeMovedState(vel.x);
        turnAround(vel.x);

        _velocity = vel;
    }

    void physicsJumpMovement()
    {
        if(compareState(PLAYER_STATE.E_ANIMATED))
        {
            _rig.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            setState(PLAYER_STATE.E_JUMPING);
        }
    }

    void physicsMove()
    {
        _velocity.y = _rig.linearVelocity.y;
        _velocity.x *= Time.fixedDeltaTime;
        _rig.linearVelocity = _velocity;
    }

    void turnAround(float velX)
    {
        if ((velX < 0 && !_renderer.flipX) || (velX >= 0 && _renderer.flipX))
        {
            _renderer.flipX = !_renderer.flipX;
        }
    }

    void changeMovedState(float velX)
    {
        if (_ani.GetBool("Moved") && Mathf.Abs(velX) <= 0f) _ani.SetBool("Moved", false);
        else if (!_ani.GetBool("Moved") && Mathf.Abs(velX) > 0f) _ani.SetBool("Moved", true);
    }

    public void SkillActivate()
    {
        _skill.Activate(_skill.GetCurrentSkillIndex());
    }
}
