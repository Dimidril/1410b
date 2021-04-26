using System.Collections;
 using UnityEngine;
 
 public class HealCircle : Bullet
 {
     #region Fields
 
     #region Private Fields
     private GameObject colGO;
     private float DeltaHealTime = 3.0f;
     private float _safityTime = 1.0f;
     private IEnumerator _healCoroutine;

     #endregion
 
     #endregion
 
     #region Methods
 
     #region Unity Methods
 
     protected override void Awake()
     {
         _healCoroutine = HealCoroutine();
     } 
 
    private void OnTriggerEnter(Collider collider)
     {
 
         if (collider.GetComponent<Enemy>() && !colGO.GetComponent<NonStationaryCombat>() )
         {
             StartCoroutine(_healCoroutine);
         }
     }
 
     private void OnTriggerExit(Collider collider)
     {
         StopCoroutine(_healCoroutine);
     }

  
 
     #endregion
 
     #region Private Methods

     private IEnumerator HealCoroutine()
     {
        // yield return new WaitForSeconds(_safityTime);
         while (true)
         {
             colGO.GetComponent<Enemy>().ApplyDamage(_damage);
             yield return new WaitForSeconds(DeltaHealTime);
         }
     }
 
     #endregion
 
     #endregion
 }