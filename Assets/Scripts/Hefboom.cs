using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hefboom : MonoBehaviour
{
    private hefboomKracht l = new hefboomKracht();
    private hefboomKracht r = new hefboomKracht();

    public GameObject leftArrow;
    public GameObject rightArrow;

    public GameObject LF;
    public GameObject LD;
    public GameObject RF;
    public GameObject RD;

    public GameObject hefboom;

    public bool balanced = true;

    void Start()
    {
        l.distance = 5f;
        r.distance = 5f;
    }

    void Update()
    {
        checkVar();
        scale();
        rotation();
    }

    void checkVar()
    {
        if(l.force > 5)
        {
            l.force = 5;
        }
        if (r.force > 5)
        {
            r.force = 5;
        }
        if (l.distance > 5)
        {
            l.distance = 5;
        }
        if (r.distance > 5)
        {
            r.distance = 5;
        }
        if (l.force < 1)
        {
            l.force = 1;
        }
        if (r.force < 1)
        {
            r.force = 1;
        }
        if (l.distance < 1)
        {
            l.distance = 1;
        }
        if (r.distance < 1)
        {
            r.distance = 1;
        }
    }

    void scale()
    {
        l.scale = 1 + (l.force / 5);
        r.scale = 1 + (r.force / 5);
        leftArrow.transform.localScale = new Vector3(l.scale, l.scale, l.scale);
        rightArrow.transform.localScale = new Vector3(r.scale, r.scale, r.scale);
        l.torque = l.force * l.distance;
        r.torque = r.force * r.distance;
    }

    public void change(int index)
    {
        if (balanced)
        {
            switch (index)
            {
                case 0:
                    l.force = LF.GetComponent<Slider>().value;
                    l.distance = l.torque / l.force;
                    LD.GetComponent<Slider>().value = l.distance;
                    break;
                case 1:
                    r.force = RF.GetComponent<Slider>().value;
                    r.distance = r.torque / r.force;
                    RD.GetComponent<Slider>().value = r.distance;
                    break;
                case 2:
                    l.distance = LD.GetComponent<Slider>().value;
                    l.force = l.torque / l.distance;
                    LF.GetComponent<Slider>().value = l.force;
                    break;
                case 3:
                    r.distance = RD.GetComponent<Slider>().value;
                    r.force = r.torque / r.distance;
                    RF.GetComponent<Slider>().value = r.force;
                    break;
            }
        }
        else
        {
            l.force = LF.GetComponent<Slider>().value;
            r.force = RF.GetComponent<Slider>().value;
            l.distance = LD.GetComponent<Slider>().value;
            r.distance = RD.GetComponent<Slider>().value;
        }
    }
    public void balance()
    {
        balanced = !balanced;
    }
    public void rotation()
    {
        if (Mathf.Abs((l.torque*1000)-(r.torque*1000)) > 2)
        {
            if (Mathf.Floor(l.torque * 1000) < Mathf.Floor(r.torque * 1000))
            {
                hefboom.transform.localEulerAngles = new Vector3(0, 16, 0);
                rightArrow.transform.position = new Vector3(0.6095433f, 2.383646f - (Mathf.Tan(0.2792526803f) * r.distance), -1 * r.distance);
                leftArrow.transform.position = new Vector3(0.6095433f, 2.383646f + (Mathf.Tan(0.2792526803f) * l.distance), l.distance);
            }
            else if (Mathf.Floor(l.torque * 1000) > Mathf.Floor(r.torque * 1000))
            {
                hefboom.transform.localEulerAngles = new Vector3(0, -16, 0);
                rightArrow.transform.position = new Vector3(0.6095433f, 2.383646f + (Mathf.Tan(0.2792526803f) * r.distance), -1 * r.distance);
                leftArrow.transform.position = new Vector3(0.6095433f, 2.383646f - (Mathf.Tan(0.2792526803f) * l.distance), l.distance);
            }
        }
        else
        {
            hefboom.transform.localEulerAngles = new Vector3(0, 0, 0);
            leftArrow.transform.position = new Vector3(0.6095433f, 2.383646f, l.distance);
            rightArrow.transform.position = new Vector3(0.6095433f, 2.383646f, -1 * r.distance);
        }
    }
}
