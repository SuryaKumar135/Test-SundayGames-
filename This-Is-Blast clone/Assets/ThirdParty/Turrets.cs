using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Turrets : MonoBehaviour
{

    [SerializeField] private int BulletsCounts = 20;

    public TurretShootPoint _turretShootPoin = new TurretShootPoint();

    [SerializeField] private int ColourID;

    public void TurretSelected()
    {
        _turretShootPoin = TurretManager.instance.TurretPlacement();

        TurretManager.instance.AddTurrets(this);
        transform.DOMove(_turretShootPoin.pos.position, 1f).OnComplete(() =>
        {
            StartCoroutine(nameof(BulletShooting));
        });
        _turretShootPoin.isInAction = true;
    }

    public bool BulletCall(int id)
    {
        if (BulletsCounts > 0)
        {
            if(id==ColourID)
            {
                DetectBullets();
                return true;
            }
        }
        return false;
    }
    private void DetectBullets()
    {
        --BulletsCounts;
    }

    IEnumerator BulletShooting()
    {
        while (BulletsCounts > 0)
        {

            yield return null;
        }

        TurretManager.instance.RemoveTurret(this);
        Tween TurrenComplete = transform.DOScale(Vector3.zero, 1);
        yield return TurrenComplete.WaitForCompletion();
        _turretShootPoin.isInAction = false;
    }

    public int GetColourID()
    {
        return ColourID;
    }
}