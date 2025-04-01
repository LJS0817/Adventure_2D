using UnityEngine;

public class Skill
{
    protected Vector2 _targetPos;
    protected string _name;
    protected string _desc;

    protected bool _activated;
    // Update is called once per frame
    public void Update()
    {
        
    }

    public void SetTarget(Vector2 pos) { _targetPos = pos; }
    public Vector2 GetTargetPos() { return _targetPos; }

    public virtual void Activate() { _activated = true; }
    public bool isAcativated() { return _activated; }
}
