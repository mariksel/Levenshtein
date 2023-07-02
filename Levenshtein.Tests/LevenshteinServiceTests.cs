using FsCheck;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Levenshtein.Tests;

public class LevenshteinServiceTests
{
    private readonly ITestOutputHelper _testOutput;
    private readonly LevenshteinService _service;

    public LevenshteinServiceTests(ITestOutputHelper testOutput)
    {
        _service = new LevenshteinService();
        _testOutput = testOutput;
    }


    [Fact]
    public void CalculateDistance_EmptyStr1AnsStr2_CorrectDistance()
    {
        Assert.Throws<ArgumentException>(() => _service.CalculateDistance("", ""));
    }

    [Theory]
    [InlineData("aaa", "bb", 3)]
    [InlineData("axa4", "bt4", 3)]
    public void CalculateDistance_NoZeroShifts(string str1, string str2, int expectedDistance)
    {
        var res = _service.CalculateDistance(str1, str2);

        _testOutput.WriteLine($"mn: {str1.Length * str2.Length} - Count: {res.count}");
        Assert.Equal(expectedDistance, res.distance);
    }

    [Theory]
    [InlineData("aFGF", "GF", 2)]
    [InlineData("ata4", "bt4", 2)]
    [InlineData("anaxa", "nxa", 2)]
    [InlineData("yaga", "yga", 1)]
    [InlineData("a", "abc", 2)]
    [InlineData("a", "babc", 3)]
    [InlineData("a", "baa", 2)]
    [InlineData("a", "bba", 2)]
    [InlineData("abc", "abc", 0)]
    [InlineData("abc", "yabd", 2)]
    [InlineData("aaa", "abc", 2)]
    [InlineData("abcd", "abc", 1)]
    [InlineData("asjsaaaa", "aaaasdsj", 7)]
    [InlineData("casjsaaaa", "aaaasdsj", 7)]
    [InlineData("aasjsaaaa", "aaaasdsj", 6)]
    [InlineData("0bc01abcef31", "abcdefg0121", 9)]
    [InlineData("a", "ssaddd", 5)]
    [InlineData("ao", "ssaddd", 5)]
    [InlineData("lv", "dvd", 2)]
    [InlineData("linl", "didnl", 2)]
    [InlineData("linvl", "didnvl", 2)]
    [InlineData("lwinvl", "dwidnvl", 2)]
    [InlineData("lwinvl", "dwidddnvl", 4)]
    [InlineData("lwinvl", "ssdwidddnvl", 6)]
    [InlineData("aaalwinvl", "ssaaadwidddnvl", 6)]
    [InlineData("ddedod", "eio", 4)]
    [InlineData("fof", "ofsfof", 3)]
    [InlineData("aaaaasdfxclvlvnlnowinvl", "aassaaasdfxclvdvnlnowidddnvl", 6)]
    public void CalculateDistance_Samples(string str1, string str2, int expectedDistance)
    {
        var res = _service.CalculateDistance(str1, str2);

        _testOutput.WriteLine($"mn: {str1.Length * str2.Length} - Count: {res.count}");
        Assert.Equal(expectedDistance, res.distance);
    }

    [Theory]
    [InlineData("sdlfjjlxaaaaasdfxclvlvnlnowinvlknaoljlsdfvjljl", "sdlfjjlxaassaaasdfxclvdvnlnowidddnvlknaoljlsdfvjljl", 6)]
    [InlineData("kdondfikofgdpofjkldfgofdgretjkldfgkl", "gdsdfondfikofgdpsdfofjkldfgofdgretjkldfgksdfl", 10)]
    [InlineData(
        "slfjjaov38sg3409vgmhge8jvkjklfdlkdfoimsdfvklnmsdfglpognsuioeifcioioidondfikofgdpofjkldfgofdgretjkldfgklflmkdffsmkbdfglk",
        "slfjjaov38sg3sdf409vgmhge8dsfgjvkjklfdlkdfoimsdfvklnmsdfglpogsdfnsuioeifcioioidondfikofgdpsdfofjkldfgofdgretjkldfgksdflflmkdffsmkbdfglk",
        16)]
    [InlineData(
        "sdjfllsknvedpvmvmdfmv58i95498fdojibmim9ombmddfjsdfjkl3gi489jr59dd334589dfbniodfbijo3490disdrfvoklpdclnjsddhsdcui378ybhuschuiduhd7xcvbnjkdsfuioh34rwehnudjiod",
        "sdjfllsknvepvmvmdfmv58i95498bmim9ombmddfjsdfjkl3gi489jr59334589dfbniodfbijo3490isdrfvoklpdclnjsdhsdcui378ybhuschuiduh7xcvbnjkdsfuioh34rwehnujio",
        13)]
    public void CalculateDistance_LargeSamples(string str1, string str2, int expectedDistance)
    {
        var res = _service.CalculateDistance(str1, str2);

        _testOutput.WriteLine($"mn: {str1.Length * str2.Length} - Count: {res.count}");
        Assert.Equal(expectedDistance, res.distance);
    }

