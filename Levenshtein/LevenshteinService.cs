using System.Collections.Generic;

namespace Levenshtein;

internal enum Op
{
    None,
    Zero,
    Shift,
    Add,
    Remove
}

file class SSToupleComparer : IEqualityComparer<(string, string, Op)>
{
    public bool Equals((string, string, Op) one, (string, string, Op) two)
    {
        return StringComparer.InvariantCultureIgnoreCase
                             .Equals(one.Item1, two.Item1)
               &&
               StringComparer.InvariantCultureIgnoreCase
                             .Equals(one.Item2, two.Item2);
    }

    public int GetHashCode((string, string, Op) item)
    {
        return StringComparer.InvariantCultureIgnoreCase
                             .GetHashCode(item.Item1)
               +
               StringComparer.InvariantCultureIgnoreCase
                             .GetHashCode(item.Item2);
    }
}

file class SSKeyToupleComparer : IEqualityComparer<(string, string)>
{
    public bool Equals((string, string) one, (string, string) two)
    {
        return StringComparer.InvariantCultureIgnoreCase
                             .Equals(one.Item1, two.Item1)
               &&
               StringComparer.InvariantCultureIgnoreCase
                             .Equals(one.Item2, two.Item2);
    }

    public int GetHashCode((string, string) item)
    {
        return StringComparer.InvariantCultureIgnoreCase
                             .GetHashCode(item.Item1)
               +
               StringComparer.InvariantCultureIgnoreCase
                             .GetHashCode(item.Item2);
    }
}

internal class LevenshteinService
{
    /// <summary>
    /// Recursive Distance
    /// </summary>
    public (int distance, int count) CalculateDistance(string str1, string str2)
    {
        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            throw new ArgumentException("string must contain at least 1 character");

        // Equal characters
        while (str1[0] == str2[0] && str1.Length > 1 && str2.Length > 1)
        {
            str1 = str1.Substring(1);
            str2 = str2.Substring(1);
        }

        var set = new HashSet<(string, string, Op)>(new SSToupleComparer());
        var lDist = Levenshtein(str1, str2, Math.Max(str1.Length, str2.Length), set, (Op.None, Op.None));
        var mn = str1.Length * str2.Length;
        return lDist;
    }

    internal (int distance, int count) Levenshtein(string str1, 
                                                   string str2, 
                                                   int threshold, 
                                                   HashSet<(string, string, Op)> set, 
                                                   (Op op1, Op op2) ops
                                                  )
    {
        (Op op1, Op op2) = ops;

        //if (set.Contains((str1, str2, op2)))
        //    throw new Exception();

        set.Add((str1, str2, op2));


        // Equal characters
        while (str1[0] == str2[0] && str1.Length > 1 && str2.Length > 1)
        {
            str1 = str1.Substring(1);
            str2 = str2.Substring(1);
            op1 = op2;
            op2 = Op.Zero;
        }

        // Specail Cases
        if (str1.Length == 1)
            return (OneChar(str1, str2), 1);

        if (str2.Length == 1)
            return (OneChar(str2, str1), 1);

        var op = op2;

        if(op == Op.Zero)
            return Levenshtein(str1, str2, threshold, set, (Op.None, Op.None));

        var count = 0;

        // Shift and modify
        //var res1 = Levenshtein(str1.Substring(1), str2.Substring(1), threshold - 1, set, (op2, Op.Shift));
        var res1 = Shift(str1.Substring(1), str2.Substring(1), set);
        res1.distance++;
        count += res1.count + 1;
        if (res1.distance < threshold)
            threshold = res1.distance;

        if (threshold <= 1)
            return (threshold, count);


        // Add
        if (op != Op.Zero && op != Op.Shift && op != Op.Remove)
        {
            var addCost = str1.Length >= str2.Length ? (str1.Length - str2.Length) + 2 : (str2.Length - str1.Length);
            if (addCost < threshold)
            {
                var res2 = Levenshtein(str1, str2.Substring(1), threshold - 1, set, (op2, Op.Add));
                res2.distance++;
                count += res2.count + 1;

                if (res2.distance < threshold)
                    threshold = res2.distance;

                if (threshold <= 1)
                    return (threshold, count);
            }
        }


        // Remove
        if (op != Op.Zero && op != Op.Shift && op != Op.Add)
        {
            var removeCost = str1.Length > str2.Length ? (str1.Length - str2.Length) : (str2.Length - str1.Length) + 2;
            if(removeCost < threshold)
            {
                var res3 = Levenshtein(str1.Substring(1), str2, threshold - 1, set, (op2, Op.Remove));
                res3.distance++;
                count += res3.count + 1;

                if (res3.distance < threshold)
                    threshold = res3.distance;
            }
        }


        return (threshold, count);
    }

