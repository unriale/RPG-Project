using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationAngles = 30;
    private void Update()
    {
        transform.Rotate(0, 0, rotationAngles * Time.deltaTime);
    }
}
