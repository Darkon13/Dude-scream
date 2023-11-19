using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private KeyListener _listener;
    [SerializeField] private CompositeCollider2D _platformCollider;
    [SerializeField] private float _acceleraion;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _airAccelerationModifier;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxHealth;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Animator _animator;
    private KeyBinder _binder;
    private bool _isGrounded;
    private bool _isRunning;
    private bool _isDoubleJumped;
    private bool _inited = false;
    private Vector2 _point;
    private Vector2[] _colliderBounds;
    private Vector2 _enemyPoint;
    private int _health;
    //private Coroutine _coroutine;
    //private List<RaycastHit2D> _results;

    public int MaxHealth => _maxHealth;
    public int Health => _health;

    public event UnityAction<Vector2> CollisionEntered;
    public event UnityAction<int> HealthChanged;

    public void AddHealth(int amount)
    {
        if(amount > 0)
        {
            int healthBefore = _health;

            _health = Mathf.Min(_health + amount, _maxHealth);

            if (_health != healthBefore)
            {
                HealthChanged?.Invoke(_health);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            int healthBefore = _health;

            _health = Mathf.Max(_health - damage, 0);

            if(_health != healthBefore)
            {
                HealthChanged?.Invoke(_health);
            }
        }
    }

    public void TestButton1()
    {
        TakeDamage(20);
    }

    public void TestButton2()
    {
        AddHealth(20);
    }

    public void Init()
    {
        if(_inited == false)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();

            _health = _maxHealth;
            _colliderBounds = new Vector2[3];

            _binder = new KeyBinder(_listener);
            //_results = new List<RaycastHit2D>();
            _isGrounded = false;

            _point = Vector2.zero;

            _binder.BindCommand(InputTypes.KeyPressed, KeyCode.A, MoveLeft);
            _binder.BindCommand(InputTypes.KeyPressed, KeyCode.D, MoveRight);
            _binder.BindCommand(InputTypes.KeyDown, KeyCode.Space, Jump);
            _binder.BindCommand(InputTypes.KeyDown, KeyCode.S, MoveDown);

            CollisionEntered += OnCollisionEntered;

            _inited = true;
        }
    }

    //private void Awake()
    //{
    //    _rigidbody2D = GetComponent<Rigidbody2D>();
    //    _collider2D = GetComponent<Collider2D>();
    //    _animator = GetComponent<Animator>();

    //    _health = _maxHealth;

    //    _binder = new KeyBinder(_listener);
    //    //_results = new List<RaycastHit2D>();
    //    _isGrounded = false;

    //    _point = Vector2.zero;
    //}

    private void OnEnable()
    {
        if(_inited == true)
        {
            _binder.BindCommand(InputTypes.KeyPressed, KeyCode.A, MoveLeft);
            _binder.BindCommand(InputTypes.KeyPressed, KeyCode.D, MoveRight);
            _binder.BindCommand(InputTypes.KeyDown, KeyCode.Space, Jump);
            _binder.BindCommand(InputTypes.KeyDown, KeyCode.S, MoveDown);

            CollisionEntered += OnCollisionEntered;
        }
    }

    private void OnDisable()
    {
        if (_inited == true)
        {
            _binder.UnbindAllCommands();

            CollisionEntered -= OnCollisionEntered;
        }
    }

    private void FixedUpdate()
    {
        if(_isRunning == true)
        {
            _animator.SetBool("IsRunning", false);
            _isRunning = false;
        }

        if(_rigidbody2D.velocity.y < 0 && _isGrounded == false)
        {
            _animator.SetBool("IsFalling", true);
        }
        else if(_isGrounded == true)
        {
            _animator.SetBool("IsFalling", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(_isGrounded == false)
        {
            foreach(ContactPoint2D contact in collision.contacts)
            {
                if ((int)contact.normal.x == 0 && contact.normal.y > 0.1 && Mathf.Abs(_collider2D.bounds.min.y - contact.point.y) <= 0.01f)
                {
                    _isGrounded = true;
                    _isDoubleJumped = false;
                    CollisionEntered?.Invoke(contact.point);

                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyMover enemyMover))
        {
            _enemyPoint = collision.contacts[0].point;

            if(collision.gameObject.TryGetComponent(out Collider2D collider2D))
            {
                _colliderBounds[0] = collider2D.bounds.max;
                _colliderBounds[1] = collider2D.bounds.min;
                _colliderBounds[2] = collider2D.bounds.center;

                Debug.Log(Mathf.Abs(collider2D.bounds.max.y - _enemyPoint.y) < 1);

                if((collider2D.bounds.max.y - _enemyPoint.y) < 0.1)
                {
                    Debug.Log("Enemy Dead");
                }
                else
                {
                    Debug.Log("Player hurt");
                }
            }
            //Debug.Log(collision.contacts[0]);
        }
    }

    private void MoveLeft() => Move(Vector2.left);

    private void MoveRight() => Move(Vector2.right);
    
    private void MoveDown()
    {
        Debug.Log("123");

        Physics2D.IgnoreCollision(_collider2D, _platformCollider);
        Move(Vector2.down);

        StartCoroutine(nameof(ReturnCollision));
    }

    private IEnumerator ReturnCollision()
    {
        yield return new WaitForSeconds(0.35f);

        Physics2D.IgnoreCollision(_collider2D, _platformCollider, false);
    }

    private void Move(Vector2 direction)
    {
        float acceleration = direction.x * _acceleraion * Time.deltaTime;

        if (_isGrounded == false)
        {
            acceleration *= _airAccelerationModifier;
        }
        else
        {
            _animator.SetBool("IsRunning", true);
            _isRunning = true;
        }

        float resultVelocityX = _rigidbody2D.velocity.x + acceleration;

        if(_isGrounded == true)
        {
            if (Mathf.Abs(resultVelocityX) > _maxSpeed)
            {
                _rigidbody2D.velocity = new Vector2(_maxSpeed * direction.x, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(resultVelocityX, _rigidbody2D.velocity.y);
            }
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(resultVelocityX, _rigidbody2D.velocity.y);
        }
    }

    private void Jump()
    {
        if(_isGrounded == true || _isDoubleJumped == false)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);

            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);

            _animator.SetBool("IsFalling", false);

            if (_isGrounded == true)
            {
                _animator.SetTrigger("Jump");
            }
            else
            {
                _animator.SetTrigger("DoubleJump");
                _isDoubleJumped = true;
            }

            _isGrounded = false;
        }
    }

    private void OnCollisionEntered(Vector2 point) => _point = point;

    private void OnDrawGizmos()
    {
        if (_point != Vector2.zero)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawCube(_point, Vector3.one / 5);
        }

        if (_enemyPoint != Vector2.zero)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawCube(_enemyPoint, Vector3.one / 5);
        }

        //if(_colliderBounds[0] != null && _inited)
        //{
        //    Gizmos.color = Color.red;

        //    for(int i = 0; i < _colliderBounds.Length; i++)
        //    {
        //        Gizmos.DrawCube(_colliderBounds[i], Vector3.one / 5);
        //    }
        //}
    }
}
