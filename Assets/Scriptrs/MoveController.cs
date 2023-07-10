using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Transform _transform;
    private Animator _animator;
    private float _angleLeft = 180;

    private void Start()
    {
        _transform = gameObject.transform;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Vector3 vectorTo = new Vector3(_speed * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.A))
            {
                MoveTo(-vectorTo, new Quaternion(0, _angleLeft, 0, 0));
            }
            else if(Input.GetKey(KeyCode.D))
            {
                MoveTo(vectorTo, new Quaternion(0, 0, 0, 0));
            }
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    private void MoveTo(Vector3 vectorTo, Quaternion rotationTo)
    {
        _animator.SetFloat("Speed", 1f);

        _transform.rotation = rotationTo;
        _transform.position += vectorTo;
    }
}
