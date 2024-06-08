using System;
using System.Collections.Generic;
using System.Threading;
using Godot;

public class DeckState
{
    public List<CardInfo> drawPileContents = new();
    public List<CardInfo> handContents = new();
    public List<CardInfo> discardContents = new();

    public event EventHandler StateChanged;

    protected virtual void OnStateChanged(EventArgs e)
    {
        StateChanged?.Invoke(this, e);
    }

    public DeckState() => Initialize();

    private void Initialize()
    {
        for (int i = 0; i < 10; i++)
        {
            var card = new CardInfo()
            {
                title = $"Example Card {i}",
                text = $"This is the {i}th example card",
            };
            drawPileContents.Add(card);
        }
        ShuffleDeck();

        for (int i = 0; i < 5; i++)
        {
            DrawCard();
        }
    }

    private void ShuffleDeck()
    {
        var newDeck = new List<CardInfo>();
        while (drawPileContents.Count > 0)
        {
            // Randi returns an unsigned int; we have to cast after taking the
            // the remainder or we might end up with a negative index
            var index = (int)(GD.Randi() % drawPileContents.Count);
            newDeck.Add(drawPileContents[index]);
            drawPileContents.RemoveAt(index);
        }
        drawPileContents = newDeck;
    }

    private void ReshuffleDiscard()
    {
        drawPileContents.AddRange(discardContents);
        ShuffleDeck();
    }

    private void DrawCard()
    {
        if (drawPileContents.Count == 0)
        {
            ReshuffleDiscard();
        }
        if (drawPileContents.Count == 0)
        {
            // nothing left to draw
            return;
        }
        var card = drawPileContents[0];
        drawPileContents.RemoveAt(0);
        handContents.Add(card);
    }

    public void PlayCard(int index)
    {
        discardContents.Add(handContents[index]);
        handContents.RemoveAt(index);
        OnStateChanged(new EventArgs());
    }
}
