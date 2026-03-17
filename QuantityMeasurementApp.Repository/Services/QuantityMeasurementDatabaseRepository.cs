using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

using QuantityMeasurementApp.Model.Entities;
using QuantityMeasurementApp.Model.Enums;

using QuantityMeasurementApp.Repository.Interface;
using QuantityMeasurementApp.Repository.Database;

namespace QuantityMeasurementApp.Repository.Services
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string _connectionString;

        public QuantityMeasurementDatabaseRepository()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);

                using SqlCommand command = new SqlCommand("sp_SaveMeasurement", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OperationType", entity.OperationType.ToString());
                command.Parameters.AddWithValue("@MeasurementCategory", entity.MeasurementCategory.ToString());

                command.Parameters.AddWithValue("@Operand1Value", entity.Operand1Value);
                command.Parameters.AddWithValue("@Operand1Unit", entity.Operand1Unit);

                command.Parameters.AddWithValue("@Operand2Value", entity.Operand2Value);
                command.Parameters.AddWithValue("@Operand2Unit", entity.Operand2Unit);

                command.Parameters.AddWithValue("@ResultValue", entity.ResultValue);
                command.Parameters.AddWithValue("@ResultUnit", entity.ResultUnit ?? (object)DBNull.Value);

                command.Parameters.AddWithValue("@ErrorMessage",
                    string.IsNullOrEmpty(entity.ErrorMessage) ? (object)DBNull.Value : entity.ErrorMessage);

                connection.Open();

                int a=command.ExecuteNonQuery();
                Console.WriteLine(a);
                 Console.WriteLine("Saved Successfully");
            }
            catch (Exception ex)
            {
                throw new Exception("Database save operation failed", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            List<QuantityMeasurementEntity> list = new();

            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);

                using SqlCommand command = new SqlCommand("sp_GetAllMeasurements", connection);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    QuantityMeasurementEntity entity = new QuantityMeasurementEntity
                    {
                        OperationType = Enum.Parse<OperationType>(
                            reader["OperationType"].ToString()
                        ),

                        MeasurementCategory = Enum.Parse<MeasurementCategory>(
                            reader["MeasurementCategory"].ToString()
                        ),

                        Operand1Value = Convert.ToDouble(reader["Operand1Value"]),
                        Operand1Unit = reader["Operand1Unit"].ToString(),

                        Operand2Value = reader["Operand2Value"] == DBNull.Value
                            ? 0
                            : Convert.ToDouble(reader["Operand2Value"]),

                        Operand2Unit = reader["Operand2Unit"].ToString(),

                        ResultValue = reader["ResultValue"] == DBNull.Value
                            ? 0
                            : Convert.ToDouble(reader["ResultValue"]),

                        ResultUnit = reader["ResultUnit"].ToString(),

                        ErrorMessage = reader["ErrorMessage"] == DBNull.Value
                            ? null
                            : reader["ErrorMessage"].ToString(),

                        Timestamp = Convert.ToDateTime(reader["Timestamp"])
                    };

                    list.Add(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database read operation failed", ex);
            }

            return list;
        }

        public void Clear()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            using SqlCommand command = new SqlCommand("sp_DeleteAllMeasurements", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            command.ExecuteNonQuery();
        }

        public int GetTotalCount()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            using SqlCommand command = new SqlCommand("sp_GetTotalMeasurements", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count;
        }
    }
}