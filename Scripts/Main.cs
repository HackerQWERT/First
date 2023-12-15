public partial class Main : Node
{
    // Don't forget to rebuild the project so the editor knows about the new export variable.

    [Export]
    public PackedScene MobScene { get; set; }

    int _score;
    public override void _Ready()
    {

    }


    public void GameOver()
    {
        GetNode<AudioStreamPlayer2D>("Music").Stop();
        GetNode<AudioStreamPlayer2D>("DeathSound").Play();

        GetNode<HUD>("HUD").ShowGameOver();

        GetNode<Godot.Timer>(StringNames.MainNode.MobTimer).Stop();

        GetNode<Godot.Timer>(StringNames.MainNode.ScoreTimer).Stop();
    }

    public void NewGame()
    {
        // Note that for calling Godot-provided methods with strings,
        // we have to use the original Godot snake_case name.
        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
        GetNode<AudioStreamPlayer2D>("Music").Play();
        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");

        _score = 0;

        var player = GetNode<Player>(StringNames.MainNode.Player);
        var startPosition = GetNode<Marker2D>(StringNames.MainNode.StartPosition);
        player.Start(startPosition.Position);

        GetNode<Godot.Timer>(StringNames.MainNode.StartTimer).Start();
    }
    void OnScoreTimerTimeout()
    {
        _score++;
        GetNode<HUD>("HUD").UpdateScore(_score);
    }

    void OnStartTimerTimeout()
    {
        GetNode<Timer>(StringNames.MainNode.MobTimer).Start();
        GetNode<Timer>(StringNames.MainNode.ScoreTimer).Start();
    }
    void OnMobTimerTimeout()
    {
        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        Mob mob = MobScene.Instantiate<Mob>();

        // Choose a random location on Path2D.
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath2D/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        // Set the mob's direction perpendicular to the path direction.
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        var ScreenSize = GetNode<Player>("Player").ScreenSize;
        // Set the mob's position to a random location.
        mob.Position = mobSpawnLocation.Position;
        GD.Print(mob.Position);
        // Vector2 position;
        // position.X = Math.Clamp(mobSpawnLocation.Position.X, 0, ScreenSize.X);
        // position.Y = Math.Clamp(mobSpawnLocation.Position.X, 0, ScreenSize.Y);
        // mob.Position = position;


        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        // Spawn the mob by adding it to the Main scene.
        AddChild(mob);
    }
}