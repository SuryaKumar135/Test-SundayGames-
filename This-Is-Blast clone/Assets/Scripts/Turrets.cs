using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Turrets : MonoBehaviour
{

    [SerializeField] private int BulletsCounts = 20;

    public TurretShootPoint _turretShootPoint = new TurretShootPoint();

    [SerializeField] private int ColourID;

    bool IsBoxsMoving;
    public GridManager gridManager;


    private void Start()
    {
        gridManager = TurretManager.instance.GridManager;
    }

    public void OnTurreSelect()
    {
        _turretShootPoint = TurretManager.instance.TurretPlacement();
        if(_turretShootPoint != null )
        {
            transform.GetComponent<Collider>().enabled = false;
            transform.DOMove(_turretShootPoint.pos.position, 1f).OnComplete(() =>
            {
                TurretManager.instance.AddTurrets(this);
                StartCoroutine(nameof(BulletShootingCalculation));
            });
            _turretShootPoint.isInAction = true;

        }

    }

    public bool BulletCall(int id)
    {
        return id == ColourID;
    }
    public void DetectBullets()
    {
        if(BulletsCounts>0)
        {
            BulletsCounts-=1;
            _turretShootPoint.bulletCount.text=BulletsCounts.ToString();
        }
    }

    IEnumerator BulletShootingCalculation()
    {
        while (BulletsCounts > 0)
        {
            CheckAndMoveRow();
            yield return null;
        }

        TurretManager.instance.RemoveTurret(this);
        Tween TurrenComplete = transform.DOScale(Vector3.zero, 1);
        yield return TurrenComplete.WaitForCompletion();
        _turretShootPoint.isInAction = false;
        _turretShootPoint.bulletCount.text=string.Empty;
    }

    public int GetColourID()
    {
        return ColourID;
    }


    //
   

    private void CheckAndMoveRow()
    {
        if (IsBoxsMoving) { return; }
        for (int x = 0; x < gridManager.boxArray.GetLength(0); x++)
        {
            // Debug.Log($" Object Names : {boxArray[x, 0]}");
            if (gridManager.boxArray[x, 0].TryGetComponent(out BoxScript script) && !script.isBoxSelected && script.GetColourID() == ColourID)
            {
                //Debug.Log($" Box ID {script.GetColourID()} : ");
                IsBoxsMoving = true;
                script.isBoxSelected = true;
                if (script.isBoxSelected)
                {
                   
                    GameObject bullet = TurretManager.instance.bulletPool.GetPooledObject();  //Bullet Object Pooling
                    bullet.SetActive(true);
                    bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);//Setting Position Adn rotation


                    bullet.transform.DOMove(script.transform.position, .1f).OnComplete(() => {
                        bullet.gameObject.SetActive(false);
                        script.transform.DOScale(Vector3.zero, .2f).OnComplete(() =>
                        {

                            script.Selected(this);
                            StartCoroutine(MoveBoxes(x));

                        });

                    });

                    break;
                   
                }
            }
        }
    }




    private IEnumerator MoveBoxes(int rowIndex)
    {
        
        for (int y = 0; y < gridManager.boxArray.GetLength(1) - 1; y++)
        {
            if (gridManager.boxArray[rowIndex, y] != null && gridManager.boxArray[rowIndex, y + 1] != null)
            {
                if (!gridManager.boxArray[rowIndex, y + 1].activeSelf) continue;

                
                GameObject tempBox = gridManager.boxArray[rowIndex, y];
                gridManager.boxArray[rowIndex, y] = gridManager.boxArray[rowIndex, y + 1];
                gridManager.boxArray[rowIndex, y + 1] = tempBox;

                Vector3 firstPos = gridManager.boxArray[rowIndex, y].transform.position;
                Vector3 secondPos = gridManager.boxArray[rowIndex, y + 1].transform.position;

                Sequence moveSequence = DOTween.Sequence();
                moveSequence.Join(gridManager.boxArray[rowIndex, y].transform.DOMove(secondPos, gridManager.cubeMoveTime))
                            .Join(gridManager.boxArray[rowIndex, y + 1].transform.DOMove(firstPos, gridManager.cubeMoveTime));

                
                yield return moveSequence.WaitForCompletion();
            }
        }

        yield return new WaitForSeconds(0.1f); 
        gridManager.boxArray[rowIndex, gridManager.boxArray.GetLength(1) - 1]=null;
        IsBoxsMoving = false;
    }
}