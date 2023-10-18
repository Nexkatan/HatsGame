public enum HatRotation
{
    Zero, Sixty, OneTwenty, OneEighty, TwoForty, ThreeHundred
}

public static class HatRotationExtensions
{

    public static HatRotation Opposite(this HatRotation rotation)
    {
        return (int)rotation < 3 ? (rotation + 3) : (rotation - 3);
    }
}