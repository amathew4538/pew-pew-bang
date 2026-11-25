using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	[Export] public float Speed = 5.0f;
	[Export] public float JumpVelocity = 4.5f;
	[Export] public float SprintMult = 2f;
	[Export] public float CrouchMult = 0.5f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			if (Input.IsActionJustPressed("crouch"))
			{
				velocity.X = direction.X * Speed * CrouchMult;
				velocity.Z = direction.Z * Speed * CrouchMult;
			}
			else if (Input.IsActionJustPressed("sprint"))
			{
				velocity.X = direction.X * Speed * SprintMult;
				velocity.Z = direction.Z * Speed * SprintMult;
			}
			else
            {
				velocity.X = direction.X * Speed;
				velocity.Z = direction.Z * Speed;
            }
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void UpdateRotation(Vector3 rotation)
    {
        Transform3D currentTransform = GlobalTransform;
		currentTransform.Basis = Basis.FromEuler(rotation);
		GlobalTransform = currentTransform;
    }
}
