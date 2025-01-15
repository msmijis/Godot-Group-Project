using Godot;
using System;

public partial class TextureRect : Godot.TextureRect
{
    private int _speed = 400;
    private float _angularSpeed = Mathf.Pi;

    public override void _Process(double delta) {
        Rotation += _angularSpeed * (float)delta;
    }
}