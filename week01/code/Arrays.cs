
public static class Arrays
{
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create an array with the specified length
        // to store the multiples of the given number.
        double[] multiples = new double[length];

        // Step 2: Loop through every position in the array.
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculate each multiple by multiplying
            // the number by (i + 1), since indexes start at 0.
            multiples[i] = number * (i + 1);
        }

        // Step 4: Return the array containing all multiples.
        return multiples;
    }

    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Calculate where the last 'amount'
        // elements begin in the original list.
        int start = data.Count - amount;

        // Step 2: Copy the elements that need to be
        // moved into a temporary list.
        List<int> moved = data.GetRange(start, amount);

        // Step 3: Remove the copied elements
        // from their original positions.
        data.RemoveRange(start, amount);

        // Step 4: Insert the saved elements at the
        // beginning of the original list.
        data.InsertRange(0, moved);
    }
}
