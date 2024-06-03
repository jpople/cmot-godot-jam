extends Resource

class_name Grid

# Resource that contains shared static data about the game grid.
# Cannot be modified, only read.

@export var gridSize 	:= Vector2(5, 5)
@export var pixelSize 	:= Vector2(32, 32)

var halfSize = pixelSize / 2

func local_pos_to_grid_pos( localPos : Vector2 ) -> Vector2:
	return ( ( localPos + halfSize ) / pixelSize ).floor();
	
func grid_pos_to_local_pos( gridPos : Vector2 ) -> Vector2:
	return pixelSize * gridPos;
	
func local_pos_to_grid_center_pos( localPos : Vector2 ) -> Vector2:
	var gridPos = local_pos_to_grid_pos( localPos );
	var gridCenter = grid_pos_to_local_pos( gridPos );
	return gridCenter
