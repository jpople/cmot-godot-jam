using System.Runtime.InteropServices;
using Godot;

public partial class Main : Node2D
{
    const float LABEL_SPEED = 3.0f;
    Label label;
    Rect2 bounds;
    Vector2 labelMovementDirection;

    public override void _Ready()
    {
        var viewportRect = GetViewportRect();
        var randomPosWithinViewport = new Vector2()
        {
            X = GD.Randf() * viewportRect.Size.X,
            Y = GD.Randf() * viewportRect.Size.Y,
        };

        label = new Label()
        {
            Text = "hello world!",
            Modulate = GetRandomPastelColor(),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Position = randomPosWithinViewport,
        };
        label.SetAnchorsPreset(Control.LayoutPreset.Center);
        AddChild(label);

        bounds = viewportRect.GrowIndividual(
            -label.Size.X / 2,
            -label.Size.Y / 2,
            -label.Size.X / 2,
            -label.Size.Y / 2
        );

        labelMovementDirection = GetRandomDiagonal();
    }

    public override void _PhysicsProcess(double delta)
    {
        var movementStep = labelMovementDirection * LABEL_SPEED;
        var nextPoint = label.GetRect().GetCenter() + movementStep;
        if (bounds.HasPoint(nextPoint))
        {
            label.Position += movementStep;
            return;
        }

        if (nextPoint.X < bounds.Position.X || nextPoint.X > bounds.End.X)
        {
            labelMovementDirection = labelMovementDirection.Reflect(Vector2.Up);
        }
        else if (nextPoint.Y < bounds.Position.Y || nextPoint.Y > bounds.End.Y)
        {
            labelMovementDirection = labelMovementDirection.Reflect(Vector2.Right);
        }
        label.Modulate = GetRandomPastelColor();
    }

    Vector2 GetRandomDiagonal()
    {
        var angles = new float[] {
            1 * Mathf.Pi / 4,
            3 * Mathf.Pi / 4,
            5 * Mathf.Pi / 4,
            7 * Mathf.Pi / 4,
        };
        var chosen = angles[GD.Randi() % angles.Length];
        return Vector2.FromAngle(chosen);
    }

    Color GetRandomPastelColor()
    {
        var selectedPastelColors = new Color[] {
            Colors.Salmon,
            Colors.SandyBrown,
            Colors.LemonChiffon,
            Colors.LightGreen,
            Colors.LightSkyBlue,
            Colors.PaleTurquoise,
            Colors.Plum
        };
        return selectedPastelColors[GD.Randi() % selectedPastelColors.Length];
    }
}
