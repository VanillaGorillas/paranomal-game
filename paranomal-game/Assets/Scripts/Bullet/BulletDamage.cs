using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField]
    private float damage;


    public void BulletDamageReduction(float bulletRange)
    {
        if (bulletRange > damage)
        {
            damage -= bulletRange / damage;
        }
        else
        {
            damage -= bulletRange / 10;
        }

        damage = damage < 0 ? 0f : damage;

        //TODO: will need enemies at different distances to test properly. Will be done later down the line
    }

    public void BulletDamageOnImpact(int collisionCount)
    {
        if (collisionCount == 2)
        {
            //Debug.Log(damage / 3);
        }
        else if (collisionCount == 1)
        {
            //Debug.Log(damage / 2); 
        }
        else if (collisionCount == 0)
        {
            //Debug.Log(damage);
        }
    }

}