    [Theory]
    [InlineData("ata4", "bt4", 2)]
    [InlineData("anaxa", "nxa", 2)]
    [InlineData("yaga", "yga", 1)]
    [InlineData("a", "abc", 2)]
    [InlineData("a", "babc", 3)]
    [InlineData("a", "baa", 2)]
    [InlineData("a", "bba", 2)]
    [InlineData("abc", "abc", 0)]
    [InlineData("abc", "yabd", 2)]
    [InlineData("aaa", "abc", 2)]
    [InlineData("abcd", "abc", 1)]
    [InlineData("asjsaaaa", "aaaasdsj", 7)]
    [InlineData("casjsaaaa", "aaaasdsj", 7)]
    [InlineData("aasjsaaaa", "aaaasdsj", 6)]
    [InlineData("0bc01abcef31", "abcdefg0121", 9)]
    [InlineData("a", "ssaddd", 5)]
    [InlineData("ao", "ssaddd", 5)]
    [InlineData("lv", "dvd", 2)]
    [InlineData("linl", "didnl", 2)]
    [InlineData("linvl", "didnvl", 2)]
    [InlineData("lwinvl", "dwidnvl", 2)]
    [InlineData("lwinvl", "dwidddnvl", 4)]
    [InlineData("lwinvl", "ssdwidddnvl", 6)]
    [InlineData("aaalwinvl", "ssaaadwidddnvl", 6)]
    [InlineData("aaaaasdfxclvlvnlnowinvl", "aassaaasdfxclvdvnlnowidddnvl", 6)]
    [InlineData("sdlfjjlxaaaaasdfxclvlvnlnowinvlknaoljlsdfvjljl", "sdlfjjlxaassaaasdfxclvdvnlnowidddnvlknaoljlsdfvjljl", 6)]
    [InlineData("kdondfikofgdpofjkldfgofdgretjkldfgkl", "gdsdfondfikofgdpsdfofjkldfgofdgretjkldfgksdfl", 10)]
    [InlineData("fof", "ofsfof", 3)]
    [InlineData(
        "slfjjaov38sg3409vgmhge8jvkjklfdlkdfoimsdfvklnmsdfglpognsuioeifcioioidondfikofgdpofjkldfgofdgretjkldfgklflmkdffsmkbdfglk",
        "slfjjaov38sg3sdf409vgmhge8dsfgjvkjklfdlkdfoimsdfvklnmsdfglpogsdfnsuioeifcioioidondfikofgdpsdfofjkldfgofdgretjkldfgksdflflmkdffsmkbdfglk",
        16)]
    //[InlineData("ddedod", "eio", 4)]
    //[InlineData(
    //    "sdjfllsknvedpvmvmdfmv58i95498fdojibmim9ombmddfjsdfjkl3gi489jr59dd334589dfbniodfbijo3490disdrfvoklpdclnjsddhsdcui378ybhuschuiduhd7xcvbnjkdsfuioh34rwehnudjiod",
    //    "sdjfllsknvepvmvmdfmv58i95498bmim9ombmddfjsdfjkl3gi489jr59334589dfbniodfbijo3490isdrfvoklpdclnjsdhsdcui378ybhuschuiduh7xcvbnjkdsfuioh34rwehnujio",
    //    13)]
    public void CalculateDistanceNoOptimisation_Samples(string str1, string str2, int expectedDistance)
    {
        var res = _service.CalculateDistanceNoOptimisation(str1, str2);

        _testOutput.WriteLine($"mn: {str1.Length * str2.Length} - Count: {res.count}");
        Assert.Equal(expectedDistance, res.distance);
    }

