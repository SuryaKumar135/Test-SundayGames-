using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public bool isBoxSelected;

    [SerializeField]
    private int BoxColourID=-1;


    public void selected(float time)
    {
        if(isBoxSelected)
        {
          // TurretManager.instance .GridManager.bulletCount--;
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
