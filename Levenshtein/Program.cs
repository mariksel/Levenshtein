
using Levenshtein;

var service = new LevenshteinService();

int Levenshtein(string str1, string str2) => service.CalculateDistance(str1, str2).distance;



//Console.WriteLine(Levenshtein("a", "abc") + " -2");
//Console.WriteLine(Levenshtein("a", "babc") + " -3");
//Console.WriteLine(Levenshtein("a", "baa") + " -2");
//Console.WriteLine(Levenshtein("a", "bba") + " -2");
//Console.WriteLine(Levenshtein("abc", "abc") + " -0");
//Console.WriteLine(Levenshtein("abc", "yabd") + " -2");
//Console.WriteLine(Levenshtein("aaa", "abc") + " -2");
//Console.WriteLine(Levenshtein("abcd", "abc") + " -1");
//Console.WriteLine(Levenshtein("asjsaaaa", "aaaasdsj") + " -7");
//Console.WriteLine(Levenshtein("casjsaaaa", "aaaasdsj") + " -7");
//Console.WriteLine(Levenshtein("aasjsaaaa", "aaaasdsj") + " -6");
//Console.WriteLine(Levenshtein("0bc01abcef31", "abcdefg0121") + " -9");
//Console.WriteLine(Levenshtein("a", "ssaddd") + " -5");
//Console.WriteLine(Levenshtein("ao", "ssaddd") + " -5");
//Console.WriteLine(Levenshtein("lv", "dvd") + " -2");
Console.WriteLine(Levenshtein("linl", "didnl") + " -2");
Console.WriteLine(Levenshtein("linvl", "didnvl") + " -2");
Console.WriteLine(Levenshtein("lwinvl", "dwidnvl") + " -2");
Console.WriteLine(Levenshtein("lwinvl", "dwidddnvl") + " -4");
Console.WriteLine(Levenshtein("lwinvl", "ssdwidddnvl") + " -6");
Console.WriteLine(Levenshtein("aaalwinvl", "ssaaadwidddnvl") + " -6");
Console.WriteLine(Levenshtein("aaaaasdfxclvlvnlnowinvl", "aassaaasdfxclvdvnlnowidddnvl") + " -6");
Console.WriteLine(Levenshtein("sdlfjjlxaaaaasdfxclvlvnlnowinvlknaoljlsdfvjljl", "sdlfjjlxaassaaasdfxclvdvnlnowidddnvlknaoljlsdfvjljl") + " -6");
Console.WriteLine(Levenshtein(
                                "kdondfikofgdpofjkldfgofdgretjkldfgkl",
                                "gdsdfondfikofgdpsdfofjkldfgofdgretjkldfgksdfl")
                                + " -10"
                             );
Console.WriteLine(Levenshtein(
                                "fof",
                                "ofsfof")
                                + " -3"
                             );
Console.WriteLine(Levenshtein(
                                "slfjjaov38sg3409vgmhge8jvkjklfdlkdfoimsdfvklnmsdfglpognsuioeifcioioidondfikofgdpofjkldfgofdgretjkldfgklflmkdffsmkbdfglk",
                                "slfjjaov38sg3sdf409vgmhge8dsfgjvkjklfdlkdfoimsdfvklnmsdfglpogsdfnsuioeifcioioidondfikofgdpsdfofjkldfgofdgretjkldfgksdflflmkdffsmkbdfglk")
                                + " -16"
                             );
Console.WriteLine(Levenshtein(
                                "ddedod",
                                "eio")
                                + " -4"
                             );
Console.WriteLine(Levenshtein(
                                "sdjfllsknvedpvmvmdfmv58i95498fdojibmim9ombmddfjsdfjkl3gi489jr59dd334589dfbniodfbijo3490disdrfvoklpdclnjsddhsdcui378ybhuschuiduhd7xcvbnjkdsfuioh34rwehnudjiod",
                                "sdjfllsknvepvmvmdfmv58i95498bmim9ombmddfjsdfjkl3gi489jr59334589dfbniodfbijo3490isdrfvoklpdclnjsdhsdcui378ybhuschuiduh7xcvbnjkdsfuioh34rwehnujio")
                                + " -13"
                             );





