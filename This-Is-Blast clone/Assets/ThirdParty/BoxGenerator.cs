using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int row;
    [SerializeField] private int column;

    [SerializeField] private float cubeMoveTime=.2f;
    [SerializeField] private float scaleTime=.2f;

    public GameObject[,] boxArray;

    [SerializeField] private Vector2 boxPositioningOffset;

    private void OnEnable()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        boxArray = new GameObject[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject box = Instantiate(boxPrefab,transform);
                Vector3 position = new Vector3(i * boxPositioningOffset.x, 0, j * boxPositioningOffset.y);
                box.name = $"Row {i} : Column {j}";
                box.transform.SetPositionAndRotation(position, Quaternion.identity);
                boxArray[i, j] = box;
            }
        }
    }

    private void Update()
    {
        CheckAndMoveRow();
    }

    /// <summary>
    /// Checks if a row needs movement and triggers the movement coroutine.
    /// </summary>
    Tween scaleTween = null;
    private void CheckAndMoveRow()
    {
        for (int x = 0; x < boxArray.GetLength(0); x++)
        {
            if (boxArray[x, 0] != null && boxArray[x, 0].TryGetComponent(out BoxScript script) && script.isBoxSelected==true)
            {
                script.selected(scaleTime);
                StartCoroutine(MoveBoxes(x));
            }
        }
    }

    /// <summary>
    /// Moves boxes in a row sequentially.
    /// </summary>
    private IEnumerator MoveBoxes(int rowIndex)
    {
        for (int y = 0; y < boxArray.GetLength(1) - 1; y++)
        {
            if (boxArray[rowIndex, y + 1] != null && boxArray[rowIndex, y + 1].activeSelf)
            {
                // Swap references first to prevent wrong positions in the next iteration
                GameObject tempBox = boxArray[rowIndex, y];
                boxArray[rowIndex, y] = boxArray[rowIndex, y + 1];
                boxArray[rowIndex, y + 1] = tempBox;

                // Store positions before moving
                Vector3 firstPos = boxArray[rowIndex, y].transform.position;
                Vector3 secondPos = boxArray[rowIndex, y + 1].transform.position;

                // Move both boxes with DOTween and wait for completion
                Tween moveFirst = boxArray[rowIndex, y].transform.DOMove(secondPos, cubeMoveTime);
                Tween moveSecond = boxArray[rowIndex, y + 1].transform.DOMove(firstPos, cubeMoveTime);

                yield return moveFirst.WaitForCompletion();
                yield return moveSecond.WaitForCompletion();
            }
        }
    }


}