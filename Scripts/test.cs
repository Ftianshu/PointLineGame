public class Solution
{
    public bool ValidateStackSequences(int[] pushed, int[] popped)
    {
        int left = 0;
        for (int i = 0; i < popped.Length; i++)
        {
            while (pushed[left] != popped[i])
            {
                left++;
                if (left > pushed.Length)
                {
                    return false;
                }
            }
            if (left + 1 < pushed.Length - 1)
            {
                pushed[left] = pushed[left + 1];
            }
            left--;
        }
        return true;
    }
}