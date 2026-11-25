using Godot;
using System;

public partial class MouseCapture : Node
{
	[Export] public bool Debug = false;

	[ExportCategory("Mouse Capture Settings")]
	[Export] public Input.MouseModeEnum CurrentMouseMode = Input.MouseModeEnum.Captured;
	[Export] public float MouseSensitivity = 0.005f;

	private Vector2 _mouseInput;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Input.MouseMode = CurrentMouseMode;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        _mouseInput = Vector2.Zero;

		if (Input.IsActionJustPressed("exit") && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            Input.MouseMode = CurrentMouseMode = Input.MouseModeEnum.Visible;
        }

		if (Input.IsActionJustPressed("interact") && Input.MouseMode == Input.MouseModeEnum.Visible)
        {
            Input.MouseMode = CurrentMouseMode = Input.MouseModeEnum.Captured;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            _mouseInput.X += -mouseMotion.ScreenRelative.X * MouseSensitivity;
			_mouseInput.Y += -mouseMotion.ScreenRelative.Y * MouseSensitivity;
        }

		if (Debug)
        {
			GD.Print(_mouseInput);
        }
    }

	public Vector2 GetMouseInput()
    {
        return _mouseInput;
    }
}
