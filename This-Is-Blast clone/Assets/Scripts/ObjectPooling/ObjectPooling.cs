using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject prefabObj;
    public List<GameObject> poolList = new List<GameObject>();
    public int poolCount = 80;

    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < poolCount; i++)
        {
            var GObj = Instantiate(prefabObj, transform.position, Quaternion.identity);
            GObj.transform.parent = transform;
            GObj.SetActive(false);
            poolList.Add(GObj);
        }
    }

    //Place Code
    //public void PlaceObject(Vector3 pos)
    //{
    //    var GObj = poolList[0];
    //    GObj.transform.position = pos;
    //    GObj.SetActive(true);
    //    poolList.RemoveAt(0);
    //}

    //public void PlaceObject(Vector3 pos,Quaternion Rotation)
    //{
    //    var GObj = poolList[0];
    //    GObj.transform.position = pos;
    //    GObj.transform.rotation = Rotation;
    //    GObj.SetActive(true);
    //    poolList.RemoveAt(0);
    //}
    //public GameObject PlaceObjectReturn(Vector3 pos, Quaternion Rotation)
    //{
    //    var GObj = poolList[0];
    //    GObj.transform.position = pos;
    //    GObj.transform.rotation = Rotation;
    //    GObj.SetActive(true);
    //    poolList.RemoveAt(0);
    //    return GObj;
    //}

    //public GameObject ReturnObject(GameObject Gobj)
    //{
    //    return Gobj;
    //}



    ////place back Code
    //public void PlaceBack(GameObject GObj)
    //{
    //    GObj.SetActive(false);
    //    GObj.transform.position = refPos.position;
    //    GObj.transform.parent = null;
    //    poolList.Add(GObj);
    //}




    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                return poolList[i];
            }
        }
        return null;
    }
    
    public void SetPoolObject(GameObject Gobj, Vector3 pos, Quaternion Rotation)
    {
        if(Gobj!=null)
        {
            Gobj.transform.position = pos;
            Gobj.transform.rotation = Rotation;
            Gobj.SetActive(true);
        }

        return;
    }
}
