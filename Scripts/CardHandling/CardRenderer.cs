using System;
using Godot;

public partial class CardRenderer : PanelContainer
{
	[Export] RichTextLabel titleLabel;

	[Export] RichTextLabel textLabel;
	public int indexInHand;

	[Signal] public delegate void PlayButtonClickedEventHandler(int index);

	public override void _Ready()
	{
		titleLabel = GetNode<RichTextLabel>("%Title");
		textLabel = GetNode<RichTextLabel>("%Text");
	}

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton e)
		{
			if (e.ButtonIndex == MouseButton.Left && e.Pressed)
			{
				EmitSignal(SignalName.PlayButtonClicked, indexInHand);
			}
		}
	}

	public void SetCardText(CardInfo cardInfo)
	{
		titleLabel.Text = cardInfo.title;
		textLabel.Text = cardInfo.text;
	}

}
