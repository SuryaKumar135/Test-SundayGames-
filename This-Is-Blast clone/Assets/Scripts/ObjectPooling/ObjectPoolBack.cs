using System.Collections;
using UnityEngine;
public class ObjectPoolBack : MonoBehaviour
{
    [SerializeField] private float TimeToPoolBack;
    private void OnEnable()
    {
        StartCoroutine(nameof(PoolBack));
    }
    IEnumerator PoolBack()
    {
        yield return new WaitForSeconds(TimeToPoolBack);
       // transform.parent=null;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    //public float timeToPlaceBack;
    //public bool bulletHole;
    //public bool bulletTrace;
    //public bool bloodTrace;
    //private void OnEnable()
    //{
    //    if (bulletHole)
    //        Invoke(nameof(TurnOffBulletHole), timeToPlaceBack);
    //    if (bulletTrace)
    //        Invoke(nameof(TurnOffTrace), timeToPlaceBack);
    //    if (bloodTrace)
    //        Invoke(nameof(TurnOffBlood), timeToPlaceBack);
    //    else
    //        return;
    //}
    //private void OnDisable()
    //{
    //    //if (bulletHole)
    //    //    TurnOffBulletHole();
    //    //if (bulletTrace)
    //    //    TurnOffTrace();
    //    //if (bloodTrace)
    //    //    TurnOffBlood();
    //    //else
    //    //    return;
    //}
    //private void TurnOffBulletHole()
    //{
    //    //GameManager.instance.bulletHolePool.PlaceBack(gameObject);
    //    gameObject.SetActive(false);
    //}
    //private void TurnOffTrace()
    //{
    //    //GameManager.instance.bulletTracePool.PlaceBack(gameObject);
    //    //GameManager.instance.bulletTracePool.ReturnObject(gameObject).GetComponent<TrailRenderer>().Clear();
    //    gameObject.SetActive(false);
    //    gameObject.GetComponent<TrailRenderer>().Clear();
    //}

    //private void TurnOffBlood()
    //{
    //    //GameManager.instance.bloodEffecPool.PlaceBack(gameObject);
    //    gameObject.SetActive(false);
    //}
}
