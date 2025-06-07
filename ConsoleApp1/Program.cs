string[] lines = File.ReadAllLines("training_data.csv");

foreach (string line in lines)
{
    string[] parts = line.Split(',');
    if (parts.Length < 5) continue;
}