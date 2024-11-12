using DG.Tweening;
using UnityEngine;

public class Ruin : InvertableBehaviour
{
    [Header("Ruin Settings")]
    public float fallDistance = 5f;
    public float fallDelay = 1f;
    public float returnDelay = 3f;

    private Vector3 initialPosition;
    private Collider platformCollider;
    private bool isMoving = false;
    public GameObject MeshPlatform;

    [SerializeField] private TweenOptions tweenOptions = new(1f, Ease.OutCubic);
    [SerializeField] private AudioClip fallAudio;

    private float initialPositionY;

    private Tween tween;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
        initialPositionY = transform.position.y;

        platformCollider = GetComponent<Collider>();
        if (isInverted)
        {
            MeshPlatform.transform.position = MeshPlatform.transform.position - Vector3.up * fallDistance;
            initialPosition = transform.position;
            MeshPlatform.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && !isMoving)
        {
            StartFalling();
        }
    }

    private void StartFalling()
    {
        isMoving = true;
        MeshPlatform.SetActive(true);

        float endPosY = isInverted ? initialPositionY : initialPositionY - fallDistance;
        float delay = !isInverted ? fallDelay : 0f;

        tween?.Kill();
        tween = MeshPlatform.transform.
            DOMoveY(endPosY, tweenOptions.Duration).
            SetEase(tweenOptions.Ease).
            SetDelay(delay).
            OnStart(() =>
            {
                platformCollider.enabled = isInverted;
                if (!isInverted) AudioManager.Instance.PlaySound(fallAudio, Random.Range(0.9f, 1.1f));
            }).
            OnComplete(() =>
            {
                isMoving = false;
                Invoke(nameof(StartReturning), returnDelay); // Запускаем таймер для возврата
            }).
            Play();
    }

    private void StartReturning()
    {
        if (isMoving) return; // Если платформа уже движется, отменяем возврат

        float endPosY = isInverted ? initialPositionY - fallDistance : initialPositionY;

        tween?.Kill();
        tween = MeshPlatform.transform.
            DOMoveY(endPosY, tweenOptions.Duration).
            SetEase(tweenOptions.Ease).
            OnComplete(() =>
            {
                platformCollider.enabled = true;
                MeshPlatform.SetActive(!isInverted);
            }).
            Play();
    }

    private void OnCollisionExit(Collision other)
    {
        // Убираем возврат, чтобы платформа не реагировала на выход игрока
    }

    protected override void OnInverted()
    {
        // Ваша логика для инвертирования платформы
    }
}



