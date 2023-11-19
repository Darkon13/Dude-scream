using System.Collections.Generic;
using UnityEngine;

public class RoutedEnemySpawner : MonoBehaviour
{
    //[SerializeField] private RouteGenerator _routeGenerator;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SawThrowerSpawner _spawner;

    private IReadOnlyList<IReadOnlyNode> _nodes;
    private List<RoutedEnemy> _routedEnemies;
    private Transform _transform;
    private int _enemyCount;
    private bool _inited = false;

    //private void OnEnable()
    //{
    //    _routeGenerator.NodesInited += Init;
    //}

    //private void OnDisable()
    //{
    //    _routeGenerator.NodesInited -= Init;
    //}

    public void Init(IReadOnlyList<IReadOnlyNode> nodes)
    {
        if(_inited == false)
        {
            _transform = GetComponent<Transform>();
            _routedEnemies = new List<RoutedEnemy>();

            _nodes = nodes;
            _enemyCount = _nodes.Count / 2;
            Debug.Log(_enemyCount);

            SpawnEnemies();
            _spawner.Init(_routedEnemies);

            _inited = true;
        }
    }

    private void SpawnEnemies()
    {
        int nodeIndex = 0;

        for (int i = 0; i < _enemyCount; i++)
        {
            GameObject enemy = Instantiate(_prefab, _transform);
            enemy.transform.position = new Vector2(_nodes[nodeIndex].X, _nodes[nodeIndex].Y);

            if(enemy.TryGetComponent(out RoutedEnemy routedEnemy))
            {
                routedEnemy.Init(_nodes[nodeIndex], Color.HSVToRGB(1f / _enemyCount * i, 1, 1));
                _routedEnemies.Add(routedEnemy);
            }

            nodeIndex += 2;
        }
    }

    private void OnDrawGizmos()
    {
        if(_inited == true)
        {
            Gizmos.color = Color.red;

            foreach(IReadOnlyNode node in _nodes)
            {
                Gizmos.DrawCube(new Vector2(node.X, node.Y), new Vector2(0.2f, 0.2f));
            }
        }
    }
}
