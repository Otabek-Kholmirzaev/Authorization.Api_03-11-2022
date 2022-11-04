namespace Authorization.Api.Services;

public class SolutionService
{
    public int GetAnswer(int n)
    {
        var answer = 1;
        for (int i = 1; i <= n; i++)
        {
            answer = Ekuk(answer, i);
        }

        return answer;
    }

    private int Ekuk(int a, int b)
    {
        return (a * b) / Ekub(a, b);
    }

    private int Ekub(int a, int b)
    {
        while (a != b)
        {
            if (a > b)
            {
                a -= b;
            }
            else
            {
                b -= a;
            } 
        }

        return a;
    }
}
