using Pearson_CodingChallenge.Models;

namespace Pearson_CodingChallenge.Utility
{
    public static class FileProcessor
    {
        public static void ProcessFile(DataFile file)
        {
            if (file.ImportFile != null)
            {
                try
                {
                    List<string> lines = new List<string>();
                    using (var fileReader = new StreamReader(file.ImportFile.OpenReadStream()))
                    {                        
                        while (fileReader.Peek() >= 0)
                        {
                            lines.Add(fileReader.ReadLine());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("File was Null");
            }

        }
    }
}
