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
            if (gridManager.boxArray[x, 0].TryGetComponent(out BoxScript script))
            {
                Debug.Log($" Box ID {script.GetColourID()} : ");
                script.isBoxSelected = BulletCall(script.GetColourID());
                if (script.isBoxSelected)
                {
                    script.Selected();
                    StartCoroutine(MoveBoxes(x));
                }
            }
        }
    }




    private IEnumerator MoveBoxes(int rowIndex)
    {
        for (int y = 0; y < gridManager.boxArray.GetLength(1) - 1; y++)
        {
            if (gridManager.boxArray[rowIndex, y + 1] != null && gridManager.boxArray[rowIndex, y + 1].activeSelf)
            {
                // Swap references first to prevent wrong positions in the next iteration
                GameObject tempBox = gridManager.boxArray[rowIndex, y];
                gridManager.boxArray[rowIndex, y] = gridManager.boxArray[rowIndex, y + 1];
                gridManager.boxArray[rowIndex, y + 1] = tempBox;

                // Store positions before moving
                Vector3 firstPos = gridManager.boxArray[rowIndex, y].transform.position;
                Vector3 secondPos = gridManager.boxArray[rowIndex, y + 1].transform.position;

                // Move both boxes with DOTween and wait for completion
                Tween moveFirst = gridManager.boxArray[rowIndex, y].transform.DOMove(secondPos, gridManager.cubeMoveTime);
                Tween moveSecond = gridManager.boxArray[rowIndex, y + 1].transform.DOMove(firstPos, gridManager.cubeMoveTime);

                IsBoxsMoving = true;
                yield return moveFirst.WaitForCompletion();
                yield return moveSecond.WaitForCompletion();
                IsBoxsMoving = false;
            }
        }
    }
}