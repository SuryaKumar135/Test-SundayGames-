using UnityEngine;

public class LevelFourPattern : GridPattern
{
    [Header("Need To Colours")]
    [SerializeField] private GameObject _turretTypeObject1;
    [SerializeField] private GameObject _turretTypeObject2;
    [SerializeField] private GameObject _turretTypeObject3;
    [SerializeField] private GameObject _turretTypeObject4;

    private Color _color1, _color2,_color3,_color4;
    public override void InitializeVariables()
    {
        _color1=_turretTypeObject1.GetComponent<Renderer>().material.color;
        _color2=_turretTypeObject2.GetComponent<Renderer>().material.color;
        _color3=_turretTypeObject3.GetComponent<Renderer>().material.color;
        _color4=_turretTypeObject4.GetComponent<Renderer>().material.color;
    }

    public override void SetGripPattern(int x, int y, int row, int coloum, GameObject box)
    {
        int id;
        Color boxColor;

        // Determine which quadrant (4 equal parts)
        if (x < row / 2)  // Top half
        {
            if (y < coloum / 2)  // Top-left quadrant
            {
                id = _turretTypeObject1.GetComponent<Turrets>().GetColourID();
                boxColor = _color1;
            }
            else  // Top-right quadrant
            {
                id = _turretTypeObject2.GetComponent<Turrets>().GetColourID();
                boxColor = _color2;
            }
        }
        else  // Bottom half
        {
            if (y < coloum / 2)  // Bottom-left quadrant
            {
                id = _turretTypeObject3.GetComponent<Turrets>().GetColourID();
                boxColor = _color3;
            }
            else  // Bottom-right quadrant
            {
                id = _turretTypeObject4.GetComponent<Turrets>().GetColourID();
                boxColor = _color4;
            }
        }

        // Assign color and ID to the box
        box.GetComponent<BoxScript>().SetColourId(id);
        box.GetComponent<BoxScript>().SetBoxColour(boxColor);
    }
}
