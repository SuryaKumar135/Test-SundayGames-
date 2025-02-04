using UnityEngine;
abstract public class GridPattern : MonoBehaviour
{
    abstract public void SetGripPattern(int x, int y,int row,int coloum,GameObject box);

    abstract public void InitializeVariables();
}
