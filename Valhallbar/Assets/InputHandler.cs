using System;
using JetBrains.Annotations;
using UnityEngine;

public class InputHandler
{
	public event EventHandler<EventArgs> SwitchLaneMove;

	private readonly string _axis;
    private readonly Action<int> _doStuff;
    private readonly bool _useTimeout;
    private bool _wasControlling;
    private float _lastTriggerTime;

    public InputHandler([NotNull] string axis, [NotNull] Action<int> doStuff, bool useTimeout = false)
    {
        if (axis == null) throw new ArgumentNullException("axis");
        if (doStuff == null) throw new ArgumentNullException("doStuff");

        _axis = axis;
        _doStuff = doStuff;
        _useTimeout = useTimeout;
    }

    public void Process()
    {
        var input = Input.GetAxisRaw(_axis);
        var isControlling = Mathf.Abs(input) > 0.2f;
        var wasControlling = _wasControlling;

        _wasControlling = isControlling;

        if (wasControlling || !isControlling) return;
        if (_useTimeout && Time.time - _lastTriggerTime < 0.2f) return;

        _doStuff(Mathf.RoundToInt(Mathf.Sign(input)));
        _lastTriggerTime = Time.time;
    }
}