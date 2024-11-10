using UnityEngine;

public class StarCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Player.AddStar();
        Destroy(gameObject);
    }
}