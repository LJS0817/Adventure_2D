using UnityEngine;

public class MouseIndicator
{

    public MouseIndicator()
    {

    }

    public void Update()
    {

    }

    public Quaternion GetAngle(Vector2 pos)
    {
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, z - 90);
    }
}
