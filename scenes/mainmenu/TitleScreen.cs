using Godot;
using System;

public partial class TitleScreen : Node2D
{
	private void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/levels/world.tscn");
	}

	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
