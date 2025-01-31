using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public bool isBoxSelected;

    public void selected(float time)
    {
        if(isBoxSelected)
        {
            transform.DOScale(Vector3.zero, time).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
