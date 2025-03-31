using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public Transform Marker;

    public Transform _mouseIndicatorOffset;
    MouseIndicator _mouseIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mouseIndicator = new MouseIndicator(Marker);
    }

    // Update is called once per frame
    void Update()
    {
        _mouseIndicatorOffset.rotation = _mouseIndicator.LerpRotation(_mouseIndicatorOffset.position, _mouseIndicatorOffset.rotation);
        _mouseIndicator.SetMarker(transform.position, _mouseIndicatorOffset.up);
    }

    public Vector2 GetHorizontalMovement(float speed)
    {
        return transform.right * Input.GetAxisRaw("Horizontal") * speed;
    }
}
