using System.Collections;
using UnityEngine;

public abstract class StationaryCombat : MonoBehaviour
{
    #region Fields

    #region Serialize Field

    [Header("StationaryCombat")]
    [SerializeField] protected float _startHealth = 10f;
    [SerializeField] protected float Scope;
    [SerializeField] protected float DeltaFireTime = 0.5f;
    [SerializeField] protected float Damage = 0.5f;

    #endregion

    #region Protected Fields

    protected IEnumerator _attackCoroutine;
    protected bool _isFire;
    protected float HP;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    protected void Awake()
    {
        _attackCoroutine = AttackCoroutine();
        _isFire = false;
    }

    protected void Start()
    {
        HP = _startHealth;
    }

    #endregion

    #region Public Methods

    //метод получения урона
    public virtual void ApplyDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
            Die();
        if (HP > _startHealth)
            HP = _startHealth;
    }

    //Гетеры здоровья
    public float GetHP() { return HP; }
    public float GetStartHP() { return _startHealth; }

    #endregion

    #region Protected Methods

    //Атака очередями
    protected virtual IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(DeltaFireTime);
        }
    }

    //Смерть
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    //Атака, реализуеться в наследниках
    protected abstract void Attack();

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Scope);
    }
    #endregion

    #endregion
}