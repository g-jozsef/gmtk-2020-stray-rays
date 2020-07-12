using UnityEngine;

public class EscapeListener : MonoBehaviour, IRaycastCollision
{
    [SerializeField] private LongVariable _escapedCount;
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private AudioSource _source;

    public void OnCollision(LightMovement lightMovement)
    {
        _waveManager.Escape();
        _escapedCount.Value += 1;
        _source.Play();
    }
}
