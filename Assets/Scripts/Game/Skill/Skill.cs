using UnityEngine;

public class Skill
{
    protected Vector2 _targetPos;
    protected string _name;
    protected string _desc;
    protected Player _player = null;

    protected bool _activated;
    protected float _power;

    public virtual void Update() { }

    public void SetTarget(Vector2 pos) { _targetPos = pos; }
    public Vector2 GetTargetPos() { return _targetPos; }

    public void Activate(Player p) { _player = p; _activated = true; action(); }
    protected virtual void action() { }
    public bool isAcativated() { return _activated; }
}
