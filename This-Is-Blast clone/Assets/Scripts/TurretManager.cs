using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public List<Turrets> InactiveTurretsList = new List<Turrets>();
    [SerializeField] List<Turrets> ActiveTurretList = new List<Turrets>();

    [SerializeField] public GridManager GridManager;


    public static TurretManager instance;


    [Space]

    [SerializeField] private List<TurretShootPoint> turretShootPoints = new List<TurretShootPoint>();

    public ObjectPooling bulletPool;


    private void Awake()
    {
        if(instance == null) { 
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void OnEnable()
    {
        bulletPool=GameObject.FindAnyObjectByType<ObjectPooling>();
    }

    public bool ReturnActiveTurret(int id)
    {
        foreach (var item in ActiveTurretList)
        {
            return item.BulletCall(id);
        }
        return false;
    }

    public void AddTurrets(Turrets addTurret)
    {
        if(!ActiveTurretList.Contains(addTurret))
        {
            ActiveTurretList.Add(addTurret);
        }
    }

    public void RemoveTurret(Turrets removeTurret)
    {
        if (ActiveTurretList.Contains(removeTurret))
        {
            ActiveTurretList.Remove(removeTurret);
        }
    }

    public TurretShootPoint TurretPlacement()
    {
        foreach(TurretShootPoint pTurret in turretShootPoints)
        {
            if(pTurret.isInAction==false)
            {

                return pTurret;
            }      
        }
        return default;
    }
   

    
}
[System.Serializable]
public class TurretShootPoint
{
    public Transform pos;
    public bool isInAction;
    public TextMeshProUGUI bulletCount;
}
