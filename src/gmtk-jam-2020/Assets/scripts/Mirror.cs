using DG.Tweening;
using DG.Tweening.Core;
using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Mirror : MonoBehaviour, IRaycastCollision
{
    [SerializeField] private EdgeCollider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _flashColor;
    [SerializeField] private Color _decayColor;
    [SerializeField] private float _flashTime = 0.1f;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _decay = 5f;

    public bool FinishedPlacing { get; set; }
    private float size;
    private Material mat;
    private Color originalColor;

    private void Awake()
    {
        mat = _renderer.material;
        originalColor = mat.GetColor("_Tint");
        size = _renderer.size.x;
    }

    private void Start()
    {
        DOVirtual.DelayedCall(_decay, () =>
        {
            DOTween.To(() => size, x => SetSize(x), 0, 3f);
            originalColor = _decayColor;
            mat.DOColor(_decayColor, "_Tint", _flashTime);
        });
    }

    void IRaycastCollision.OnCollision(LightMovement lightMovement)
    {
        if (FinishedPlacing)
        {
            DOTween.To(() => size, x => SetSize(x), Mathf.Max(size - 0.35f, 0.34999f), 0.1f);

            mat.DOColor(_flashColor, "_Tint", _flashTime).OnComplete(() =>
            {
                mat.DOColor(originalColor, "_Tint", _flashTime);
            });
        }
        _audio.pitch = UnityEngine.Random.Range(1.3f, 1.7f);
        _audio.PlayOneShot(_clips[UnityEngine.Random.Range(0, _clips.Length)]);
    }

    private void Update()
    {
        if (FinishedPlacing && size <= 0.35)
        {
            Destroy(this.gameObject);
        }
    }

    public float SetSize(float x)
    {
        size = x;
        _renderer.size = new Vector2(x, _renderer.size.y);
        _collider.points = new Vector2[] {
            new Vector2(-x / 2, 0),
            new Vector2(+x / 2, 0)
        };
        return size;
    }
}
