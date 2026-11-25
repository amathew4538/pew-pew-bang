using Godot;
using System;

public partial class CameraController : Node3D
{
	[Export] public bool Debug = false;

	[ExportCategory("References")]
	[Export] PlayerController CurrentPlayerController;
	[Export] MouseCapture CurrentMouseCapture;

	[ExportCategory("Camera Settings")]
	[ExportGroup("Camera Tilt")]
	[Export(PropertyHint.Range, "-90,-60")] int TiltLowerLimit = -90;
	[Export(PropertyHint.Range, "60,90")] int TiltUpperLimit = 90;

	private Vector3 _rotation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        UpdateCameraRotation(CurrentMouseCapture.GetMouseInput());
    }

	private void UpdateCameraRotation(Vector2 input)
    {
		_rotation.X += input.Y;
		_rotation.Y += input.X;

		_rotation.X = Mathf.Clamp(_rotation.X, Mathf.DegToRad(TiltLowerLimit), Mathf.DegToRad(TiltUpperLimit));

		Vector3 _playerRotation = new(0, _rotation.Y, 0);
		Vector3 _cameraRotation = new(_rotation.X, 0, 0);

		Transform3D currentTransform = Transform;
		currentTransform.Basis = Basis.FromEuler(_cameraRotation);
		Transform = currentTransform;

		CurrentPlayerController.UpdateRotation(_playerRotation);

		_rotation.Z = 0;
    }
}
