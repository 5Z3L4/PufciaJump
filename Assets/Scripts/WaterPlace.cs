using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlace : MonoBehaviour
{
    public GameObject waterTile;
    public GameObject parentObject;
    public GameObject rowHolder;
    public int width;
    public int height;

    private int xStart;
    private int yStart = 50;

    // Start is called before the first frame update
    void Start()
    {
        var row = Instantiate(rowHolder, Vector3.zero, Quaternion.identity, parentObject.transform);
        for (int i = 0; i < width; i++)
        {
            Instantiate(waterTile, new Vector3(xStart,0,0), Quaternion.identity, row.transform);
            Instantiate(waterTile, new Vector3(-xStart,0,0), Quaternion.identity, row.transform);
            xStart += 50;
        }
        for (int i = 0; i < height; i++)
        {
            Instantiate(row, new Vector3(row.transform.position.x, 0, yStart), Quaternion.identity, parentObject.transform);
            Instantiate(row, new Vector3(row.transform.position.x, 0, -yStart), Quaternion.identity, parentObject.transform);
            yStart += 50;
        }
    }
}
