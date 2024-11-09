using UnityEngine;
using DG.Tweening; 

public class Ruin : InvertableBehaviour
{
    [Header("Ruin Settings")]
    public float fallDistance = 5f;      
    public float riseDistance = 10f;     
    public float fallDelay = 1f;         
    public float returnDelay = 3f;       

    private Vector3 initialPosition;    
    private Collider platformCollider;
    private bool isMoving = false;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position; 
        platformCollider = GetComponent<Collider>();
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

        Vector3 targetPosition = isInverted ? initialPosition + Vector3.up * riseDistance : initialPosition - Vector3.up * fallDistance;
        Debug.Log(targetPosition);

        transform.DOMove(targetPosition, 1f).SetDelay(fallDelay).OnComplete(() =>
        {
            Debug.Log("u");
            platformCollider.enabled = false; 

           
            transform.DOMove(initialPosition, 1f).SetDelay(returnDelay).OnComplete(() =>
            {
                platformCollider.enabled = true;
                isMoving = false;
            });
        });
    }

    protected override void OnInverted()
    {
        
    }
}


