using LaChose.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LaChose
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
                Debug.Listeners.Add(myWriter);

                Debug.WriteLine("Starting ...");

                //EnigmeD();

                Mendeleiev();

                Debug.WriteLine("End");
                Console.Read();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error " + ex.Message);
                Console.Read();
            }
        }

        public static void EnigmeD()
        {
            string pathIn = @"C:\Yann\temp\indata2.tsv";
            string pathOut = @"C:\Yann\temp\out.csv";

            var lines = File.ReadAllLines(pathIn, Encoding.UTF8);
            var csv = lines.Where(x => x.Contains("1972")).Select(row => string.Join(";", row.Split('\t')));


            File.WriteAllLines(pathOut, csv, Encoding.UTF8);

        }

        #region Enigma A

        public static void EnigmeA()
        {
            //Test
            //List<int> decallages1 = new List<int>() { -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11, -12, -13, -14, -15, -16, -17, -18 };
            //string test = "TCLRYPLIWNFSNFS";
            //string result = CesarProgressif(test, decallages1);
            //Debug.WriteLine("Result : " + result);

            List<string> inputs = new List<string>() { "TCLRYPLIWNFSNFS", "KCMQTYGNVBPM", "WXVAVJPOKFR", "SFEUYGWSYK", "MNANPOGT" };

            //List<string> inputs = new List<string>() { "WXVAVJPOKFR" };

            List<List<int>> decallageList = GetDecallages();


            foreach (string input in inputs)
            {
                Debug.WriteLine("");
                Debug.WriteLine("Input : " + input);

                //Debug.WriteLine(" ");
                foreach (var dec in decallageList)
                {
                    string decallageDisplay = dec.Aggregate<int, string>(String.Empty, (x, y) => (x.Length > 0 ? x + "," : x) + y.ToString());
                    //Debug.Write(decallageDisplay + "  ");

                    string result = CesarProgressif(input, dec);

                    char[] charArray = result.ToCharArray();
                    Array.Reverse(charArray);
                    string resultReverse = new string(charArray);

                    //Debug.Write(result + "  ");
                    Debug.Write(result + "  \t\t " + decallageDisplay + "\r\n");
                    //Debug.Write(result + " / " + resultReverse + "  \t\t " + decallageDisplay + "\r\n");

                }
                Debug.WriteLine(" ");
            }

        }

        public static string CesarProgressif(string input, List<int> decallages)
        {
            string strAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alpha = strAlphabet.ToCharArray();

            List<char> inputCharList = input.ToList<char>();

            if (decallages.Count < input.Length)
                Debug.WriteLine("Warning, pas assez de décallages");

            string result = "";
            int indexDecallage = 0;
            foreach (char c in inputCharList)
            {
                if (indexDecallage >= input.Length)
                    break;

                int originalIndex = strAlphabet.IndexOf(c);
                int decallage = decallages[indexDecallage];
                int alphaPos = originalIndex + decallage;

                //int originalAlphaPos = alphaPos;

                //Debug.WriteLine("originalIndex : " + originalIndex + " decallage : " + decallage + " alphaPos : " + alphaPos);

                if (alphaPos > 0 && alphaPos < 26)
                {
                    alphaPos = alphaPos + 1;

                    if (alphaPos == 26)
                        alphaPos = 0;
                }
                else
                {
                    while (alphaPos < 0)
                        alphaPos = alphaPos + 25;

                    while (alphaPos > 25)
                        alphaPos = alphaPos - 25;
                }

                char cDecoded = strAlphabet[alphaPos];

                //Debug.WriteLine( c + " --> "  + cDecoded);

                result += cDecoded;

                indexDecallage++;
            }


            return result;
        }

        public static List<List<int>> GetDecallages()
        {
            List<List<int>> decallageList = new List<List<int>>();

            //decallageList.Add(new List<int>() { -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11, -12, -13, -14, -15, -16, -17, -18 });
            //decallageList.Add(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });

            //decallageList.Add(new List<int>() { -1, 2, -3, 4, -5, 6, -7, 8, -9, 10, -11, 12, -13, 14, -15, 16, -17, 18 });
            //decallageList.Add(new List<int>() { 1, -2, 3, -4, 5, -6, 7, -8, 9, -10, 11, -12, 13, -14, 15, -16, 17, -18 });

            //decallageList.Add(new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            //decallageList.Add(new List<int>() { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            //decallageList.Add(new List<int>() { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });

            //decallageList.Add(new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 });
            //decallageList.Add(new List<int>() { 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11, -12, -13, -14, -15, -16, -17, -18 });
            //decallageList.Add(new List<int>() { 0, -1, 2, -3, 4, -5, 6, -7, 8, -9, 10, -11, 12, -13, 14, -15, 16, -17, 18 });
            //decallageList.Add(new List<int>() { 0, 1, -2, 3, -4, 5, -6, 7, -8, 9, -10, 11, -12, 13, -14, 15, -16, 17, -18 });

            //decallageList.Add(new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31 });
            //decallageList.Add(new List<int>() { -1, -3, -5, -7, -9, -11, -13, -15, -17, -19, -21, -23, -25, -27, -29, -31 });

            //decallageList.Add(new List<int>() { -1, 3, -5, 7, -9, 11, -13, 15, -17, 19, -21, 23, -25, 27, -29, 31 });
            //decallageList.Add(new List<int>() { 1, -3, 5, -7, 9, -11, 13, -15, 17, -19, 21, -23, 25, -27, 29, -31 });

            //decallageList.Add(new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10 });
            //decallageList.Add(new List<int>() { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, });

            //decallageList.Add(new List<int>() { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, });

            //decallageList.Add(new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89 });

            //decallageList.Add(new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 });


            decallageList.AddRange(AddSimpleCeasarProgressifDecallages());
            decallageList.AddRange(AddSimpleCeasarProgressifDecallagesReverse());

            return decallageList;

        }

        public static List<List<int>> AddSimpleCeasarProgressifDecallages()
        {
            List<List<int>> decallageList = new List<List<int>>();

            List<int> baseDecalage = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            decallageList.Add(baseDecalage);

            for (int i = 1; i <= 25; i++)
            {
                decallageList.Add(baseDecalage.Select(x => x + i).ToList());
            }

            for (int i = 1; i <= 25; i++)
            {
                decallageList.Add(baseDecalage.Select(x => x - i).ToList());
            }

            return decallageList;

        }

        public static List<List<int>> AddSimpleCeasarProgressifDecallagesReverse()
        {
            List<List<int>> decallageList = new List<List<int>>();

            List<int> baseDecalage = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            baseDecalage = baseDecalage.OrderByDescending(x => x).ToList();

            decallageList.Add(baseDecalage);

            for (int i = 1; i <= 25; i++)
            {
                decallageList.Add(baseDecalage.Select(x => x + i).ToList());
            }

            for (int i = 1; i <= 25; i++)
            {
                decallageList.Add(baseDecalage.Select(x => x - i).ToList());
            }

            return decallageList;

        }

        #endregion


        #region Tableau périodique des éléments

        public static void Mendeleiev()
        {
            //CsvToJson();
            //XmlToJson();

            //Play();

            //Affichage();

            //DicoParse();

            SearchWordInChimic();
        }

        public static void Play()
        {
            string txt = System.IO.File.ReadAllText(@"C:\Yann\projets\LaChose\LaChose\resources\atomes.json", Encoding.UTF8);
            List<Atome> atomes = JsonConvert.DeserializeObject<List<Atome>>(txt);

            List<string> sequences = new List<string>() { "57", "75", "84", "07", "34", "99", "43", "57", "16", "14", "09", "53", "20", "22", "08", "07" };

            foreach (string s in sequences)
            {
                Atome a = atomes.FirstOrDefault(x => Convert.ToInt32(x.numero) == Convert.ToInt32(s));

                if (a != null)
                    Debug.Write(a.symbole);
            }

            Debug.WriteLine("");
        }

        public static void Affichage()
        {
            string txt = System.IO.File.ReadAllText(@"C:\Yann\projets\LaChose\LaChose\resources\atomes.json", Encoding.UTF8);
            List<Atome> atomes = JsonConvert.DeserializeObject<List<Atome>>(txt);

            //foreach (Atome a in atomes.OrderBy(x => x.decouverte_annee))
            //{
            //    Debug.WriteLine(a.nom + " " + a.symbole + " " + a.decouverte_annee + " ");
            //}

            foreach (Atome a in atomes.OrderBy(x => x.symbole))
            {
                Debug.Write(a.symbole + " ");
            }




        }

        public static void SearchWordInChimic()
        {
            string atomJsonTxt = System.IO.File.ReadAllText(@"C:\Yann\projets\LaChose\LaChose\resources\atomes.json", Encoding.UTF8);
            List<Atome> atomes = JsonConvert.DeserializeObject<List<Atome>>(atomJsonTxt);

            List<string> results = new List<string>();
            var dicoLines = File.ReadAllLines(@"C:\Yann\projets\LaChose\LaChose\resources\dela-fr-public.txt", Encoding.UTF8);

            foreach (string word in dicoLines)
            {
                string workingWord = word;
                List<Atome> atomsNeeded = new List<Atome>();

                while (workingWord.Length > 0)
                {
                    //search 1 letter
                    string searchLetter = workingWord[0].ToString();

                    Atome a = atomes.FirstOrDefault(x => x.symbole.ToLower() == searchLetter.ToLower());

                    if (a != null)
                    {
                        atomsNeeded.Add(a);

                        if (workingWord.Length == 1)
                        {
                            //Mot complet
                            string info = word + " \t " + string.Join(" ", atomsNeeded.Select(x => x.symbole).ToArray()) + " \t " + string.Join(" ", atomsNeeded.Select(x => x.numero).ToArray());
                            Debug.WriteLine(info);
                            results.Add(info);
                            workingWord = ""; //on sort du while et on passe au suivant
                           
                        }
                        else
                            workingWord = workingWord.Substring(1, workingWord.Length - 1);
                    }
                    else
                    {
                        //ya pas de résultats en 1 lettre, on tente en 2 lettres
                                                                                          
                        if (workingWord.Length >= 2)
                        {
                            //search 2 letters 
                            searchLetter = workingWord.Substring(0, 2);
                            Atome aa = atomes.FirstOrDefault(x => x.symbole.ToLower() == searchLetter.ToLower());

                            if (aa != null)
                            {
                                atomsNeeded.Add(aa);

                                if (workingWord.Length == 2)
                                {
                                    //Mot complet
                                    string info = word + " \t " + string.Join(" ", atomsNeeded.Select(x => x.symbole).ToArray()) + " \t " + string.Join(" ", atomsNeeded.Select(x => x.numero).ToArray());
                                    Debug.WriteLine(info);
                                    results.Add(info);
                                    workingWord = ""; //on sort du while et on passe au suivant                                    
                                }
                                else
                                    workingWord = workingWord.Substring(2, workingWord.Length - 2);

                            }
                            else
                            {
                                //pas de symbole en 1 ou 2 lettres
                                workingWord = ""; //on sort du while et on passe au suivant
                            }
                        }
                        else
                        {                            
                            workingWord = ""; //on sort du while et on passe au suivant
                        }
                    }
                }

            }

            File.WriteAllLines(@"C:\Yann\projets\LaChose\LaChose\resources\results.txt", results, Encoding.UTF8);
        }


        public static void DicoParse()
        {
            List<string> dico = new List<string>();
            var txt = System.IO.File.ReadAllLines(@"C:\Yann\projets\LaChose\LaChose\resources\dela-fr-public.dic", Encoding.UTF8);

            foreach (string s in txt)
            {
                string input = s.Split(',')[0];
                dico.Add(input);
            }

            File.WriteAllLines(@"C:\Yann\projets\LaChose\LaChose\resources\dela-fr-public.txt", dico, Encoding.UTF8);


        }


        public static void TestJson()
        {
            var txt = System.IO.File.ReadAllText(@"C:\Yann\projets\LaChose\LaChose\resources\atomes.json", Encoding.UTF8);
            var atomes = JsonConvert.DeserializeObject<List<Atome>>(txt);
            var json = JsonConvert.SerializeObject(atomes);
            File.WriteAllText(@"C:\yann\temp\atomes2.json", json, Encoding.UTF8);
        }



        public static void CsvToJson()
        {
            var csv = new List<string[]>();

            var lines = System.IO.File.ReadAllLines(@"C:\yann\temp\atomes.csv", Encoding.UTF8);

            foreach (string line in lines)
                csv.Add(line.Split(','));

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);

            System.IO.File.WriteAllText(@"C:\yann\temp\atomes.json", json, Encoding.UTF8);
        }

        public static void XmlToJson()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"C:\yann\temp\atomes.xml");
            string json = JsonConvert.SerializeXmlNode(doc);

            System.IO.File.WriteAllText(@"C:\yann\temp\atomes.json", json, Encoding.UTF8);
        }

        #endregion

    }
}
