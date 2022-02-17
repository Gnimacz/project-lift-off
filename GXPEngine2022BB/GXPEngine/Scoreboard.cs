using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GXPEngine
{
    public static class Scoreboard
    {
        public static String scores { get; set; }
        public static string oldScore;

        public static async void WriteScore()
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(@"scores.txt");
                await sw.WriteAsync("No scores yet");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Created new scoreboard file.");
                await Task.Delay(100);
                ReadScores();
            }
        }
        public static void WriteScore(int score)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(@"scores.txt");
                sw.WriteLineAsync($"{score}");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Wrote new scores to file");
            }
        }
        public static void ReadScores()
        {
            
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"scores.txt");
                //Read the first line of text
                scores = sr.ReadLine();
                oldScore = scores;
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("Creating new scores file");
                WriteScore();
            }
        }
    }
}
