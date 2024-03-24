namespace CsvKit;

internal static class Program
{
    private static void Main(string[] args)
    {
        var input = """
                    1;Herr;"";"Sigisbert";"Forst";"";"Lüdinghauser Straße";"10";"16348";"Marienwerder";"03337/75618287";"0155/7061212";"";"s1994@net-mail.none";"";"06.02.1997"
                    2;Frau;"";"Frida";"Graf";"";"Hegenerstraße";"15";"94530";"Auerbach";"09901/21213607";"";"";"f_graf@trashmail.none";"";"24.10.1947"
                    """;

        // var input = """
        //             1;Herr;"";"Sigisbert";"Forst";"";"Lüdinghauser Straße";"10";"16348";"Marienwerder";"03337/75618287";"0155/7061212";"";"s1994@net-mail.none";"";"06.02.1997"
        //             2;Frau;"";"Frida";"Graf";"";"Hegenerstraße";"15";"94530";"Auerbach";"09901/21213607";"";"";"f_graf@trashmail.none";"";"24.10.1947"
        //             3;Herr;"";"Heimbert";"Suttner";"";"Aremberger Straße";"166";"56288";"Bell";"06762/82766157";"0168/2838039";"";"h-suttner@live-mail.none";"ja";"20.10.1920"
        //             4;Frau;"";"Janne";"Magnus";"";"Reuschenlay";"28";"96152";"Burghaslach";"";"0176/6184183";"";"janne_magnus@goggle-mail.none";"";"01.09.1970"
        //             5;Frau;"";"Liesl";"Haumann";"";"Höhenweg";"18";"55595";"Burgsponheim";"0671/19135929";"0178/2704573";"";"liesl.1942@funmail.none";"";"01.03.1954"
        //             6;"Frau";"";"Kreszenz";"Biber";"";"Berghausen";"135";"23911";"Harmsdorf";"04541/8916555";"0174/9751676";"";"";"";"13.03.2024"
        //             7;"Frau";"";"Riana";"Brocks";"";"Stefanstraße";"107";"66507";"Reifenberg";"";"0175/6212673";"";"";"";"23.08.2004"
        //             8;"Frau";"";"Belina";"Bennemann";"";"Alte Heide";"101";"34277";"Fuldabrück";"0561/14383100";"0170/9644753";"";"";"";"18.06.1956"
        //             9;"Frau";"";"Linda";"Thompson";"23.01.1936";"Feldstiege";"127";"29553";"Bienenbüttel";"05823/81329392";"0155/8200551";"";"lindathompson@company.none";"";"11.10.2001"
        //             10;"Herr";"";"Arndt";"Neufeld";"02.04.2006";"Aumerich";"174";"94227";"Lindberg";"";"0163/9378864";"";"arndt-neufeld@email.none";"";"20.07.2007"
        //             """;
        //
        var parser = new CsvParser(FieldSeparators.Semicolon, LineSeparators.Unix);
        var result = parser.Run(input);
        
        foreach (var row in result) {
            foreach (var field in row) {
                Console.Write($"{field}\t");
            }
            Console.WriteLine();
        }
    }
}
