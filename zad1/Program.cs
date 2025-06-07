class KNN
{
    public string DistanceType;

    public double CalculateDistance(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (DistanceType == "euclidean")
            {
                sum += (a[i] - b[i]) * (a[i] - b[i]);
            }
            else if (DistanceType == "manhattan")
            {
                sum += Math.Abs(a[i] - b[i]);
            }
        }

        if (DistanceType == "euclidean")
        {
            return Math.Sqrt(sum);
        }
        else
        {
            return sum;
        }
    }
}