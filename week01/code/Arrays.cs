
public static class Arrays
{
    public static double[] MultiplesOf(double number, int length)
    {
        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    public static void RotateListRight(List<int> data, int amount)
    {
        int start = data.Count - amount;
        List<int> moved = data.GetRange(start, amount);

        data.RemoveRange(start, amount);
        data.InsertRange(0, moved);
    }
}
