using System.Collections.Generic;
using UnityEngine;

public class SawThrowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _sawThrower;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private SawThrowerContoller _contoller;
    [SerializeField] private Transform _point;
    [SerializeField] private float _radius;
    //[SerializeField] private float _offset;
    [SerializeField, Range(0, 180)] private float _angle;

    private Transform _transform;
    //private int _count = 3;
    private List<SawThrower> _sawThrowers = new List<SawThrower>();
    private bool _inited = false;

    public void Init(List<RoutedEnemy> targets)
    {
        if(_inited == false)
        {
            _transform = GetComponent<Transform>();
            int count = targets.Count;
            float offset = (180 - _angle) / 2 + (_angle / count) / 2;

            for (int i = 0; i < count; i++)
            {
                float posX = _point.position.x + Mathf.Cos((_angle/ count * i + offset) * Mathf.Deg2Rad) * _radius;
                float posY = _point.position.y + Mathf.Sin((_angle / count * i + offset) * Mathf.Deg2Rad) * _radius;
                Vector3 pos = new Vector2(posX, posY);

                //Debug.Log((_point.position - pos).normalized);
                Quaternion angle = Quaternion.LookRotation(Vector3.forward, (pos - _point.position).normalized);

                GameObject gameObject = Instantiate(_sawThrower, pos, angle, _transform);

                if(gameObject.TryGetComponent(out SawThrower sawThrower))
                {
                    sawThrower.Init(targets[i].transform, targets[i].Color, _pool);
                    _sawThrowers.Add(sawThrower);
                }
            }

            _contoller.Init(_sawThrowers);

            _inited = true;
        }
    }

    private void OnDrawGizmos()
    {
        if(_inited == true)
        {
            Gizmos.color = Color.green;

            foreach(SawThrower sawThrower in _sawThrowers)
            {
                Vector3 sawThrowerPos = sawThrower.transform.position;
                Vector3 pos = sawThrowerPos + (_point.position - sawThrowerPos).normalized;

                Gizmos.DrawCube(pos, new Vector2(0.2f, 0.2f));
            }
        }   
    }
}
