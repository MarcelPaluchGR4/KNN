class DataRow
{
    public double[] Numbers;
    public string Category;

    public DataRow(double[] n, string c)
    {
        Numbers = n;
        Category = c;
    }
}

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


class Program
{
    static void Main()
    {
        var loaded = ReadCsv("/home/mp/python_projects/knn/training_data.csv");
    }

    static List<DataRow> ReadCsv(string filename)
    {
        var rows = new List<DataRow>();
        var stuff = File.ReadAllLines(filename);

        foreach (var line in stuff)
        {
            var bits = line.Split(',');
            if (bits.Length != 5) continue;

            double[] nums = new double[4];
            for (int i = 0; i < 4; i++)
            {
                nums[i] = double.Parse(bits[i]);
            }

            string type = bits[4];
            rows.Add(new DataRow(nums, type));
        }

        return rows;
    }
}