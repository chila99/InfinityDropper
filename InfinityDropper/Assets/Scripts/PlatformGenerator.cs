using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GeneratePlatform(10, 2, 4, 2, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GeneratePlatform(float size, float minHoleX, float maxHoleX, float minHoleZ, float maxHoleZ)
    {
        // calculate randomly the size of the hole
        float holeX = Random.Range(minHoleX, maxHoleX);
        float holeZ = Random.Range(minHoleZ, maxHoleZ);

        // calculate the position of the hole
        // pick random x position from 0 to size - holeX
        float startingX = Random.Range(0, size - holeX);
        // pick random z position from 0 to size - holeZ
        float startingZ = Random.Range(0, size - holeZ);

        // instantiate for cubes for the platform to cover the space
        // around the hole
        // left
        GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
        left.transform.position = new Vector3(-size / 2 + startingX / 2, 0, 0);
        left.transform.localScale = new Vector3(startingX, 1, size);
        left.name = "left";
        left.transform.parent = gameObject.transform;

        // right
        GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
        right.transform.position = new Vector3(size / 2 - (size - holeX - startingX) / 2, 0, 0);
        right.transform.localScale = new Vector3(size - holeX - startingX, 1, size);
        right.name = "right";
        right.transform.parent = gameObject.transform;

        // top
        GameObject top = GameObject.CreatePrimitive(PrimitiveType.Cube);
        top.transform.position = new Vector3(0, 0, size / 2 - (size - holeZ - startingZ) / 2);
        top.transform.localScale = new Vector3(size, 1, size - holeZ - startingZ);
        top.name = "top";
        top.transform.parent = gameObject.transform;

        // bottom
        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottom.transform.position = new Vector3(0, 0, -size / 2 + startingZ / 2);
        bottom.transform.localScale = new Vector3(size, 1, startingZ);
        bottom.name = "bottom";
        bottom.transform.parent = gameObject.transform;

        // assign random color to the platform
        Color randomColor = Random.ColorHSV();
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = randomColor;
        }
        return gameObject;
    }
}
