using Microsoft.Data.Sqlite;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class UserRepository
{
    private const string ConnectionString = "Data Source = Users.db";
    private SqliteCommand _command;
    private SqliteConnection _connection;

    public UserRepository()
    {
        OpenConnection();
        CreateUsersTable();
    }

    public void OpenConnection()
    {
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();
        _command = _connection.CreateCommand();
    }

    public void CreateUsersTable()
    {
        _command.CommandText = "create table if not exists users(id integer primary key autoincrement, name TEXT, email TEXT,phoneNumber TEXT, password TEXT)";
        _command.ExecuteNonQuery();
    }
    public void CommandTable(Users users)
    {
        _connection.Open();
        _command = _connection.CreateCommand();
        _command.CommandText = "INSERT INTO users(name, email, phoneNumber, password) VALUES(@name, @email, @phonenumber, @password)";
        _command.Parameters.AddWithValue("@name", users.Name);
        _command.Parameters.AddWithValue("@email", users.Email);
        _command.Parameters.AddWithValue("@phonenumber", users.PhoneNumber);
        _command.Parameters.AddWithValue("@password", users.Password);
        _command.Prepare();

        _command.ExecuteNonQuery();
        _connection.Close();
        
    }
    public List<Users> GetUsers()
    {
        var user = new List<Users>();
        _connection.Open();
        var _command = _connection.CreateCommand();
        _command.CommandText = "SELECT * FROM users";
        var data = _command.ExecuteReader();

        while (data.Read())
        {
            var users = new Users();

            users.Name = data.GetString(0);
            users.Email = data.GetString(1);
            users.PhoneNumber = data.GetString(2);
            users.Password = data.GetString(3);

            user.Add(users);
        }
        _connection.Open();

        return user;
    }
    public Users GetUserByPhoneNumber(string phoneNumber)
    {
        var user = new Users();
        _connection.Open();
        var _command = _connection.CreateCommand();
        _command.CommandText = $"SELECT * FROM users WHERE phoneNumber = '{phoneNumber}'";
        var data = _command.ExecuteReader();  
        while (data.Read())
        {
            user.Id = data.GetInt32(0);
            user.Name = data.GetString(1);
            user.Email = data.GetString(2);
            user.PhoneNumber = data.GetString(3);
            user.Password = data.GetString(4);
        }
        _connection.Close();
        return user;
    }
    public Users GetUserByIndex(int id)
    {
        var user = new Users();
        _connection.Open();
        var _command = _connection.CreateCommand();

        _command.CommandText = $"SELECT * FROM users WHERE id = {id}";
        var data = _command.ExecuteReader();
        while (data.Read())
        {
            user.Name = data.GetString(0);
            user.Email = data.GetString(1);
            user.PhoneNumber = data.GetString(2);
            user.Password = data.GetString(4);
        }
        _connection.Close();
        return user;
    }


   
}
