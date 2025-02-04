using DG.Tweening;
using UnityEngine;

public class DiagonalPattern : GridPattern
{
    [SerializeField] private GameObject _turretTypeObject1;
    [SerializeField] private GameObject _turretTypeObject2;

    private Color _color1;
    private Color _color2;
    public override void InitializeVariables()
    {
        _color1 = _turretTypeObject1.GetComponent<Renderer>().material.color;
        _color2 = _turretTypeObject2.GetComponent<Renderer>().material.color;
    }

    public override void SetGripPattern(int x, int y, int row, int coloum, GameObject box)
    {
        int id;
        Color boxColor;

        // Create diagonal stripes based on (x + y)
        if ((x + y) % 2 == 0)  // Even diagonals
        {
            id = TurretManager.instance.InactiveTurretsList[0].GetComponent<Turrets>().GetColourID();
            boxColor = _color1;
        }
        else  // Odd diagonals
        {
            id = TurretManager.instance.InactiveTurretsList[1].GetComponent<Turrets>().GetColourID();
            boxColor = _color2;
        }

        box.GetComponent<BoxScript>().SetColourId(id);
        box.GetComponent<BoxScript>().SetBoxColour(boxColor);
    }
}
