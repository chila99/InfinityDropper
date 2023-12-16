using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject GeneratePlatform(float size, float minHoleX, float maxHoleX, float minHoleZ, float maxHoleZ, float borderHeight)
    {
        // calculate randomly the size of the hole
        float holeX = Random.Range(minHoleX, maxHoleX);
        float holeZ = Random.Range(minHoleZ, maxHoleZ);

        // calculate the position of the hole
        // pick random x position from 0 to size - holeX
        float startingX = Random.Range(0, size - holeX);
        // pick random z position from 0 to size - holeZ
        float startingZ = Random.Range(0, size - holeZ);

        Color randomColor = Random.ColorHSV();
        // instantiate for cubes for the platform to cover the space
        // around the hole
        // left
        GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
        left.transform.parent = gameObject.transform;
        left.transform.localPosition = new Vector3(-size / 2 + startingX / 2, 0, 0);
        left.transform.localScale = new Vector3(startingX, 1, size);
        left.AddComponent<PlayerDestroyer>();
        left.name = "platformLeft";
        left.GetComponent<Renderer>().material.color = randomColor;

        // right
        GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
        right.transform.parent = gameObject.transform;
        right.transform.localPosition = new Vector3(size / 2 - (size - holeX - startingX) / 2, 0, 0);
        right.transform.localScale = new Vector3(size - holeX - startingX, 1, size);
        right.AddComponent<PlayerDestroyer>();
        right.name = "platformRight";
        right.GetComponent<Renderer>().material.color = randomColor;

        // top
        GameObject top = GameObject.CreatePrimitive(PrimitiveType.Cube);
        top.transform.parent = gameObject.transform;
        top.transform.localPosition = new Vector3(0, 0, size / 2 - (size - holeZ - startingZ) / 2);
        top.transform.localScale = new Vector3(size, 1, size - holeZ - startingZ);
        top.AddComponent<PlayerDestroyer>();
        top.name = "platformTop";
        top.GetComponent<Renderer>().material.color = randomColor;

        // bottom
        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottom.transform.parent = gameObject.transform;
        bottom.transform.localPosition = new Vector3(0, 0, -size / 2 + startingZ / 2);
        bottom.transform.localScale = new Vector3(size, 1, startingZ);
        bottom.AddComponent<PlayerDestroyer>();
        bottom.name = "platformBottom";
        bottom.GetComponent<Renderer>().material.color = randomColor;

        // generate the borders
        // left border
        GameObject leftBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftBorder.transform.parent = gameObject.transform;
        leftBorder.transform.localPosition = new Vector3(-size / 2 - 0.5f, borderHeight / 2, 0);
        leftBorder.transform.localScale = new Vector3(1, borderHeight, size);
        leftBorder.name = "leftBorder";

        // right border
        GameObject rightBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightBorder.transform.parent = gameObject.transform;
        rightBorder.transform.localPosition = new Vector3(size / 2 + 0.5f, borderHeight / 2, 0);
        rightBorder.transform.localScale = new Vector3(1, borderHeight, size);
        rightBorder.name = "rightBorder";

        // top border
        GameObject topBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topBorder.transform.parent = gameObject.transform;
        topBorder.transform.localPosition = new Vector3(0, borderHeight / 2, size / 2 + 0.5f);
        topBorder.transform.localScale = new Vector3(size, borderHeight, 1);
        topBorder.name = "topBorder";

        // bottom border
        GameObject bottomBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottomBorder.transform.parent = gameObject.transform;
        bottomBorder.transform.localPosition = new Vector3(0, borderHeight / 2, -size / 2 - 0.5f);
        bottomBorder.transform.localScale = new Vector3(size, borderHeight, 1);
        bottomBorder.name = "bottomBorder";

        return gameObject;
    }
}
