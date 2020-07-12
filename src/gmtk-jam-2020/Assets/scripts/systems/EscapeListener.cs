using UnityEngine;

public class EscapeListener : MonoBehaviour, IRaycastCollision
{
    [SerializeField] private LongVariable _escapedCount;
    public void OnCollision(LightMovement lightMovement)
    {

        _escapedCount.Value += 1;
    }
}
