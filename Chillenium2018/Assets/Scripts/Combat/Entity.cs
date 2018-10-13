using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public enum Element { bass, guitar, horn, neutral };
    public Element type;
    public void changeType(Element newType)
    {
        type = newType;
    }
}
