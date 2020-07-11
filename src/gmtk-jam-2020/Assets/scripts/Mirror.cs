using UnityEngine;

public class Mirror : MonoBehaviour, IRaycastCollision
{
    [SerializeField] private int _lives = 5;
    void IRaycastCollision.OnCollision(LightMovement lightMovement)
    {
        _lives -= 1;
        if(_lives == 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
