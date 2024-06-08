using Godot;
using System;

public partial class Gameboard : Node2D
{
	
	[Export] public TileMap tilemap;

	[Signal] public delegate void GridMouseMotionEventHandler( bool hasChangedCell, Vector2I cellPos, Vector2 globalPos );

	public Vector2 HoveredCell { get; private set; }


	// Return the center local pos of the cell the mouse is currently hovering on
	public Vector2 GetMouseCurrentCellCenter()
	{
		Vector2 localMousePos = this.GetLocalMousePosition();
		Vector2I mapPosition = tilemap.LocalToMap( localMousePos );
		return tilemap.MapToLocal( mapPosition );
	}


	// Return the cell coordinate at the given position
	public Vector2I LocalToCellPos( Vector2 localPosition )
	{
		return tilemap.LocalToMap( localPosition );
	}

/**
	Because I can't get all the coordinate conversion right when listening from other objects, let the gameboard listen
	to mouse motion, and re-propagate the signal with the conversion done and extra data.
**/
	private void OnArea2DEntered( Viewport viewport, InputEvent inputEvent, int shapeIdx )
	{
		InputEventMouseMotion mouseEvent = inputEvent as InputEventMouseMotion;

		if ( mouseEvent == null )
			return;

		Vector2 localMousePos 		= this.GetLocalMousePosition();
		Vector2I newHoveredCellPos 	= this.LocalToCellPos( localMousePos );
		Vector2 cellCenterPos 		= GetMouseCurrentCellCenter();
		bool hasChangedCell 		= HoveredCell == newHoveredCellPos;

		HoveredCell = newHoveredCellPos;

		EmitSignal(SignalName.GridMouseMotion, hasChangedCell, newHoveredCellPos, cellCenterPos );

		return;
	}

}
