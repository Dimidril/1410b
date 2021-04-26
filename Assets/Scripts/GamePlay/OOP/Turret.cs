using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SphereCollider))]
public class Turret : StationaryCombat
{
    #region Serialized Fields

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _gunObject;
    [SerializeField] private float _destroyTimer = 20f;
    [SerializeField] private TextMeshPro _text;

    #endregion

    #region Private Fields

    private Enemy _currentTarget = null;
    private List<Enemy> _enemyList;

    #endregion

    #region Methods

    #region Unity Methods

    new private void Awake()
    {
        base.Awake();
        _enemyList = new List<Enemy>();
        StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        if (_currentTarget != null)
        {
            _gunObject.LookAt(_currentTarget.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            _enemyList.Add(other.gameObject.GetComponent<Enemy>());
            if(_currentTarget == null)
            {
                _currentTarget = _enemyList[0];
                StartCoroutine(_attackCoroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            _enemyList.Remove(other.gameObject.GetComponent<Enemy>());
            if(_enemyList.Count == 0)
            {
                StopCoroutine(_attackCoroutine);
                _currentTarget = null;
            }
        }
    }

    #endregion

    #region Private Methods

    protected override void Attack()
    {
        GameObject _currentBullet = Instantiate(_bulletPrefab.gameObject, _spawnPoint.position, _spawnPoint.rotation);
        _currentBullet.GetComponent<Rigidbody>().AddForce(_currentBullet.transform.forward * _bulletSpeed);
        _currentBullet.GetComponent<Bullet>().OnCreate(Damage);
        _currentBullet = null;
    }



    protected override IEnumerator AttackCoroutine()
    {
        while (_enemyList.Count > 0)
        {
            if(_currentTarget == null)
            {
                _enemyList.Remove(_enemyList[0]);
                _currentTarget = (_enemyList.Count > 0) ? _enemyList[0] : null;
                if(!_currentTarget) StopCoroutine(_attackCoroutine);
            }
            Attack();
            yield return new WaitForSeconds(DeltaFireTime);
        }
    }

    private IEnumerator DestroyTimer()
    {
        for(int i = (int)_destroyTimer; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Die();
    }

    #endregion

    #endregion
}