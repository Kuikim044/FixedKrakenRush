using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    public Transform rotationCenter; // วัตถุที่ต้องการให้หมุนรอบ
    public float rotationSpeed = 10f; // ความเร็วการหมุน
    public float movementRadius = 5f; // รัศมีการเคลื่อนที่
    public float movementSpeed = 2f; // ความเร็วการเคลื่อนที่

    private float angle = 0f;

    private void Update()
    {
        // หมุนรอบวัตถุที่กำหนดในแกน Y
        transform.RotateAround(rotationCenter.position, Vector3.back, rotationSpeed * Time.deltaTime);

        // เคลื่อนที่รอบตัววัตถุที่กำหนด
        angle += movementSpeed * Time.deltaTime;
        float x = Mathf.Sin(angle) * movementRadius;
        float y = Mathf.Cos(angle) * movementRadius;
        transform.position = rotationCenter.position + new Vector3(x, y, 0f);
    }
}
