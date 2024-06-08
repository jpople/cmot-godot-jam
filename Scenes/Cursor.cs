using Godot;
using System;

public partial class Cursor : Sprite2D
{
    [ Export ] private Gameboard gb;

	public override void _Ready()
	{
		gb.GridMouseMotion += OnGridMouseMotion;
	}

	private void OnGridMouseMotion( bool hasChangedCell, Vector2I cellPos, Vector2 globalPos )
	{
		this.Position = globalPos;
	}
}
