using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _objectsCount;
    [SerializeField] private GameObject _prefab;

    private List<Transform> _objects;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _objects = new List<Transform>();

        for(int i = 0; i < _objectsCount; i++)
        {
            GameObject gameObject = Instantiate(_prefab, _transform);
            gameObject.SetActive(false);

            _objects.Add(gameObject.transform);
        }
    }

    public bool TryGetObject(out Transform transform)
    {
        transform = _objects.FirstOrDefault(entity => entity.gameObject.activeInHierarchy == false);
        
        return transform != null;
    }

    public bool TryGetObjects(int count, out List<Transform> result)
    {
        result = _objects.Where(entity => entity.gameObject.activeInHierarchy == false).Take(count).ToList();

        return result.Count != 0;
    }

    private void DisableAllObjects()
    {
        _objects.ForEach(entity => entity.gameObject.SetActive(false));
    }
}
