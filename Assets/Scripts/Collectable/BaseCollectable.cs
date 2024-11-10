using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour, ICollectable
{
    protected virtual void Update()
    {
        Vector3 direction = Player.CameraPosition - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    public abstract void Collect();
}