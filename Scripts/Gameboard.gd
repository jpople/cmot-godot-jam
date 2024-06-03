extends Node2D

# Script that manages most of the behaviour around the game grid.

@export var grid : Grid

# Should probably be moved in its own class
@export var cursor : Node2D

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_area_2d_input_event(viewport, event, shape_idx):
	var mouseEvent = event as InputEventMouseMotion
	
	if !mouseEvent:
		return
		
	var globalMousePos = self.get_global_mouse_position();
	var localMousePos = self.get_local_mouse_position();
	var localPosition = self.to_local(event.position)
	
	var cellCenter = grid.local_pos_to_grid_center_pos( globalMousePos )
	
	cursor.global_position = cellCenter

