using Microsoft.Data.Sqlite;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class VideoRepository
{
    private const string ConnectionString = "Data Source = video.db";
    private SqliteConnection _connection;

    public VideoRepository()
    {
        _connection = new SqliteConnection(ConnectionString);
        CreateVideoTable();
    }

    public void CreateVideoTable()
    {
        _connection.Open();
        var _command = _connection.CreateCommand();
        _command.CommandText = "create table if not exists video(id integer primary key autoincrement, user_id INTEGER, name TEXT, url TEXT)";
        _command.ExecuteNonQuery();
        _connection.Close();
    }
    public void PostVideo(int userId, Video video)
    {
        _connection.Open();
        var _command = _connection.CreateCommand();
        _command.CommandText = "INSERT INTO video(user_id, name, url) VALUES(@user_id, @name, @url)";
        _command.Parameters.AddWithValue("@user_id", userId);
        _command.Parameters.AddWithValue("@name", video.Name);
        _command.Parameters.AddWithValue("@url", video.Url);
        _command.Prepare();
        _command.ExecuteNonQuery();
        _connection.Close();
    }
    public List<Video> GetVideos(int userId)
    {
        var videos = new List<Video>();
        _connection.Open();
        var _command = _connection.CreateCommand();
        _command.CommandText = $"SELECT * FROM video WHERE user_id = {userId}";
        var data = _command.ExecuteReader();

        while (data.Read())
        {
            var video = new Video();
            video.Id = data.GetInt32(0);
            video.UserId = data.GetInt32(1);
            video.Name = data.GetString(2);
            video.Url = data.GetString(3);
            videos.Add(video);
        }
        _connection.Close();

        return videos;
    }
    public List<Video> GetAllVideos()
    {
        var videos = new List<Video>();

        _connection.Open();
        var cmd = _connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM video";
        var data = cmd.ExecuteReader();
        while (data.Read())
        {
            var video = new Video();

            video.Id = data.GetInt32(1);
            video.UserId = data.GetInt32(2);
            video.Name = data.GetString(3);
            video.Url = data.GetString(4);
            
            videos.Add(video);
        }

        _connection.Close();
        return videos;
    }

}
