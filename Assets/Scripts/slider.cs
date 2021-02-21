using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    public void setValue(string type)
    {
        transform.gameObject.GetComponent<Text>().text = (Mathf.Round(transform.GetComponentInParent<Slider>().value * 1000) / 1000).ToString() + type;
    }
}
