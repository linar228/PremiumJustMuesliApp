using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using MuesliCore.ViewModels;

namespace MuesliCore
{
    public class DBConnect
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MuesliDB"].ConnectionString;
        private static IDbConnection connection = new SqlConnection(connStr);

        public static bool IsLoginCorrect(LoginModel model)
        {
            return connection.Query<User>("SELECT * FROM [dbo].[User] u " +
                                            $"where u.[Login] = '{model.Email}'" +
                                            $"and u.[Password] = '{model.Password}'").AsList().Count > 0;
        }
        public static bool RegisterUser(RegisterModel model)
        {
            try
            {
                connection.Query($"insert into [dbo].[User] (Email, [Password]) " +
                    $"values('{model.Email}', '{model.Password}')");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static List<Type> GetTypes()
        {
            return connection.Query<Type>("select * from [dbo].[Type]").AsList();
        }
        public static List<Muesli> FindMuesliByType( Type type)
        {
            return connection.Query<Muesli>($"select * from Muesli m where m.[TypeId] = {type.ID}").AsList();
            
        }
        public static List<MuesliMix> GetMuesliMixes()
        {
            return connection.Query<MuesliMix>("select * from MuesliMix").AsList();
        }
        public static List<Order> GetOrders()
        {
            return connection.Query<Order>("select * from [Order]").AsList();
        }
    }
}
