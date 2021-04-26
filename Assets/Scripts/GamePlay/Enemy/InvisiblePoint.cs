using UnityEngine;

public class InvisiblePoint : MonoBehaviour
{

    private bool isActive = false;
    public delegate void PointAbyss();
    public event PointAbyss die;

    #region Methods


    protected void Start()
    {
        EnemyMoveManager.instance.changePoint += SetActive;
        SetActive(EnemyMoveManager.instance.ActivePoint);
    }

    public void Die()
    {
        if(isActive)
        {
            die?.Invoke();
            Destroy(gameObject);
        }
    }

    private void SetActive(InvisiblePoint point)
    {
        if (point == this)
        {
            isActive = true;
        }
    }


    #endregion
}
