using UnityEngine;

public class PoolReseter : MonoBehaviour, IRaycastCollision
{
    public void OnCollision(LightMovement lightMovement)
    {
        lightMovement.gameObject.SetActive(false);
    }
}
