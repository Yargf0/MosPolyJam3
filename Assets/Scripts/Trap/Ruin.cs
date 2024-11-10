using UnityEngine;
using DG.Tweening; 

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

    private float initialPositionY;

    private Tween tween;

    
    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
        initialPositionY = transform.position.y;

        platformCollider = GetComponent<Collider>();
        if(isInverted)
        {
            MeshPlatform.transform.position = MeshPlatform.transform.position - Vector3.up * fallDistance;
            initialPosition = transform.position;
            MeshPlatform.SetActive(false);
        }
            
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (isMoving)
                return;
            
            MeshPlatform.SetActive(true);

            isMoving = true;
            float endPosY = isInverted ?  initialPositionY : initialPositionY - fallDistance;
            float delay = !isInverted ? fallDelay : 0f;

            tween?.Kill();
            tween = MeshPlatform.transform.
                DOMoveY(endPosY, tweenOptions.Duration).
                SetEase(tweenOptions.Ease).
                SetDelay(delay).
                OnStart(() => platformCollider.enabled = isInverted).
                OnComplete(delegate
                {
                    isMoving = false;
                    MeshPlatform.SetActive(isInverted);
                }).
                OnKill(() => isMoving = false).
                Play();
        }


        //if (other.collider.CompareTag("Player") && !isMoving) 
        //{
        //    TriggerPlatform();
        //}
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            float endPosY = isInverted ? initialPositionY - fallDistance : initialPositionY;

            tween?.Kill();
            tween = MeshPlatform.transform.
                DOMoveY(endPosY, tweenOptions.Duration).
                SetEase(tweenOptions.Ease).
                OnComplete(() => platformCollider.enabled = true).
                Play();

            //if (!isInverted)
            //    return;
            
            //tween = MeshPlatform.transform.DOMove(initialPosition - Vector3.up * fallDistance, 1f).SetDelay(returnDelay).Play().OnComplete(() =>
            //{
            //    isMoving = false;
            //    MeshPlatform.SetActive(false);
            //});
        }
    }

    private void TriggerPlatform()
    {
        isMoving = true;
        if (!isInverted)
        {
            Vector3 targetPosition = initialPosition - Vector3.up * fallDistance;

            tween = transform.DOMove(targetPosition, 1f).SetDelay(fallDelay).Play().OnComplete(() =>
            {
                platformCollider.enabled = false;

                tween = transform.DOMove(initialPosition, 1f).SetDelay(returnDelay).Play().OnComplete(() =>
                {
                    platformCollider.enabled = true;
                    isMoving = false;
                });
            });
        }
        else
        {
            MeshPlatform.SetActive(true);
            Vector3 targetPosition = initialPosition;

            tween = MeshPlatform.transform.DOMove(targetPosition, 1f).SetDelay(0).Play();
        }

    }

    protected override void OnInverted()
    {

    }
}


