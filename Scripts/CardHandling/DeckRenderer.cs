using System;
using System.Linq;
using Godot;

public partial class DeckRenderer : Control
{
	const int CARD_SPACING = 185;
	DeckState deckData;

	RichTextLabel discardLabel;
	RichTextLabel drawPileLabel;
	// TEMP
	RichTextLabel cardPlayLog;

	PackedScene cardScene = GD.Load<PackedScene>("res://CardHandling/CardRenderer.tscn");

	public override void _Ready()
	{
		deckData = new DeckState();
		deckData.StateChanged += HandleStateChanged;
		discardLabel = GetNode<RichTextLabel>("%DiscardPileLabel");
		drawPileLabel = GetNode<RichTextLabel>("%DrawPileLabel");
		cardPlayLog = GetNode<RichTextLabel>("%CardPlayLogTemp");
		RefreshView();
	}

	void RenderHand()
	{
		for (int i = 0; i < deckData.handContents.Count; i++)
		{
			InstantiateCardInHand(i);
		}
	}

	void RenderDeckHUD()
	{
		discardLabel.Text = $"discard: {deckData.discardContents.Count}";
		drawPileLabel.Text = $"deck: {deckData.drawPileContents.Count}";
	}

	void InstantiateCardInHand(int index)
	{
		var position = new Vector2(CARD_SPACING * (index + 0.5f), 0);
		var cardInfo = deckData.handContents[index];
		var card = cardScene.Instantiate<CardRenderer>();
		card.indexInHand = index;
		card.Position = position;
		card.SetCardText(cardInfo);
		card.PlayButtonClicked += HandlePlayButtonClicked;
		AddChild(card);
		card.AddToGroup("CardRenderers");
	}

	void HandlePlayButtonClicked(int index)
	{
		cardPlayLog.Text = $"played card {deckData.handContents[index].title}!";
		cardPlayLog.Visible = true;
		GetTree().CreateTimer(1.0f).Timeout += () => cardPlayLog.Visible = false;
		deckData.PlayCard(index);
	}

	void HandleStateChanged(object sender, EventArgs e)
	{
		var cardRenderers = GetTree()
			.GetNodesInGroup("CardRenderers")
			.OfType<CardRenderer>();
		foreach (var r in cardRenderers)
		{
			RemoveChild(r);
			r.QueueFree();
		}
		RefreshView();
	}

	void RefreshView()
	{
		RenderHand();
		RenderDeckHUD();
	}
}