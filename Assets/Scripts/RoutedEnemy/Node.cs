using UnityEngine;

public class Node : MonoBehaviour, IReadOnlyNode
{
    public IReadOnlyNode NextNode { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}
