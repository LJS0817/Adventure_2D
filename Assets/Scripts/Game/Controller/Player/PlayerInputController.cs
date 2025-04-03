using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public Transform Marker;

    public Transform _mouseIndicatorOffset;
    MouseIndicator _mouseIndicator;

    Player _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mouseIndicator = new MouseIndicator(Marker);
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _mouseIndicatorOffset.rotation = _mouseIndicator.LerpRotation(_mouseIndicatorOffset.position, _mouseIndicatorOffset.rotation);
        _mouseIndicator.SetMarker(transform.position, _mouseIndicatorOffset.up, transform.rotation);

        if(Input.GetMouseButtonDown(0)) {
            _player.SkillActivate();
            _player.SetState(PLAYER_STATE.E_FAST_MOVE);
        }
    }

    public bool IsMarkerTurned() { return _mouseIndicator.GetContacted(); }
    public Vector2 GetHorizontalMovement(float speed) { return transform.right * Input.GetAxisRaw("Horizontal") * speed; }
}
