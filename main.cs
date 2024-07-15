using Godot;
using System;
using System.Linq;

public partial class main : Node2D
{
	public Texture2D Dead = (Texture2D)GD.Load("res://Textures/Dead.png");
	public Texture2D Alive = (Texture2D)GD.Load("res://Textures/Alive.png");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_focus_next"))
		{
			if(GetChild<Timer>(1).IsStopped())
			{
				GetChild<Timer>(1).Start();
			}else{
				GetChild<Timer>(1).Stop();
			}
			
		}
	}
	public void _on_timer_timeout()
	{
		var x=GetTree().GetNodesInGroup("Cell");
		foreach (var item in x.Cast<cell>())
		{
			item.checkNextState();
		}
		foreach (var item in x.Cast<cell>())
		{
			item.goNextState();
		}
	}
	public void _load()
	{
		var x=GetTree().GetNodesInGroup("Cell");
		foreach (var item in x.Cast<cell>())
		{
			item.afterLoad();
		}
		
	}
}
