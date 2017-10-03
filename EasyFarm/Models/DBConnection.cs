using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace EasyFarm.Models
{
    public class DBConnection
    {
       private string ConnectionString = "Server=localhost;Database=easyfarm;Uid=api;Pwd=admin;";
       private static readonly DBConnection instance = new DBConnection();

       public static DBConnection Instance
       {
           get
           {
               return instance;
           }
       }

        public Field insertField(Field field, string user_id)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand cmd;
            connection.Open();

            try
            {              
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Field(name,location,size,last_action,user_id)VALUES(@Name, @Location, @Size, @LastAction, @User_id);";
                cmd.Parameters.AddWithValue("@Name", field.name);
                cmd.Parameters.AddWithValue("@Location", field.location);
                cmd.Parameters.AddWithValue("@Size", field.size);
                cmd.Parameters.AddWithValue("@LastAction", field.last_action);
                cmd.Parameters.AddWithValue("@User_id", user_id);
                cmd.ExecuteNonQuery();
                field.Id = cmd.LastInsertedId;
                connection.Close();
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return new Field();
            }
            

            return field;
        }

        public Field updateField(int Id, Field field, string user_id )
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand cmd;
            connection.Open();

            try
            {

                cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE field SET name=@Name, location=@Location, size=@Size, last_action=@LastAction  WHERE id=@Id AND user_id = @User_id;";
                cmd.Parameters.AddWithValue("@Name", field.name);
                cmd.Parameters.AddWithValue("@Location", field.location);
                cmd.Parameters.AddWithValue("@Size", field.size);
                cmd.Parameters.AddWithValue("@LastAction", field.last_action);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@User_id", user_id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return new Field();
            }

            return getField(Id,user_id);
        }

        public Field getField(int id, string user_id)
        {
            Field field = new Field();
           
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand cmd;
            connection.Open();

            try
            {              
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM field WHERE id = @Id AND user_id = @User_id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@User_id", user_id);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    field.Id = (int)dataReader["id"];
                    field.name = dataReader["name"].ToString();
                    field.location = dataReader["location"].ToString();
                    field.size = (float) dataReader["size"];
                    field.last_action = dataReader["last_action"].ToString();
                }

                dataReader.Close();
                connection.Close();

            }
            catch
            {
              
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return new Field();
            }


            return field;
        }

        public List<Field> getFields(string user_id)
        {
            List<Field> fieldList = new List<Field>();

            Field field;

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand cmd;
            connection.Open();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM field WHERE user_id = @User_id;";
                cmd.Parameters.AddWithValue("@User_id", user_id);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    field = new Field();
                    field.Id = (int)dataReader["id"];
                    field.name = dataReader["name"].ToString();
                    field.location = dataReader["location"].ToString();
                    field.size = (float)dataReader["size"];
                    field.last_action = dataReader["last_action"].ToString();

                    fieldList.Add(field);
                }

                dataReader.Close();
                connection.Close();

            }
            catch
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return new List<Field>();
            }


            return fieldList;
        }

        public bool deleteField(int id, string user_id)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand cmd;
            connection.Open();

            try
            {

                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Field WHERE id=@id AND user_id = @User_id;";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@User_id", user_id);
                cmd.ExecuteNonQuery();
               // field.Id = cmd.LastInsertedId;
                connection.Close();
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return false;
            }

            return true;
        }
    }
}