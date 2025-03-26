using Godot;
using System;

public partial class Door : Node2D
{
	player player;
	Node currentScene;
	private AnimatedSprite2D _doorSprite;
	private AnimationPlayer SceneTransition;
	bool within_access_range = false;
	
	public override void _Ready() {
    
    	currentScene = GetTree().CurrentScene;
		string sceneName = currentScene.Name;
		player = (player)GetTree().Root.GetNode(sceneName).GetNode("Player");
		_doorSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        SceneTransition = GetNode<AnimationPlayer>("SceneTransition/AnimationPlayer");
		
		
		// connect signals
		var accessRange = GetNode<Area2D>("AccessRange");
		accessRange.BodyEntered += OnAccessRangeBodyEnter;
		accessRange.BodyExited += OnAccessRangeBodyExit;
	}
	
	    public override void _Process(double delta) {
        if (within_access_range) {
            if (Input.IsActionJustPressed("space")) {
                SceneTransition.Play("fade_in");
                Timer();
            }
        }
    }

    private async void Timer() {
        // Create a timer and wait for the timeout signal asynchronously
        await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		ColorRect Rect = GetNode<ColorRect>("SceneTransition/ColorRect");
		Rect.Color = new Color();
		EnterDoor(currentScene);
		SceneTransition.Play("fade_out");

    }
	
	private void EnterDoor(Node currentScene) {
		if (currentScene.Name == "World2") {
			GetTree().ChangeSceneToFile("res://scenes/world.tscn");
		} else {
			GetTree().ChangeSceneToFile("res://scenes/world_2.tscn");
		}
	}
	
	/*private void updateAnimation() {
		if (within_access_range) {
			_doorSprite.Animation = 
		}
	}*/
	
	public void OnAccessRangeBodyEnter(Node2D body) {
		within_access_range = true;
		_doorSprite.Animation = "door_open";
	}
	
	public void OnAccessRangeBodyExit(Node2D body) {
		within_access_range = false;
		_doorSprite.Animation = "door_close";
	}
}
