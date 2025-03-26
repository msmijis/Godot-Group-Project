using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Textbox : CanvasLayer
{
	private const double CHAR_READ_RATE = 0.05;

	private MarginContainer textboxContainer;
	private Label startSymbol;
	private Label label;

	private Queue<string> queue = new Queue<string>();
	private string state = "READY";

	public override void _Ready() {
		textboxContainer = GetNode<MarginContainer>("TextboxContainer");
		startSymbol = GetNode<Label>("TextboxContainer/MarginContainer/HBoxContainer/Start");
		label = GetNode<Label>("TextboxContainer/MarginContainer/HBoxContainer/Label");
		AddText("Hello world.");
		AddText("My name is Frog!");
	}

    public override void _Process(double delta) {
		switch (state) {
			case "READY":
				if (queue.Count != 0) {
					DisplayText();
				}
				break;
			case "READING":
				if (Input.IsActionJustPressed("space")) {
					label.VisibleCharacters = 1;
					//tween.Stop();
					state = "FINISHED";
				}
				break;
			case "FINISHED":
				if (Input.IsActionJustPressed("space")) {
					state = "READY";
					HideTextbox();
				}
				break;
		}
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

	private void AddText(string text) {
		queue.Enqueue(text);
	}

	private async Task DisplayText() {
		string nextText = queue.Dequeue();
		label.SetText(nextText);
		state = "READING";
		ShowTextbox();
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(label, "visible_characters", label.Text.Length, label.Text.Length * CHAR_READ_RATE).From(0);

		await ToSignal(tween, Tween.SignalName.Finished);
		state = "FINISHED";

	}
}
