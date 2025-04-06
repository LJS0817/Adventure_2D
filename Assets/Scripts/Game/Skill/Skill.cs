using UnityEngine;

public class Skill
{
    protected Vector2 _targetPos = Vector2.zero;

    protected string _name;
    protected string _desc;
    protected Player _player = null;

    protected bool _activated = false;
    protected float _force;

    public virtual void Update() {
        if (!_activated || !_player) return;
    }

    public void SetTarget(Vector2 pos) { _targetPos = pos; }
    public Vector2 GetTargetPos() { return _targetPos; }

    public void Activate(Player p) { _player = p; _activated = true; action(); }
    protected virtual void action() { }
    public bool isAcativated() { return _activated; }
}
