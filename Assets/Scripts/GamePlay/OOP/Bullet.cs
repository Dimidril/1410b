using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Bullet : MonoBehaviour
{
    #region Fields

    #region Serialize Field

    [SerializeField] protected float destroyTimer = 10f;
    [SerializeField] private bool isEnemyBullet = false;

    #endregion

    #region Protected Field

    protected Rigidbody _rig;
    protected float _damage = 0f;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    protected virtual void Awake()
    {
        StartCoroutine(DestroyTimer());
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        GameObject colGO = collision.gameObject;
        if (isEnemyBullet)
        {
            if (colGO.GetComponent<StationaryCombat>() && !colGO.GetComponent<Enemy>())
            {
                colGO.GetComponent<StationaryCombat>().ApplyDamage(_damage);
            }
        }
        else if (colGO.GetComponent<Enemy>())
        {
            colGO.GetComponent<Enemy>().ApplyDamage(_damage);
        }
        DestroyBullet();
    }

    #endregion

    #region Public Methods

    //инициализация урона
    public void OnCreate(float Damage)
    {
        _damage = Damage;
    }

    #endregion

    #region Protected Methods

    //Удаление пули
    protected void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //Время исчезновения
    protected IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTimer);
        DestroyBullet();
    }

    #endregion

    #endregion
}