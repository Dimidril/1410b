using System.Collections;
using UnityEngine;

public class DamageCircle : Bullet
{
    #region Fields

    #region Private Fields

    private GameObject colGO;
    private float DeltaDamageTime = 1.0f;
    private float _safityTime = 3.0f;
    private IEnumerator _damageCoroutine;
    
    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    protected override void Awake()
    {
        _damageCoroutine = DamageCoroutine();
        StartCoroutine(DestroyTimer());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Hero>())
        {
          colGO = collider.gameObject;
          StartCoroutine(_damageCoroutine);
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
         if (colGO.GetComponent<Hero>())
        {
            StopCoroutine(_damageCoroutine);
        }
    }

    #endregion

    #region Private Methods

   private IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(_safityTime);
        while (true)
        {
            colGO.GetComponent<NonStationaryCombat>().ApplyDamage(_damage);
            yield return new WaitForSeconds(DeltaDamageTime);
        }
    }

    #endregion

    #endregion
}