    [Theory]
    [InlineData("ata4", "bt4", 2)]
    [InlineData("anaxa", "nxa", 2)]
    [InlineData("yaga", "yga", 1)]
    [InlineData("a", "abc", 2)]
    [InlineData("a", "babc", 3)]
    [InlineData("a", "baa", 2)]
    [InlineData("a", "bba", 2)]
    [InlineData("abc", "abc", 0)]
    [InlineData("abc", "yabd", 2)]
    [InlineData("aaa", "abc", 2)]
    [InlineData("abcd", "abc", 1)]
    [InlineData("asjsaaaa", "aaaasdsj", 7)]
    [InlineData("casjsaaaa", "aaaasdsj", 7)]
    [InlineData("aasjsaaaa", "aaaasdsj", 6)]
    [InlineData("0bc01abcef31", "abcdefg0121", 9)]
    [InlineData("a", "ssaddd", 5)]
    [InlineData("ao", "ssaddd", 5)]
    [InlineData("lv", "dvd", 2)]
    [InlineData("linl", "didnl", 2)]
    [InlineData("linvl", "didnvl", 2)]
    [InlineData("lwinvl", "dwidnvl", 2)]
    [InlineData("lwinvl", "dwidddnvl", 4)]
    [InlineData("lwinvl", "ssdwidddnvl", 6)]
    [InlineData("aaalwinvl", "ssaaadwidddnvl", 6)]
    [InlineData("aaaaasdfxclvlvnlnowinvl", "aassaaasdfxclvdvnlnowidddnvl", 6)]
    [InlineData("sdlfjjlxaaaaasdfxclvlvnlnowinvlknaoljlsdfvjljl", "sdlfjjlxaassaaasdfxclvdvnlnowidddnvlknaoljlsdfvjljl", 6)]
    [InlineData("kdondfikofgdpofjkldfgofdgretjkldfgkl", "gdsdfondfikofgdpsdfofjkldfgofdgretjkldfgksdfl", 10)]
    [InlineData("fof", "ofsfof", 3)]
    [InlineData(
        "slfjjaov38sg3409vgmhge8jvkjklfdlkdfoimsdfvklnmsdfglpognsuioeifcioioidondfikofgdpofjkldfgofdgretjkldfgklflmkdffsmkbdfglk",
        "slfjjaov38sg3sdf409vgmhge8dsfgjvkjklfdlkdfoimsdfvklnmsdfglpogsdfnsuioeifcioioidondfikofgdpsdfofjkldfgofdgretjkldfgksdflflmkdffsmkbdfglk",
        16)]
    [InlineData("ddedod", "eio", 4)]
    [InlineData(
        "sdjfllsknvedpvmvmdfmv58i95498fdojibmim9ombmddfjsdfjkl3gi489jr59dd334589dfbniodfbijo3490disdrfvoklpdclnjsddhsdcui378ybhuschuiduhd7xcvbnjkdsfuioh34rwehnudjiod",
        "sdjfllsknvepvmvmdfmv58i95498bmim9ombmddfjsdfjkl3gi489jr59334589dfbniodfbijo3490isdrfvoklpdclnjsdhsdcui378ybhuschuiduh7xcvbnjkdsfuioh34rwehnujio",
        13)]
    public void CalculateDistanceDP_Samples(string str1, string str2, int expectedDistance)
    {
        var res = _service.CalculateDistanceDP(str1, str2);

        _testOutput.WriteLine($"mn: {str1.Length * str2.Length} - Count: {res.count}");
        Assert.Equal(expectedDistance, res.distance);
    }

    [Fact]
    public void Case1()
    {
        var (str1, str2) = ("xAxx", "AAAbbbb");
        var expectedDistance = 6;

        var res = _service.CalculateDistance(str1, str2);

        _testOutput.WriteLine($"mn: {str1.Length * str2.Length} - Count: {res.count}");
        Assert.Equal(expectedDistance, res.distance);
    }

    [Fact]
    public void Propery()
    {
        var gen = GenStringTouples();
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        Prop.ForAll<string>(s => true).Check(new Configuration() { MaxNbOfTest = 1000, QuietOnSuccess = false });

        Prop.ForAll<Tuple<string, string>>(arb, s =>
            {
                var dict = _service.CalculateDistance(s.Item1, s.Item2);

                var lengthDiff = Math.Abs(s.Item1.Length - s.Item2.Length);
                Assert.False(lengthDiff > dict.distance, $"distance: {dict.distance} was smaller than lengthDiff: {lengthDiff}");

            })
            .VerboseCheckThrowOnFailure();

    }

    [Fact]
    public void CalculateDistance_Performance_Propery()
    {
        var gen = GenStringTouples(6);
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        var config = Configuration.VerboseThrowOnFailure;
        config.MaxNbOfTest = 1_000;
        config.QuietOnSuccess = false;

        var count = 0;
        Prop.ForAll<Tuple<string, string>>(arb, s =>
            {
                count++;
                var dict = _service.CalculateDistance(s.Item1, s.Item2);
                var dictDP = _service.CalculateDistanceDP(s.Item1, s.Item2);

                Assert.True(dictDP.count >= dict.count, $"Distance DP: {dictDP.count} was smaller than Distance: {dict.count} ({count})");
            })
            .Check(config);

        _testOutput.WriteLine($"performed {count} tests");
    }

