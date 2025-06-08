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
    public List<DataRow> dataset = new List<DataRow>();
    public int neighbors;
    public string metric;

    public KNN(int k, string distance)
    {
        neighbors = k;
        metric = distance;
    }

    public void AddTrainingData(List<DataRow> data)
    {
        dataset = data;
    }

    public double CalculateDistance(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (metric == "euclidean")
            {
                sum += (a[i] - b[i]) * (a[i] - b[i]);
            }
            else if (metric == "manhattan")
            {
                sum += Math.Abs(a[i] - b[i]);
            }
        }

        if (metric == "euclidean")
        {
            return Math.Sqrt(sum);
        }
        else
        {
            return sum;
        }
    }

    public string Guess(double[] input)
    {
        List<Tuple<double, string>> dists = new List<Tuple<double, string>>();

        for (int i = 0; i < dataset.Count; i++)
        {
            var d = CalculateDistance(input, dataset[i].Numbers);
            dists.Add(new Tuple<double, string>(d, dataset[i].Category));
        }

        dists.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        Dictionary<string, int> counter = new Dictionary<string, int>();
        int k = neighbors;

        for (int x = 0; x < k; x++)
        {
            var name = dists[x].Item2;
            if (counter.ContainsKey(name))
            {
                counter[name]++;
            }
            else
            {
                counter[name] = 1;
            }
        }

        string most = "";
        int max = -1;

        foreach (var pair in counter)
        {
            if (pair.Value > max)
            {
                most = pair.Key;
                max = pair.Value;
            }
        }

        return most;
    }



}


class Program
{
    static void Main()
    {
        var loaded = ReadCsv("/home/mp/python_projects/knn/training_data.csv");
        KNN model = new KNN(3, "manhattan");
        model.AddTrainingData(loaded);

        double[] example = new double[] { 5.9, 3.0, 5.1, 1.8 };

        string answer = model.Guess(example);

        Console.WriteLine("Zakwalifikowano jako: " + answer);
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