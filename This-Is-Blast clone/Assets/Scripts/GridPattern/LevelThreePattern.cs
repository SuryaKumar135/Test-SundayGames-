using UnityEngine;

public class LevelThreePattern : GridPattern
{
    [Header("Need To Colours")]
    [SerializeField] private GameObject _turretTypeObject1;
    [SerializeField] private GameObject _turretTypeObject2;
    [SerializeField] private GameObject _turretTypeObject3;

    private Color _color1;
    private Color _color2;
    private Color _color3;

    public override void InitializeVariables()
    {
        _color1 = _turretTypeObject1.GetComponent<Renderer>().material.color;
        _color2 = _turretTypeObject2.GetComponent<Renderer>().material.color;
        _color3 = _turretTypeObject3.GetComponent<Renderer>().material.color;
    }

    public override void SetGripPattern(int x, int y, int row, int coloum, GameObject box)
    {
        int id;
        Color boxColor;
        if (x < row / 2)  // Left side of the split
        {
            if (y < coloum / 2)  // Top-left quadrant
            {
                id = _turretTypeObject1.GetComponent<Turrets>().GetColourID();
                boxColor = _color1;
            }
            else  // Top-right quadrant
            {
                id = _turretTypeObject3.GetComponent<Turrets>().GetColourID();
                boxColor = _color3;
            }
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
