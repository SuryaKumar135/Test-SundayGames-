using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] List<Turrets> TurretsList = new List<Turrets>();

    [SerializeField] private int turretCount;

    [SerializeField] List<Transform> transformList = new List<Transform>();

    private void OnEnable()
    {
        if(transformList!=null)
        {
            foreach(Transform turretsTransform in transformList)
            {
                foreach (Turrets turrets in TurretsList)
                {
                    turrets.transform.position = turretsTransform.position;
                }
            }
        }
    }
}
