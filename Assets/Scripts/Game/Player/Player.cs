using UnityEngine;

public enum PLAYER_STATE { 
    E_IDLE,
    E_READY,
    E_ANIMATED,

    E_JUMPING,
    E_FAST_MOVE,
}

public class Player : MonoBehaviour
{
    Animator _ani;
    Rigidbody2D _rig;
    SpriteRenderer _renderer;

    public float Speed;
    public float JumpForce;
    Vector2 _velocity;

    bool _useGravity;
    Vector2 _gravity;

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

        _skill = GetComponent<SkillController>();
        _skill.Init(new FastMovement());
        initGravity();
        _useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    private void FixedUpdate()
    {
        forceGravity();
        physicsMove();
        physicsJumpMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            SetState(PLAYER_STATE.E_IDLE);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            _ani.SetTrigger("Land");
        }
    }

    void initGravity()
    {
        _gravity = transform.up * -9.81f * 5f;
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

    public void SetState(PLAYER_STATE st)
    {
        _state = st;
        Debug.Log(_state);
    }

    void jump()
    {
        if(compareState(PLAYER_STATE.E_IDLE) && Input.GetKeyDown(KeyCode.Space))
        {
            SetState(PLAYER_STATE.E_READY);
            _ani.SetTrigger("Jump");
        }
    }

    public void jumpTrigger()
    {
        SetState(PLAYER_STATE.E_ANIMATED);
    }

    void move()
    {
        if (compareState(PLAYER_STATE.E_FAST_MOVE)) return;
        float xSpeed = _inputController.GetHorizontalMovement(Speed);

        changeMovedState(xSpeed);
        turnAround(xSpeed);

        _velocity = transform.right * xSpeed;
    }

    void forceGravity()
    {
        if(_useGravity) _rig.AddForce(_gravity, ForceMode2D.Force);
    }

    void physicsJumpMovement()
    {
        if(compareState(PLAYER_STATE.E_ANIMATED))
        {
            _rig.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            SetState(PLAYER_STATE.E_JUMPING);
        }
    }

    void physicsMove()
    {
        _rig.position += _velocity * Time.fixedDeltaTime;
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
        _skill.Activate(_skill.GetCurrentSkillIndex(), this);
    }

    public Rigidbody2D GetRigibbody() { return _rig; }

    public PlayerInputController GetInputController() { return _inputController; }

    public void UseGravity(bool b)
    { 
        _useGravity = b;
        if(b)
        {
            initGravity();
        }
    }
}
