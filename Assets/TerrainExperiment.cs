using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainExperiment : MonoBehaviour
{
    [SerializeField] List<GameObject> terrains = new List<GameObject>();
    List<GameObject> current = new List<GameObject>();
    int index = 0;

    void Start()
    {
        GenerateTerrain(index);
    }

    void GenerateTerrain(int num)
    {
        current.ForEach(c => Destroy(c));

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                current.Add(Instantiate(terrains[index], new Vector3(i, 0, j), Quaternion.identity));
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            index--;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            index++;

        index = terrains.Count <= index ? 0 : index;
        index = index < 0 ? terrains.Count - 1 : index;

        GenerateTerrain(index);
    }

}
