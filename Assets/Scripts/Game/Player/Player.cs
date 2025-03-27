using UnityEngine;

public class Player : MonoBehaviour
{
    const float movedVelocity = 0.2f;
    Animator _ani;
    Rigidbody2D _rig;

    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ani = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Equals("Ground"))
        {
            _ani.SetTrigger("Land");
        }
    }

    void move()
    {
        Vector2 vel = transform.right * Input.GetAxis("Horizontal") * Speed * Time.deltaTime;

        changeMovedState(vel.x);
        turnAround(vel.x);

        _rig.linearVelocity = vel;
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
        if (_ani.GetBool("Moved") && Mathf.Abs(velX) <= movedVelocity) _ani.SetBool("Moved", false);
        else if (!_ani.GetBool("Moved") && Mathf.Abs(velX) > movedVelocity) _ani.SetBool("Moved", true);
    }
}
