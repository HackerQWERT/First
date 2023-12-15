

public partial class Player : Area2D
{

    [Signal]
    public delegate void HitEventHandler();

    [Export]
    public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).
    [Export]
    public AnimatedSprite2D PlayerAnimatedSprite2D { get; set; }
    public Vector2 ScreenSize; // Size of the game window.
                               // Called when the node enters the scene tree for the first time.
    public Vector2 velocity = Vector2.Zero; // The player's movement vector.              
    public override void _Ready()
    {
        GD.Print("Hello World!");
        // Hide();
        ScreenSize = GetViewportRect().Size;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Console.WriteLine(velocity);
        ChangePlayerVelocity();
        MovePlayer(delta);
        // See the note below about boolean assignment.
        ClampPlayerPosition();
        ControlPlayerAnimation();
    }

    void OnBodyEntered(PhysicsBody2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(SignalName.Hit);
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>(StringNames.PlayerNode.PlayerCollisionShape2D).SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<CollisionShape2D>(StringNames.PlayerNode.PlayerCollisionShape2D).Disabled = false;
    }
    void ChangePlayerVelocity()
    {
        velocity = Vector2.Zero; // The player's movement vector.

        if (Input.IsActionPressed(StringNames.PlayerInputMap.MoveRight))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed(StringNames.PlayerInputMap.MoveLeft))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed(StringNames.PlayerInputMap.MoveDown))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed(StringNames.PlayerInputMap.MoveUp))
        {
            velocity.Y -= 1;
        }

        if (velocity.Length() > 0)
            velocity = velocity.Normalized() * Speed;


    }
    void MovePlayer(double delta)
    {
        Position += velocity * (float)delta;
    }
    void ClampPlayerPosition()
    {
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
    void ControlPlayerAnimation()
    {
        PlayerAnimatedSprite2D.FlipH = velocity.X < 0;
        PlayerAnimatedSprite2D.FlipV = velocity.Y > 0;

        if (velocity.Length() > 0)
        {
            PlayerAnimatedSprite2D.Play();
        }
        else
        {
            PlayerAnimatedSprite2D.Stop();
        }

        if (velocity.X != 0)
        {
            PlayerAnimatedSprite2D.Animation = StringNames.AnimatedSprite.AnimatedSprite2DWalk;
        }
        else if (velocity.Y != 0)
        {
            PlayerAnimatedSprite2D.Animation = StringNames.AnimatedSprite.AnimatedSprite2DUp;
        }
    }
}
