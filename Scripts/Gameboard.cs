using Godot;
using System;
using System.Collections.Generic;

public struct Cell 
{
	public Vector2I cellPos;
	public Vector2 localCenter;
	public Building data;

	public Cell( Vector2I cellPos, Vector2 localCenter, Building data )
	{
		this.cellPos = cellPos;
		this.localCenter = localCenter;
		this.data = data;
	}
}

public partial class Gameboard : Node2D
{
	[Export] public TileMap tilemap;

	[Signal] public delegate void GridMouseMotionEventHandler( bool hasChangedCell, Vector2I cellPos, Vector2 globalPos );
	[Signal] public delegate void GridMouseButtonEventHandler( Vector2I cellPos, Vector2 globalPos );

	public Vector2 HoveredCell { get; private set; }

	public Dictionary<Vector2I, Cell> Grid { get; private set; } = new Dictionary<Vector2I, Cell>();

	// Return the center local pos of the cell the mouse is currently hovering on
	public Vector2 GetMouseCurrentCellCenter()
	{
		Vector2 localMousePos = this.GetLocalMousePosition();
		Vector2I cellPos = tilemap.LocalToMap( localMousePos );
		return tilemap.MapToLocal( cellPos );
	}

	// Return the cell coordinate at the given position
	public Vector2I LocalToCellPos( Vector2 localPosition )
	{
		return tilemap.LocalToMap( localPosition );
	}

	public bool HasBuilding( Vector2I cellPos )
	{
		if ( !Grid.ContainsKey( cellPos ))
		{
			return false;
		}

		Cell cell = Grid[ cellPos ];

		return cell.data != null;
	}

	public Building GetBuilding(Vector2I cellPos){
		if(HasBuilding(cellPos)){
			return Grid[cellPos].data;
		}
		return null;
	}

	public List<Building> GetBuildings(List<Vector2I> cellPos){
		List<Building> Out = new();

		foreach(Vector2I pos in cellPos){
			if (HasBuilding(pos)){
				Out.Add(GetBuilding(pos));
			}
		}

		return Out;
	}

	public bool AddBuilding( Vector2I cellPos, Building building )
	{
		if ( HasBuilding( cellPos ) )
			return false;

		Cell newCell = new Cell( cellPos, tilemap.MapToLocal( cellPos ), building );
		Grid[ cellPos ] = newCell;
		building.OnPlaced(cellPos, this);

		// TODO : Get Sprite and Instantiate SpriteNode? Setting them directly into tilemap seems clunky.
		// Placeholder : place a generic red tile
		Vector2I redTileAtlasPos = new Vector2I( 0, 6 );
		tilemap.SetCell( 0, cellPos, 1, redTileAtlasPos );

		return true;
	}

/**
	Because I can't get all the coordinate conversion right when listening from other objects, let the gameboard listen
	to mouse motion, and re-propagate the signal with the conversion done and extra data.
**/
	private void OnArea2DEntered( Viewport viewport, InputEvent inputEvent, int shapeIdx )
	{
		InputEventMouseMotion mouseEvent = inputEvent as InputEventMouseMotion;
		InputEventMouseButton buttonEvent = inputEvent as InputEventMouseButton;

		Vector2 localMousePos 		= this.GetLocalMousePosition();
		Vector2I newHoveredCellPos 	= this.LocalToCellPos( localMousePos );
		Vector2 cellCenterPos 		= GetMouseCurrentCellCenter();
		bool hasChangedCell 		= HoveredCell == newHoveredCellPos;

		if ( buttonEvent != null )
		{
			//Add a fake building for now
			BuildingData data = (BuildingData)GD.Load("res://Resources/Cards/House.tres");
			Building building = new Building(data);
			AddBuilding( newHoveredCellPos, building );

			EmitSignal( SignalName.GridMouseButton, newHoveredCellPos, cellCenterPos );
		}

		if ( mouseEvent != null )
		{
			HoveredCell = newHoveredCellPos;

			EmitSignal(SignalName.GridMouseMotion, hasChangedCell, newHoveredCellPos, cellCenterPos );
		}
	}

}
