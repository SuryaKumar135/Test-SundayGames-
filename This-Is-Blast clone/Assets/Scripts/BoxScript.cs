using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public bool isBoxSelected;
    [SerializeField] private int BoxColourID = -1;

    public void Selected(Turrets turrets)
    {
        if (isBoxSelected)
        {
            turrets.DetectBullets();
            TurretManager.instance.GridManager.noOfCubes--;
            gameObject.SetActive(false);
        }
    }

    public int GetColourID()
    {
        return BoxColourID;
    }

    public void SetColourId(int colourID)
    {
        BoxColourID = colourID;
    }

    public void SetBoxColour(Color colour)
    {
        transform.GetComponent<Renderer>().material.color = colour;
    }
}
