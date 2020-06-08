using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * 1000 * Time.deltaTime);
    }
}
