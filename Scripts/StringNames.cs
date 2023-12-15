
public static class StringNames
{
    public static class PlayerInputMap
    {
        
        public static string MoveRight { get; set; } = "MoveRight";
        public static string MoveLeft { get; set; } = "MoveLeft";
        public static string MoveDown { get; set; } = "MoveDown";
        public static string MoveUp { get; set; } = "MoveUp";
    }
    public static class AnimatedSprite
    {
        public static string AnimatedSprite2DWalk { get; set; } = "Walk";
        public static string AnimatedSprite2DUp { get; set; } = "Up";
    }

    public static class PlayerNode
    {
        public static string PlayerCollisionShape2D { get; set; } = "CollisionShape2D";
    }

    public static class MainNode
    {
        public static string Player { get; set; } = "Player";

        public static string MobTimer { get; set; } = "MobTimer";
        public static string ScoreTimer { get; set; } = "ScoreTimer";
        public static string StartTimer { get; set; } = "StartTimer";
        public static string StartPosition { get; set; } = "StartPosition";
    }

}
