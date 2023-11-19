using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _contacted = false;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //if (_contacted == false)
        //{
        //    MoveLeft();
        //}

        //if(_rigidbody.Cast(Vector2.left, new RaycastHit2D[1], 0.01f) > 0 && _contacted == false)
        //{
        //    Debug.Log("Contact");

        //    _rigidbody.velocity = Vector2.zero;
            
        //    _contacted = true;
        //}
    }

    private void MoveLeft() => Move(Vector2.left);

    private void MoveRight() => Move(Vector2.right);

    private void Move(Vector2 direction)
    {
        _rigidbody.velocity += _speed * direction * Time.deltaTime;
    }
}
