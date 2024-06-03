using Pearson_CodingChallenge.Utility;
using Pearson_CodingChallenge.Models;
using System.Data.SqlClient;
using System.Data;

namespace Pearson_CodingChallenge.Services
{
    public class DatabaseService
    {
        string connectionString = ConnectionString.ConnectionName;

        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Order order = new Order
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        CustomerId = sqlDataReader["CustomerId"].ToString(),
                        StudyGuideId = sqlDataReader["StudyGuideId"].ToString(),
                        IsFulfilled = false
                    };
                    if (sqlDataReader["IsFulfilled"] != DBNull.Value)
                    {
                        order.IsFulfilled = Convert.ToBoolean(sqlDataReader["IsFulfilled"]);
                    }
                    if (sqlDataReader["DateFulfilled"] != DBNull.Value)
                    {
                        order.DateFulfilled = DateOnly.FromDateTime(Convert.ToDateTime(sqlDataReader["DateFulfilled"]));
                    }
                    orders.Add(order);
                }
                con.Close();
            }
            return orders;
        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", customer.Id);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddStudyGuide(StudyGuide studyGuide)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddStudyGuide", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", studyGuide.Id);
                cmd.Parameters.AddWithValue("@Name", studyGuide.Name);
                cmd.Parameters.AddWithValue("@Price", studyGuide.Price);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddOrder(Order order)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                cmd.Parameters.AddWithValue("@StudyGuideId", order.StudyGuideId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", order.Id);
                cmd.Parameters.AddWithValue("@IsFulfilled", order.IsFulfilled);
                cmd.Parameters.AddWithValue("@DateFulfilled", order.DateFulfilled);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Customer GetCustomer(string customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", customerId);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                Customer customer = null;

                while (sqlDataReader.Read())
                {
                    customer = new Customer
                    {
                        Id = sqlDataReader["Id"].ToString(),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        LastName = sqlDataReader["LastName"].ToString(),
                        Email = sqlDataReader["Email"].ToString()
                    };
                }
                con.Close();

                return customer;
            }
        }

        public StudyGuide GetStudyGuide(string studyGuideId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetStudyGuide", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", studyGuideId);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                StudyGuide studyGuide = null;

                while (sqlDataReader.Read())
                {
                    studyGuide = new StudyGuide
                    {
                        Id = sqlDataReader["Id"].ToString(),
                        Name = sqlDataReader["Name"].ToString(),
                        Price = (double)sqlDataReader["LastName"],
                    };
                }
                con.Close();

                return studyGuide;
            }
        }

        public Order GetOrder(string customerId, string studyGuideId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@StudyGuideId", studyGuideId);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                Order order = null;

                while (sqlDataReader.Read())
                {
                    order = new Order
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        CustomerId = sqlDataReader["CustomerId"].ToString(),
                        StudyGuideId = sqlDataReader["StudyGuideId"].ToString()
                    };
                }
                con.Close();

                return order;
            }
        }

        public Order GetOrderById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetOrderById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                Order order = null;

                while (sqlDataReader.Read())
                {
                    order = new Order
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        CustomerId = sqlDataReader["CustomerId"].ToString(),
                        StudyGuideId = sqlDataReader["StudyGuideId"].ToString()
                    };
                }
                con.Close();

                return order;
            }
        }
    }
}
