using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    private float damage;

    private Rigidbody rb;

    private CountdownTimer destroyTimer;

    public void Init(float damage, Vector3 moveForce)
    {
        this.damage = damage;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.AddForce(moveForce, ForceMode.Impulse);
        rb.AddTorque(0.1f * Vector3.forward);

        destroyTimer = new(value: 5f, play: true);
        destroyTimer.Play();
        destroyTimer.OnFinished(() => Destroy(gameObject));
    }

    private void Update()
    {
        if (rb.linearVelocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log("Damage: " + collision.gameObject.name);
            damagable.Damage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log("Damage: " + other.name);
            damagable.Damage(damage);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //destroyTimer.Reset();
        //TimerSystem.Instance.Remove(destroyTimer, TimerUpdateType.Update);
    }
}