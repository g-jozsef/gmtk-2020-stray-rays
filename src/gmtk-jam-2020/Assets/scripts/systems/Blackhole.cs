using DG.Tweening;
using UnityEngine;

public class Blackhole : MonoBehaviour, IRaycastCollision
{
    [SerializeField] private LightSpawner _spawner;
    [SerializeField] private float _gravity;
    [SerializeField] private LongVariable _capturedCurrent;


    public void OnCollision(LightMovement lightMovement)
    {
        Debug.Log("Light trapped!");
        _capturedCurrent.Value += 1;
        lightMovement.CanMove = false;
        DOVirtual.DelayedCall(2, () =>
        {
            lightMovement.gameObject.SetActive(false);
        });
    }


    private void Update()
    {
        _spawner.Pool.ForEach(x =>
        {
            Vector3 vect = x.Direction * x.Speed;
            float gravMagnitude = _gravity / Mathf.Pow(Vector2.Distance(x.transform.position, Vector2.zero), 2) * Time.deltaTime;
            Vector3 grav = (Vector2.zero - new Vector2(x.transform.position.x, x.transform.position.y)) * gravMagnitude;
            var res = vect + grav;

            x.Direction = res.normalized;
            x.Speed = res.magnitude;
        });
    }
}
