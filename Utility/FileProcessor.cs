using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Pearson_CodingChallenge.Models;
using Pearson_CodingChallenge.Services;

namespace Pearson_CodingChallenge.Utility
{
    public static class FileProcessor
    {
        public static Dictionary<string, string> ProcessFile(DataFile file)
        {
            Dictionary<string, string> processedFile = new Dictionary<string, string>
            {
                { "errors", "" },
                { "message", ""}
            };
            if (file.ImportFile != null)
            {               

                try
                {
                    List<string> lines = new List<string>();
                    using (var fileReader = new StreamReader(file.ImportFile.OpenReadStream()))
                    {                        
                        while (!fileReader.EndOfStream)
                        {
                            lines.Add(fileReader.ReadLine());
                        }

                        fileReader.Close();
                    }

                    return InsertObjects(lines, processedFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    processedFile["errors"] = "Error processing file - invalid file format.";
                }

            }
            else
            {
                processedFile["errors"] = "Error processing file - invalid file format.";
            }

            return processedFile;
        }

        private static Dictionary<string, string> InsertObjects(List<string> lines, Dictionary<string, string> processedFile)
        {
            string[] headers = lines[0].Split('|');
            if(headers.Length == 1)
            {
                processedFile["errors"] = "Error processing file - invalid file format";
                return processedFile;
            }
            string recordType = "";
            Dictionary<string, int> result = new Dictionary<string, int>
            {
                {"successCount", 0 },
                {"failCount", 0 },
                {"skipCount", 0 }
            };
            for (int i = 1; i < lines.Count; i++)
            {
                string[] record = lines[i].Split("|");
                if (record.Length == 1)
                {
                    result["failCount"]++;
                    continue;
                }

                if (lines[0].Contains("CUSTOMER") && lines[0].Contains("STUDY GUIDE"))
                {
                    recordType = "Order";
                    List<Order> orders = CreateOrder(record);
                    result = InsertOrders(orders, result);
                }
                else if (lines[0].Contains("CUSTOMER"))
                {
                    recordType = "Customer";
                    Customer customer = CreateCustomer(record);
                    result = InsertCustomer(customer, result);
                }
                else if (lines[0].Contains("STUDY GUIDE"))
                {
                    recordType = "Study Guide";
                    StudyGuide studyGuide = CreateStudyGuide(record);
                    result = InsertStudyGuide(studyGuide, result);
                }
            }

            processedFile["message"] = result["successCount"] + " " + recordType + " successfully imported, " + result["failCount"] + " failed, " + result["skipCount"] + " skipped";

            return processedFile;
        }

        private static Customer CreateCustomer(string[] record)
        {
            Customer customer = new Customer();
            customer.Id = record[0];
            string[] name = record[1].Split(' ');
            customer.FirstName = name[0];
            customer.LastName = name[1];
            customer.Email = record[2];

            return customer;
        }

        private static StudyGuide CreateStudyGuide(string[] record)
        {
            StudyGuide studyGuide = new StudyGuide
            {
                Id = record[0],
                Name = record[1],
                Price = Convert.ToDouble(record[2])
            };

            return studyGuide;
        }

        private static List<Order> CreateOrder(string[] record)
        {
            string[] studyGuideOrders = record[1].Split(',');
            List<Order> orders = new List<Order>();
            foreach (var studyGuide in studyGuideOrders)
            {
                Order order = new Order
                {
                    CustomerId = record[0],
                    StudyGuideId = studyGuide
                };
                orders.Add(order);
            }

            return orders;
        }

        private static Dictionary<string, int> InsertOrders(List<Order> orders, Dictionary<string, int> result)
        {
            DatabaseService dbService = new DatabaseService();
            foreach (Order order in orders)
            {
                Order existingOrder = dbService.GetOrder(order.CustomerId, order.StudyGuideId);
                if (existingOrder != null)
                {
                    result["skipCount"]++;
                }
                else
                {
                    try
                    {
                        dbService.AddOrder(order);
                        result["successCount"]++;
                    }
                    catch (Exception ex)
                    {
                        result["failCount"]++;
                        Console.WriteLine("FileProcessor :: InsertOrders :: Failed :: " + ex.ToString());
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, int> InsertCustomer(Customer customer, Dictionary<string, int> result)
        {
            DatabaseService dbService = new DatabaseService();
            Customer dbCustomer = dbService.GetCustomer(customer.Id);
            if(dbCustomer != null)
            {
                result["skipCount"]++;
            }
            else
            {
                try
                {
                    dbService.AddCustomer(customer);
                    result["successCount"]++;
                }
                catch (Exception ex)
                {
                    result["failCount"]++;
                    Console.WriteLine("FileProcessor :: InsertCustomer :: Failed to insert :: " + ex.ToString());
                }
            }

            return result;
        }

        private static Dictionary<string, int> InsertStudyGuide(StudyGuide studyGuide, Dictionary<string, int> result)
        {
            DatabaseService dbService = new DatabaseService();
            StudyGuide dbStudyGuide = dbService.GetStudyGuide(studyGuide.Id);
            if(dbStudyGuide != null)
            {
                result["skipCount"]++;
            }
            else
            {
                try
                {
                    dbService.AddStudyGuide(studyGuide);
                    result["successCount"]++;
                }
                catch (Exception ex)
                {
                    result["failCount"]++;
                    Console.WriteLine("FileProcessor :: InsertStudyGuide :: Failed to insert :: " + ex.ToString());
                }
            }

            return result;
        }
    }
}
