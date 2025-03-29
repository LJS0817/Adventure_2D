using UnityEngine;

public class Player : MonoBehaviour
{
    Animator _ani;
    Rigidbody2D _rig;

    public float Speed;
    public float JumpForce;
    Vector2 _velocity;

    bool _isJump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isJump = false;
        _velocity = Vector2.zero;
        _ani = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
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
            _isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            _ani.SetTrigger("Land");
        }
    }

    void jump()
    {
        if(!_isJump && Input.GetKeyDown(KeyCode.Space))
        {
            _ani.SetTrigger("Jump");
        }
    }

    public void jumpTrigger()
    {
        _isJump = true;
    }

    void move()
    {
        Vector2 vel = transform.right * Input.GetAxisRaw("Horizontal") * Speed;

        changeMovedState(vel.x);
        turnAround(vel.x);

        _velocity = vel;
    }

    void physicsJumpMovement()
    {
        if(_isJump)
        {
            _rig.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            _isJump = false;
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
        if ((velX < 0 && transform.localScale.x > 0) || (velX >= 0 && transform.localScale.x < 0))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void changeMovedState(float velX)
    {
        if (_ani.GetBool("Moved") && Mathf.Abs(velX) <= 0f) _ani.SetBool("Moved", false);
        else if (!_ani.GetBool("Moved") && Mathf.Abs(velX) > 0f) _ani.SetBool("Moved", true);
    }
}
