using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _platformPartPrefab;
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
        GameObject left = Instantiate(_platformPartPrefab);
        left.transform.parent = gameObject.transform;
        left.transform.localPosition = new Vector3(-size / 2 + startingX / 2, 0, 0);
        left.transform.localScale = new Vector3(startingX, 1, size);
        left.name = "platformLeft";
        left.GetComponent<Renderer>().material.color = randomColor;

        // right
        GameObject right = Instantiate(_platformPartPrefab);
        right.transform.parent = gameObject.transform;
        right.transform.localPosition = new Vector3(size / 2 - (size - holeX - startingX) / 2, 0, 0);
        right.transform.localScale = new Vector3(size - holeX - startingX, 1, size);
        right.name = "platformRight";
        right.GetComponent<Renderer>().material.color = randomColor;

        // top
        GameObject top = Instantiate(_platformPartPrefab);
        top.transform.parent = gameObject.transform;
        top.transform.localPosition = new Vector3(0, 0, size / 2 - (size - holeZ - startingZ) / 2);
        top.transform.localScale = new Vector3(size, 1, size - holeZ - startingZ);
        top.name = "platformTop";
        top.GetComponent<Renderer>().material.color = randomColor;

        // bottom
        GameObject bottom = Instantiate(_platformPartPrefab);
        bottom.transform.parent = gameObject.transform;
        bottom.transform.localPosition = new Vector3(0, 0, -size / 2 + startingZ / 2);
        bottom.transform.localScale = new Vector3(size, 1, startingZ);
        bottom.name = "platformBottom";
        bottom.GetComponent<Renderer>().material.color = randomColor;

        float lightOcclude = 1f;
        // generate the borders
        // left border
        GameObject leftBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftBorder.transform.parent = gameObject.transform;
        leftBorder.transform.localPosition = new Vector3(-size / 2 - 0.5f, borderHeight / 2, 0);
        leftBorder.transform.localScale = new Vector3(1, borderHeight, size + lightOcclude);
        leftBorder.name = "leftBorder";
        leftBorder.GetComponent<Renderer>().material.color = Color.white;

        // right border
        GameObject rightBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightBorder.transform.parent = gameObject.transform;
        rightBorder.transform.localPosition = new Vector3(size / 2 + 0.5f, borderHeight / 2, 0);
        rightBorder.transform.localScale = new Vector3(1, borderHeight, size + lightOcclude);
        rightBorder.name = "rightBorder";
        rightBorder.GetComponent<Renderer>().material.color = Color.white;

        // top border
        GameObject topBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topBorder.transform.parent = gameObject.transform;
        topBorder.transform.localPosition = new Vector3(0, borderHeight / 2, size / 2 + 0.5f);
        topBorder.transform.localScale = new Vector3(size + lightOcclude, borderHeight, 1);
        topBorder.name = "topBorder";
        topBorder.GetComponent<Renderer>().material.color = Color.white;

        // bottom border
        GameObject bottomBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottomBorder.transform.parent = gameObject.transform;
        bottomBorder.transform.localPosition = new Vector3(0, borderHeight / 2, -size / 2 - 0.5f);
        bottomBorder.transform.localScale = new Vector3(size + lightOcclude, borderHeight, 1);
        bottomBorder.name = "bottomBorder";
        bottomBorder.GetComponent<Renderer>().material.color = Color.white;

        return gameObject;
    }
}