    internal (int distance, int count) Shift(string str1, string str2, HashSet<(string, string, Op)> set)
    {

        set.Add((str1, str2, Op.None));

        var shortString = str1;
        var longString = str2;
        if(shortString.Length > longString.Length)
        {
            shortString = str2;
            longString = str1;
        }

        var dist = 0;
        var threshold = longString.Length;

        for (int i = 0; i < shortString.Length; i++)
        {
            var shortC = shortString[i];
            var longC = longString[i];
            if (shortC == longC)
            {
                shortString = shortString.Substring(i+1);
                longString = longString.Substring(i+1);
                if (shortString.Length < 1) 
                    return (i + longString.Length, 0);

                var res = Levenshtein(shortString, longString, threshold, set, (Op.None, Op.None));
                return (res.distance + dist, res.count);
            }
            dist++;
        }

        return (longString.Length, 0);
    }


    public (int distance, int count) CalculateDistanceNoOptimisation(string str1, string str2)
    {
        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            throw new ArgumentException("string must contain at least 1 character");

        var lDist = LevenshteinNoOptimisation(str1, str2, Math.Max(str1.Length, str2.Length));
        var mn = str1.Length * str2.Length;
        return lDist;

    }

    internal (int distance, int count) LevenshteinNoOptimisation(string str1, string str2, int threshold)
    {

        // Equal characters
        while (str1[0] == str2[0] && str1.Length > 1 && str2.Length > 1)
        {
            str1 = str1.Substring(1);
            str2 = str2.Substring(1);
        }

        // Specail Cases
        if (str1.Length == 1)
            return (OneChar(str1, str2), 1);

        if (str2.Length == 1)
            return (OneChar(str2, str1), 1);

        var count = 0;

        // Shift and modify
        var res1 = LevenshteinNoOptimisation(str1.Substring(1), str2.Substring(1), threshold - 1);
        res1.distance++;
        count += res1.count + 1;
        if (res1.distance < threshold)
            threshold = res1.distance;

        if (threshold <= 1)
            return (threshold, count);


        // Add
        var res2 = LevenshteinNoOptimisation(str1, str2.Substring(1), threshold - 1);
        res2.distance++;
        count += res2.count + 1;

        if (res2.distance < threshold)
            threshold = res2.distance;

        if (threshold <= 1)
            return (threshold, count);


        // Remove
        var res3 = LevenshteinNoOptimisation(str1.Substring(1), str2, threshold - 1);
        res3.distance++;
        count += res3.count + 1;

        if (res3.distance < threshold)
            threshold = res3.distance;


        return (threshold, count);
    }


    internal int LevenshteinDP(string str1, string str2, IDictionary<(string, string), int> dict)
    {
        if(dict.ContainsKey((str1, str2)))
            return dict[(str1, str2)];

        // Specail Cases
        if (str1.Length == 1)
        {
            var dist1c = OneChar(str1, str2);
            dict.Add((str1, str2), dist1c);
            return dist1c;
        }

        if (str2.Length == 1)
        {
            var dist1c = OneChar(str2, str1);
            dict.Add((str1, str2), dist1c);
            return dist1c;
        }

        var dist = int.MaxValue;

        // Zero shft
        if (str1[0] == str2[0])
        {
            var dist0 = LevenshteinDP(str1.Substring(1), str2.Substring(1), dict);
            dist = Math.Min(dist, dist0);
        }

        // Shift and modify
        var dist1 = LevenshteinDP(str1.Substring(1), str2.Substring(1), dict);
        dist1++;
        dist = Math.Min(dist, dist1);

        // Add
        var dist2 = LevenshteinDP(str1, str2.Substring(1), dict);
        dist2++;
        dist = Math.Min(dist, dist2);

        // Remove
        var dist3 = LevenshteinDP(str1.Substring(1), str2, dict);
        dist3++;
        dist = Math.Min(dist, dist3);

        dict.Add((str1, str2), dist);

        return dist;
    }

    public (int distance, int count) CalculateDistanceDP(string str1, string str2)
    {
        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            throw new ArgumentException("string must contain at least 1 character");

        var dict = new Dictionary<(string, string), int>(new SSKeyToupleComparer());

        var dist = LevenshteinDP(str1, str2, dict);
        return (dist, dict.Count);

    }

    internal int OneChar(string str1, string str2)
    {
        if (str1.Length != 1) throw new Exception();

        if (str2.Length == 1)
            return str1.First() == str2.First() ? 0 : 1;

        if (str1.First() == str2.First())
            return str2.Length - 1;

        return 1 + OneChar(str1, str2.Substring(1));
    }

}
