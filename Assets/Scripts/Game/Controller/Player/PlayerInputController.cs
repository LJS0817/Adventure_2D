using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public Transform _mouseIndicatorOffset;
    MouseIndicator _mouseIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mouseIndicator = new MouseIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        _mouseIndicator.Update();
        _mouseIndicatorOffset.rotation = _mouseIndicator.LerpRotation(transform.position, _mouseIndicatorOffset.rotation);
    }

}
