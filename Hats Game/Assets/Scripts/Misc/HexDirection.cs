public enum HexDirection
{
    NE, E, SE, SW, W, NW
}

public enum LongboiDirection
{
    SW_SW, SW_W, W_W, W_NW, NW_NW, NW_NE, NE_NE, NE_E, E_E, E_SE, SE_SE, SE_SW
}

public static class HexDirectionExtensions
{

    public static HexDirection Opposite(this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }
}