    [Fact]
    public void CalculateDistanceDP_ResultPlot()
    {
        var gen = GenStringTouples(20).Where(t => t.Item1.Length == 5);
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        var config = Configuration.VerboseThrowOnFailure;
        config.MaxNbOfTest = 1_000;
        config.QuietOnSuccess = false;

        var count = 0;
        var results = new List<(int n, int count, int mn)>();
        Prop.ForAll<Tuple<string, string>>(arb, s =>
            {
                count++;
                var res = _service.CalculateDistanceDP(s.Item1, s.Item2);

                results.Add((s.Item2.Length, res.count, s.Item2.Length * s.Item1.Length));
            })
            .Check(config);

        _testOutput.WriteLine($"performed {count} tests");
    }

    [Fact]
    public void CalculateDistanceNoOptimisation_Propery()
    {
        var gen = GenStringTouples(10);
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        Prop.ForAll<string>(s => true).Check(new Configuration() { MaxNbOfTest = 100, QuietOnSuccess = false });

        Prop.ForAll<Tuple<string, string>>(arb, s =>
            {
                var dict = _service.CalculateDistanceNoOptimisation(s.Item1, s.Item2);

                var lengthDiff = Math.Abs(s.Item1.Length - s.Item2.Length);
                Assert.False(lengthDiff > dict.distance, $"distance: {dict.distance} was smaller than lengthDiff: {lengthDiff}");
            })
            .VerboseCheckThrowOnFailure();
    }

    [Fact]
    public void CalculateDistance_PropertyCompare_LevenshteinNoOptimisation()
    {
        var gen = GenStringTouples(15);
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        Prop.ForAll<string>(s => true).Check(new Configuration() { MaxNbOfTest = 10000, QuietOnSuccess = false });

        Prop.ForAll<Tuple<string, string>>(arb, s =>
            {
                var dict = _service.CalculateDistance(s.Item1, s.Item2);

                var dist2 = _service.CalculateDistanceNoOptimisation(s.Item1, s.Item2);

                Assert.Equal(dist2.distance, dict.distance);
            })
            .VerboseCheckThrowOnFailure();
    }

    [Fact]
    public void CalculateDistanceDP_PropertyCompare_LevenshteinNoOptimisation()
    {
        var gen = GenStringTouples(15);
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        var config = Configuration.VerboseThrowOnFailure;
        config.MaxNbOfTest = 1000;
        config.QuietOnSuccess = false;

        var count = 0;
        Prop.ForAll<Tuple<string, string>>(arb, s =>
        {
            var dict = _service.CalculateDistanceDP(s.Item1, s.Item2);

            var dist2 = _service.CalculateDistanceNoOptimisation(s.Item1, s.Item2);

            count++;
            Assert.Equal(dist2.distance, dict.distance);
        })
        .Check(config);

        _testOutput.WriteLine($"performed {count} tests");
    }

    [Fact]
    public void CalculateDistance_PropertyCompare_CalculateDistanceDP()
    {
        var gen = GenStringTouples(15);
        var arb = Arb.From(gen, x => Arb.Shrink(x).Where(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2)));

        var config = Configuration.VerboseThrowOnFailure;
        config.MaxNbOfTest = 1000;
        config.QuietOnSuccess = false;

        var count = 0;
        Prop.ForAll<Tuple<string, string>>(arb, s =>
        {
            var dict = _service.CalculateDistanceDP(s.Item1, s.Item2);

            var dist2 = _service.CalculateDistance(s.Item1, s.Item2);

            count++;
            Assert.Equal(dist2.distance, dict.distance);
        })
        .Check(config);

        _testOutput.WriteLine($"performed {count} tests");
    }


    static Gen<Tuple<string, string>> GenStringTouples(int strLength = 100)
    {
        var strings = Arb.Generate<string>();
        //char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();
        //char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] chars = "ABCD".ToCharArray();
        var randomCharsGen = Gen.Choose(0, chars.Length - 1)
                                .Select(i => chars[i]);

        var simpleStringsGen = Gen.Choose(1, strLength)
                                  .SelectMany(n => Gen.ListOf(n, randomCharsGen)
                                                      .Select(cs => new string(cs.ToArray()))
                                  );

        var toupleGen = Gen.Two(simpleStringsGen);
        //var x = toupleGen.Sample(1, 1000);
        return toupleGen;
    }

}