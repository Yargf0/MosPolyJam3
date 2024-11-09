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
    private Tween tween;
    public GameObject MeshPlatform;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.localPosition; 
        platformCollider = GetComponent<Collider>();
        if(isInverted)
            MeshPlatform.transform.localPosition = MeshPlatform.transform.localPosition - Vector3.up * fallDistance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && !isMoving) 
        {
            TriggerPlatform();
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
            Vector3 targetPosition = initialPosition;

            tween = MeshPlatform.transform.DOMove(targetPosition, 1f).SetDelay(0).Play().OnComplete(() =>
            {
                tween = MeshPlatform.transform.DOMove(initialPosition - Vector3.up * fallDistance, 1f).SetDelay(returnDelay).Play().OnComplete(() =>
                {
                    isMoving = false;
                });
            });
        }

    }

    protected override void OnInverted()
    {

    }
}


