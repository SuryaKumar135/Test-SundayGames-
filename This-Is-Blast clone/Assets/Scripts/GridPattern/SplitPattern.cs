using DG.Tweening;
using UnityEngine;

public class SplitPattern : GridPattern
{
    [Header("Need To Colours")]
    [SerializeField] private GameObject _turretTypeObject1;
    [SerializeField] private GameObject _turretTypeObject2;

    private Color _color1, _color2;

    public override void InitializeVariables()
    {
        _color1 = _turretTypeObject1.GetComponent<Renderer>().material.color;
        _color2 = _turretTypeObject2.GetComponent<Renderer>().material.color;
    }

    public override void SetGripPattern(int x, int y, int row, int coloum, GameObject box)
    {
        int id;
        Color boxColor;
        if (x < row / 2)  // Left side of the split
        {
            id = _turretTypeObject1.GetComponent<Turrets>().GetColourID();
            boxColor = _color1;
        }
        else // Right side of the split
        {
            id = _turretTypeObject2.GetComponent<Turrets>().GetColourID();
            boxColor = _color2;
        }

        box.GetComponent<BoxScript>().SetColourId(id);
        box.GetComponent<BoxScript>().SetBoxColour(boxColor);
    }
}
