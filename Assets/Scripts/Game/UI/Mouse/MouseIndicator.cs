using UnityEngine;

public class MouseIndicator
{

    public MouseIndicator()
    {

    }

    public void Update()
    {

    }

    Quaternion getAngle(Vector2 pos)
    {
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, z - 90);
    }

    public Quaternion LerpRotation(Vector2 pos, Quaternion rot)
    {
        return Quaternion.Lerp(rot, getAngle(pos), 0.2f);
    }
}
