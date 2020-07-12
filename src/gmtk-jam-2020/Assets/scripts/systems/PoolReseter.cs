using DG.Tweening;
using UnityEngine;

public class PoolReseter : MonoBehaviour, IRaycastCollision
{
    public void OnCollision(LightMovement lightMovement)
    {

        lightMovement.CanMove = false;

        DOVirtual.DelayedCall(2, () =>
        {
            lightMovement.gameObject.SetActive(false);
        });
    }
}
