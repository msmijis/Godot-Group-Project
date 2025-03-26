using Godot;
using System;

public partial class World2 : Node2D
{
    private AnimationPlayer sceneTransition;
    private ColorRect colorRect;

    public override void _Ready()
    {
        colorRect = GetNode<ColorRect>("SceneTransition/ColorRect");
        sceneTransition = GetNode<AnimationPlayer>("SceneTransition/AnimationPlayer");

        if (colorRect != null)
        {
            Visible = false;

            // Start fully black
            colorRect.Color = new Color(0, 0, 0, 1);
        }

        // Wait before showing the scene & fading out
        GetTree().CreateTimer(0.01f).Timeout += ShowAndFadeOut;
    }

    private void ShowAndFadeOut()
    {
        Visible = true;

        if (sceneTransition != null)
        {
            sceneTransition.Play("fade_out");
        }
    }
}
