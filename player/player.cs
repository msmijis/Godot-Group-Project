using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	private int speed = 50;

	private AnimatedSprite2D _playerSprite;
	private Vector2 currentVelocity;
	private String direction = "down";

	public override void _Ready() {
		_playerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		handleInput();

		Velocity = currentVelocity;
		MoveAndSlide();

		updateAnimation();
	}

	private void updateAnimation() {
		if (currentVelocity.Length() == 0) { //if currentVelocity's magnitude is zero...
			_playerSprite.Stop(); //first frame of walk animation only will show
			return;
		}

		string new_direction = "down";
		if (currentVelocity.X < 0) new_direction = "left";
		else if (currentVelocity.X > 0) new_direction = "right";
		else if (currentVelocity.Y < 0) new_direction = "up";
		else if (currentVelocity.Y > 0) new_direction = "down";
		
		if (direction != new_direction) { //prevents frames from reseting
			direction = new_direction;
			_playerSprite.Animation = "walk_" + direction; //resets frame count to 0 and changes animation to correct direction
		}

		_playerSprite.Play();
	}

	private void handleInput() {
		currentVelocity = Input.GetVector("move_left","move_right", "move_up", "move_down"); //sets unit vector determining movement direction
		currentVelocity = currentVelocity.Normalized(); //avoids diagonal movement being faster
		currentVelocity *= speed; //multiplies aforementioned unit vector for movement by speed constant
	}
}
