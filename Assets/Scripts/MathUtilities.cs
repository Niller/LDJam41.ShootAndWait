namespace DefaultNamespace
{
    public static class MathUtilities
    {
        public static float Normalize180Angle(float angle)
        {
            return angle >= 180f ? angle - 360f : angle;
        }
    }
}