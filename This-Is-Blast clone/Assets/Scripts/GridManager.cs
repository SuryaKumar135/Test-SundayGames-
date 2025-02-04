using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int row;
    [SerializeField] private int column;

    [Space]
    [Header("Box Move Timing")]

    public float cubeMoveTime = .1f;
    public float scaleTime = .1f;

    [Space]
    [Header("Bullet Move Timing")]
    public float bulletMoveTime = .1f;

    public GameObject[,] boxArray;

    [Space]
    [Header("Grid Spacing Offset")]
    [SerializeField] private Vector2 boxPositioningOffset;


    public int noOfCubes;

    public int bulletCount=20;
    public int cubesDestroyed;

    //Color color1 = Color.red;  // First color
    //Color color2 = Color.blue; // Second color

    private GridPattern _gridPattern;

    private void Start()
    {
        //color1 = TurretManager.instance.InactiveTurretsList[0].GetComponent<Renderer>().material.color;
        //color2 = TurretManager.instance.InactiveTurretsList[1].GetComponent<Renderer>().material.color;

        //grid manager initializer

        _gridPattern=GetComponentInChildren<GridPattern>();
        _gridPattern.InitializeVariables();


        GenerateLevel();
        Debug.Log($"Number Of childs {transform.childCount}");
        
    }


    public void GenerateLevel()
    {
        boxArray = new GameObject[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject box = Instantiate(boxPrefab, transform);
                Vector3 position = new Vector3(i * boxPositioningOffset.x, 0, j * boxPositioningOffset.y);
                box.name = $"Row {i} : Column {j}";
                box.transform.SetPositionAndRotation(position, Quaternion.identity);
                boxArray[i, j] = box;
                noOfCubes++;

                //if (i < row / 2)  // Left side of the split
                //{
                //    int id= TurretManager.instance.InactiveTurretsList[0].GetComponent<Turrets>().GetColourID();
                //    box.GetComponent<BoxScript>().SetColourId(id);
                //    box.GetComponent<BoxScript>().SetBoxColour(color1);
                //}
                //else // Right side of the split
                //{
                //    int id = TurretManager.instance.InactiveTurretsList[1].GetComponent<Turrets>().GetColourID();
                //    box.GetComponent<BoxScript>().SetColourId(id);
                //    box.GetComponent<BoxScript>().SetBoxColour(color2);
                //}
                _gridPattern.SetGripPattern(i,j,row,column,box);
            }
        }
    }

   

    private void Update()
    {
       // CheckAndMoveRow();
        InptDetectMouse();
        if (noOfCubes<=0)
        {
            UIManager.Instance.ShowLevelCompletion();
        }
    }
    private void InptDetectMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out RaycastHit hit) && hit.collider != null)
        {
           // Debug.Log($"Object Name{hit.collider.gameObject}");
            if (hit.collider.TryGetComponent(out Turrets turret))
            {
                turret.OnTurreSelect();
            }
        }
    }

    #region Grid Types


    #endregion

    //Not Using Called In Turret 

    /// <summary>
    /// Checks if a row needs movement and triggers the movement coroutine.
    /// </summary>
    //Tween scaleTween = null;
    //private void CheckAndMoveRow()
    //{
    //    if(isMoving) { return; }
    //    for (int x = 0; x < boxArray.GetLength(0); x++)
    //    {
    //       // Debug.Log($" Object Names : {boxArray[x, 0]}");
    //        if (boxArray[x, 0].TryGetComponent(out BoxScript script))
    //        {
    //            Debug.Log($" Box ID {script.GetColourID()} : ");
    //            script.isBoxSelected = TurretManager.instance.ReturnActiveTurret(script.GetColourID());
    //            if (script.isBoxSelected)
    //            {
    //                script.Selected(scaleTime);
    //                StartCoroutine(MoveBoxes(x));
    //            }
    //        }
    //    }
    //}




    //private IEnumerator MoveBoxes(int rowIndex)
    //{
    //    for (int y = 0; y < boxArray.GetLength(1) - 1; y++)
    //    {
    //        if (boxArray[rowIndex, y + 1] != null && boxArray[rowIndex, y + 1].activeSelf)
    //        {
    //            // Swap references first to prevent wrong positions in the next iteration
    //            GameObject tempBox = boxArray[rowIndex, y];
    //            boxArray[rowIndex, y] = boxArray[rowIndex, y + 1];
    //            boxArray[rowIndex, y + 1] = tempBox;

    //            // Store positions before moving
    //            Vector3 firstPos = boxArray[rowIndex, y].transform.position;
    //            Vector3 secondPos = boxArray[rowIndex, y + 1].transform.position;

    //            // Move both boxes with DOTween and wait for completion
    //            Tween moveFirst = boxArray[rowIndex, y].transform.DOMove(secondPos, cubeMoveTime);
    //            Tween moveSecond = boxArray[rowIndex, y + 1].transform.DOMove(firstPos, cubeMoveTime);

    //            isMoving = true;
    //            yield return moveFirst.WaitForCompletion();
    //            yield return moveSecond.WaitForCompletion();
    //            isMoving = false;
    //        }
    //    }
    //}

}