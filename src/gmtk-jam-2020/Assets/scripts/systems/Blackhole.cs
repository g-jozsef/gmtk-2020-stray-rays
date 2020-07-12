using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [SerializeField] private LightSpawner _spawner;
    [SerializeField] private float _gravity;

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
