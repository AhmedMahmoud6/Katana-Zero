using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Camera MainCam;
    Vector3 mousePos;
    public SpriteRenderer sr;
    public SpriteRenderer sr2;
    bool Attack = PlayerMovement.attack;
    float rotZ;
    void Update()
    {
        mousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        if (Input.GetMouseButtonDown(0) && Attack == false)
        {
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if(Mathf.Abs(rotZ) > 90)
        {
            sr.flipY = true;
        }
        if (Mathf.Abs(rotZ) < 90)
        {
            sr.flipY = false;
        }
        if (PlayerMovement.isFacingRight)
        {
            sr2.flipX = false;
        }
        if (!PlayerMovement.isFacingRight)
        {
            sr2.flipX = true;
        }
    }
}
