using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    #region Methods

    #region Private Methods

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Hero>())
        {
            col.GetComponent<Hero>().ApplyDamage(6666);
        }
    }
    #endregion

    #endregion
}
