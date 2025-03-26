using Godot;
using System;
using System.Threading.Tasks;

public partial class Textbox : CanvasLayer
{
	private const double CHAR_READ_RATE = 0.5;

	private MarginContainer textboxContainer;
	private Label startSymbol;
	private Label label;

	public override void _Ready() {
		textboxContainer = GetNode<MarginContainer>("TextboxContainer");
		startSymbol = GetNode<Label>("TextboxContainer/MarginContainer/HBoxContainer/Start");
		label = GetNode<Label>("TextboxContainer/MarginContainer/HBoxContainer/Label");
		AddText("Hello bro.");
	}   

	private void HideTextbox() {
		startSymbol.SetText("");
		label.SetText("");
		textboxContainer.Hide();
	}

	private void ShowTextbox() {
		startSymbol.SetText("*");
		textboxContainer.Show();
	}

	private async Task AddText(string text) {
		label.SetText(text);
		ShowTextbox();

		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(label, "visible_characters", text.Length, text.Length * CHAR_READ_RATE).From(0);
	}
}
