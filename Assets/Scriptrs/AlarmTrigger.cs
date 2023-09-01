using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CompositeCollider2D))]
public class AlarmTrigger : MonoBehaviour
{
    [HideInInspector] public event UnityAction OnEnter;
    [HideInInspector] public event UnityAction OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit?.Invoke();
    }
}
