using Godot;
using System;

public partial class Gameboard : Node2D
{
	
	[Export] public TileMap tilemap;

	[Export] Node2D cursor;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnArea2DEntered( Viewport viewport, InputEvent inputEvent, int shapeIdx )
	{
		InputEventMouseMotion mouseEvent = inputEvent as InputEventMouseMotion;

		if ( mouseEvent == null )
			return;

		Vector2 localMousePos = this.GetLocalMousePosition();
		Vector2I mapPosition = tilemap.LocalToMap( localMousePos );
		Vector2 tileCenter = tilemap.MapToLocal( mapPosition );

		cursor.Position = tileCenter;

		return;
	}

}
