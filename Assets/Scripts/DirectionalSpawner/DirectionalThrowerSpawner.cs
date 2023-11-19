using System.Collections.Generic;
using UnityEngine;

public class DirectionalThrowerSpawner : MonoBehaviour
{
    [SerializeField] private int _throwerCount;
    [SerializeField] private float _offsetX;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private DirectionalThrowerController _controller;
    [SerializeField] private ObjectPool _pool;

    private Transform _transform;
    private List<DirectionalThrower> _throwers;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _throwers = new List<DirectionalThrower>();

        for (int i = 0; i < _throwerCount; i++)
        {
            Vector2 pos = (Vector2)_transform.position + new Vector2(_offsetX * i, 0);
            GameObject gameObject = Instantiate(_prefab, pos, new Quaternion(), _transform);

            if(gameObject.TryGetComponent(out DirectionalThrower thrower))
            {
                thrower.Init(_pool);
                _throwers.Add(thrower); 
            }
        }

        _controller.Init(_throwers);
    }
}
