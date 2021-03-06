using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDir : MonoBehaviour
{

    private void Update()
    {
        MouseDir();
    }

    private void MouseDir()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3 (0, 0, angle + 270);
    }
}