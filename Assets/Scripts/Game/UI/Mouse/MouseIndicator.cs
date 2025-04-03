using UnityEngine;

public class MouseIndicator
{
    const float _raycastDist = 7.5f;
    Transform _marker;
    Vector2 _up;

    public MouseIndicator(Transform m)
    {
        _marker = m;
        _up = Vector2.zero;
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

    public Vector2 GetUpVector() { return _up; }

    public void SetMarker(Vector2 pos, Vector2 dir, Quaternion rot)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, _raycastDist, ~(1 << 2));
        Debug.DrawLine(pos, pos + dir * _raycastDist, Color.red);
        if (hit)
        {
            if (hit.collider != null)
            {
                _marker.position = hit.point;
                _marker.up = _up = hit.normal;
            }
        }
        else
        {
            _marker.position = pos + dir * _raycastDist;
            if (_marker.rotation != rot)
            {
                _up = Vector2.zero;
                _marker.rotation = rot;
            }
        }
    }
}
