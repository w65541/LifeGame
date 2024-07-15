using Godot;
using GodotPlugins.Game;
using System;
using System.Collections.Generic;

public partial class cell : Node2D
{
	// Called when the node enters the scene tree for the first time.
	Texture2D Dead;
	Texture2D Alive ;
	List<cell> somsiedzi=new List<cell>();
	[Export] public int currentState=0;
	public int nextState=0;
	
	public override void _Ready()
	{
		Dead=GetTree().Root.GetChild<main>(0).Dead;
		Alive=GetTree().Root.GetChild<main>(0).Alive;
		if(currentState==0)
		{
			//currentState=0;
			GetChild<Sprite2D>(0).Texture=Dead;
		}else{
			//currentState=1;
			GetChild<Sprite2D>(0).Texture=Alive;
		}
		/*for (int i = 2; i < 10; i++)
		{
			somsiedzi.Add((cell)GetChild<RayCast2D>(i).GetCollider());
		}
		foreach (var item in somsiedzi)
		{
			GD.Print(Name+" "+item.Name+" "+item.currentState);
		}*/
	}
	public void afterLoad()
	{
		for (int i = 1; i < 9; i++)
		{
			if(GetChild(1).GetChild<RayCast2D>(i).IsColliding())
			{
			var x=(Node)GetChild(1).GetChild<RayCast2D>(i).GetCollider();
			somsiedzi.Add(x.GetParent<cell>());
			}
		}
		
			//GD.Print(Name+" "+somsiedzi.Count);
		
		for (int i = 1; i < 9; i++)
		{
			
			GetChild(1).GetChild<RayCast2D>(i).Dispose();
			
			}
		}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	/*public override void _Process(double delta)
	{
		
		if (Input.IsActionJustPressed("ui_focus_next"))
		{
			afterLoad();
		}
	}*/
	public void checkNextState()
	{
		int aliveN=0;
		foreach (var item in somsiedzi)
		{
			aliveN+=item.currentState;
		}
		if(currentState==0 && aliveN==3){
			nextState=1;
		}else{
			if(currentState==1 && (aliveN==3 || aliveN==2)) 
			{
				nextState=1;
			}else{
				nextState=0;
			}
		}
		
	}
	public void goNextState()
	{
		currentState=nextState;
		if(currentState==0)
		{
			//currentState=0;
			GetChild<Sprite2D>(0).Texture=Dead;
		}else{
			//currentState=1;
			GetChild<Sprite2D>(0).Texture=Alive;
		}
	}
	public void _on_area_2d_input_event(Node node,InputEvent inputEvent,int shape)
	{
		if(inputEvent.GetType()==new InputEventMouseButton().GetType()){
		var x=(InputEventMouseButton)inputEvent;
		if(inputEvent.IsPressed() && x.ButtonIndex.Equals(MouseButton.Left)){
		if(currentState==1)
		{
			currentState=0;
			GetChild<Sprite2D>(0).Texture=Dead;
		}else{
			currentState=1;
			GetChild<Sprite2D>(0).Texture=Alive;
		}}}
	}

	public void _on_area_2d_mouse_entered()
	{
		GetChild<CanvasItem>(2).Visible=true;
	}
	public void _on_area_2d_mouse_exited()
	{
		GetChild<CanvasItem>(2).Visible=false;
	}
}
