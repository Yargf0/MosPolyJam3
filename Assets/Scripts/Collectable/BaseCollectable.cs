using UnityEngine;

public abstract class BaseCollectable : InvertableBehaviour2D, ICollectable
{
    protected virtual void Update()
    {
        Vector3 direction = Player.Instance.CameraPosition - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    public abstract void Collect();
}