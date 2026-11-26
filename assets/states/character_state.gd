class_name CharacterState
extends LimboState

@export var animation_name : StringName

func _enter() -> void:
	agent.sprite.play(animation_name)
