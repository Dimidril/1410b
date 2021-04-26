using UnityEngine;

public class Damager : Enemy
{

    #region Fields

    #region Serialized Fields

    [SerializeField] private DamageCircle damageCircle;

    #endregion

    #region Private Fields

    private Transform DamageCircleSpawner;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected override void Attack()
    {
        if (Hero.instance.isDeath) 
        {
            return;
        }
        else if (!Hero.instance.isDeath)
        {
            DamageCircleSpawner = Hero.instance.gameObject.transform;
            Transform circ = Instantiate(damageCircle.transform, DamageCircleSpawner.position, Quaternion.identity);
            circ.GetComponent<DamageCircle>().OnCreate(Damage);
            circ.name = "DamageCircle";
        }
    }

    #endregion

    #endregion
}