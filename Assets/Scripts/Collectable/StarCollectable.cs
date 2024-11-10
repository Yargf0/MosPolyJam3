using UnityEngine;

public class StarCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Player.Instance.AddStar();
        Destroy(gameObject);
    }
}