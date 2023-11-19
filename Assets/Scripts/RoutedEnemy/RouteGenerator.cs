using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RouteGenerator : MonoBehaviour
{
    [SerializeField] private int _nodeCount;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private RoutedEnemySpawner _spawner;

    private List<Node> _nodes;
    private Transform _transform;

    //public event UnityAction<IReadOnlyList<IReadOnlyNode>> NodesInited;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _nodes = new List<Node>();

        Debug.Log("RouteGenerator awaked");

        Generate();
    }

    private void Generate() 
    {
        float maxDegree = 360;
        Node prevNode = null;

        for(int i = 0; i < _nodeCount; i++)
        {
            float posX = _transform.position.x + Mathf.Cos((maxDegree / _nodeCount * i) * Mathf.Deg2Rad) * _radius;
            float posY = _transform.position.y + Mathf.Sin((maxDegree / _nodeCount * i) * Mathf.Deg2Rad) * _radius;
            Vector2 position = new Vector2(posX, posY);

            GameObject node = Instantiate(_nodePrefab, position, Quaternion.Euler(0, 0, 0), _transform);
            
            if(node.TryGetComponent(out Node nodeComponent))
            {
                if(prevNode != null)
                {
                    prevNode.NextNode = nodeComponent;
                }

                nodeComponent.X = position.x;
                nodeComponent.Y = position.y;

                _nodes.Add(nodeComponent);
                prevNode = nodeComponent;
            }
        }

        if(prevNode != null)
        {
            prevNode.NextNode = _nodes[0];
        }

        _spawner.Init(_nodes);
        //NodesInited?.Invoke(_nodes);
    }
}
