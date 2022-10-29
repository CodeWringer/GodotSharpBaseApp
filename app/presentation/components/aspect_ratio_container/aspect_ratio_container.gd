tool

# Represents an AspectRatioContainer with a hacky fix to its behavior, 
# so that it actually maintains its aspect ratio, depending on its
# stretch_mode. 
extends AspectRatioContainer

func _ready():
	connect("resized", self, "_on_resized")
	_do_size_hack()
	
func _on_resized():
	_do_size_hack()

func _do_size_hack():
	if (self.stretch_mode == STRETCH_HEIGHT_CONTROLS_WIDTH):
		self.rect_min_size.x = self.rect_size.y
		self.rect_min_size.y = self.rect_size.y
	elif (self.stretch_mode == STRETCH_WIDTH_CONTROLS_HEIGHT):
		self.rect_min_size.x = self.rect_size.x
		self.rect_min_size.y = self.rect_size.x
