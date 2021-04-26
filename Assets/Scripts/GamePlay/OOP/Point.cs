using System.Collections;
using System;
using UnityEngine;

public class Point : StationaryCombat
{
    #region Fields

    #region Serialized Fields

    //[SerializeField] private short attackRange = 10;
    [SerializeField] private int DefTime = 60;
    [SerializeField] private int RetreatTime = 15;
    [SerializeField] private int Reward = 50;
    [SerializeField] private Transform respawnPoint;

    #endregion

    #region Private Fieldes

    private DateTime timerEnd;
    private bool isActive = false;

    #endregion

    #region Public Fields

    public TimeSpan deltaTime { get; private set; }
    public Transform RespawnPoint { get { return respawnPoint; } }
    public delegate void PointAbyss();
    public event PointAbyss die;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    protected void Awake()
    {
        base.Awake();
        LevelManager.instance.changePoint += SetActive;
        SetActive(LevelManager.instance.ActivePoint);
    }

    /*
    private void Update()
    {
        if (isActive)
        {
            deltaTime = timerEnd - DateTime.Now;
            if (deltaTime.TotalSeconds <= 0)
            {
                AbyssPoint();
            }
        }
    }
    */

    #endregion

    //переопределение плучения урона
    public override void ApplyDamage(float damage)
    {
        if (isActive)
            base.ApplyDamage(damage);
    }

    #region Private Methods

    protected override void Attack()
    {

    }

    //Переопределение смерти
    protected override void Die()
    {
        die?.Invoke();
        base.Die();
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    */

    //Делает точку активной
    private void SetActive(Point point)
    {
        if (point == this)
        {
            isActive = true;
            timerEnd = DateTime.Now.AddSeconds(DefTime);
        }
    }

    #endregion

    #region public Methods

    //гетеры полей

    public float GetRetreatTime()
    {
        return RetreatTime;
    }

    public float GetDefTime()
    {
        return DefTime;
    }

    public int GetReward()
    {
        return Reward;
    }

    //...

    #endregion

    #endregion
}