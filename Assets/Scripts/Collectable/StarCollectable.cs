using UnityEngine;

public class StarCollectable : BaseCollectable
{
    public override void Collect()
    {
        Player.AddStar();
        Destroy(gameObject);
    }
}