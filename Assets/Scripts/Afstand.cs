using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Afstand : MonoBehaviour
{
    private static float distanceOffset = -187.2219f;
    private static float distance = distanceOffset + 201.03f;
    [SerializeField]
    public bool generation = false;
    private float t = 0;
    private float maxTime;

    public float velocity = 1;
    public float acceleration = 1;
    public GameObject cube;
    public GameObject SnelheidGraph;
    public GameObject AfstandGraph;
    public GameObject velocityObj;
    public GameObject accelerationObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generation && (t < maxTime))
        {
            RunFysicsSimulation();
            RunGraphSimulation();
            t += Time.deltaTime;
        }
        else
        {
            generation = false;
        }
    }

    void RunFysicsSimulation()
    {
        float pos = (velocity * t) + (0.5f * acceleration * t * t);
        cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, distanceOffset - pos + 194.8719f);
    }
    void RunGraphSimulation()
    {
        SnelheidGraph.GetComponentInChildren<LineRenderer>().positionCount += 1;
        Vector3 pos = new Vector3(0,0,0);
        pos.x = (t / maxTime) * 140;
        if(acceleration < 0)
        {
            pos.y = ((velocity + (acceleration * t)) / (velocity)) * 65;
        }
        else
        {
            pos.y = ((velocity + (acceleration * t)) / ((acceleration * maxTime)+velocity)) * 65;
        }
        SnelheidGraph.GetComponentInChildren<LineRenderer>().SetPosition(SnelheidGraph.GetComponentInChildren<LineRenderer>().positionCount -1, pos);

        AfstandGraph.GetComponentInChildren<LineRenderer>().positionCount += 1;
        pos = new Vector3(0, 0, 0);
        pos.x = (t / maxTime) * 140;
        pos.y = ((velocity * t) + (0.5f * acceleration * t * t))/distance * 65;
        AfstandGraph.GetComponentInChildren<LineRenderer>().SetPosition(AfstandGraph.GetComponentInChildren<LineRenderer>().positionCount - 1, pos);
    }

    public void G()
    {
        ReloadVars();
        generation = true;
        SnelheidGraph.GetComponentInChildren<LineRenderer>().positionCount = 0;
        AfstandGraph.GetComponentInChildren<LineRenderer>().positionCount = 0;
        t = 0;
        if (acceleration != 0)
        {
            maxTime = ((-velocity + Mathf.Sqrt(velocity * velocity + (2 * acceleration * distance))) / acceleration);
        }
        else
        {
            maxTime = distance / velocity;
        }
    }

    public void ReloadVars()
    {
        if(float.Parse(velocityObj.GetComponent<Text>().text) == 0 && float.Parse(accelerationObj.GetComponent<Text>().text) == 0)
        {
            velocityObj.GetComponent<Text>().text = "1";
            accelerationObj.GetComponent<Text>().text = "1";
        }
        velocity = float.Parse(velocityObj.GetComponent<Text>().text);
        acceleration = float.Parse(accelerationObj.GetComponent<Text>().text);
    }
}
