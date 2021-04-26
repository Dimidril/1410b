using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Mine : StationaryCombat
{
    #region Serialized Fields

    [SerializeField] private MineExplosion _explosionObject;

    #endregion

    #region Private Fields

    private Enemy _enemy;

    #endregion

    #region Methods

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            _enemy = other.GetComponent<Enemy>();
            Attack();
        }
    }

    #endregion

    #region Private Methods

    protected override void Attack()
    {
        _enemy.ApplyDamage(Damage);
        MineExplosion _currentExplosion = Instantiate(_explosionObject);
        _currentExplosion.name = "MineExplosion";
        _currentExplosion = null;
        Destroy(gameObject);
    }

    #endregion

    #endregion
}