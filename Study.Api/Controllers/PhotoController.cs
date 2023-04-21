using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;



namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class PhotosController : ControllerBase
{
  private readonly string _connectionString =
    "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<Photo> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM photos", connection);
      var reader = command.ExecuteReader();  
      var photos = new List<Photo>();
      while (reader.Read())
      {
        var photo = new Photo
      {
        albumId = Convert.ToInt32(reader[0]),
        Id = Convert.ToInt32(reader[1]),
        title = (string)reader[2],
        url = (string)reader[3],
        thumbnailUrl = (string)reader[4]
      };
        photos.Add(photo);
      }
      connection.Close();
    
      return photos;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public void Add([FromBody] Photo photo)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      INSERT INTO photos(
            albumId,
            title,
            url,
            thumbnailurl     
) values (
               '{photo.albumId}',
               '{photo.title}',
               '{photo.url}',
               '{photo.thumbnailUrl}'
     );
",connection);

      command.ExecuteNonQuery();
      connection.Close();
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }

  }
  
  [HttpDelete ("{Id}")]
  public void Delete(long Id)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      DELETE FROM photos where id={Id};
",connection);

      command.ExecuteNonQuery();
        connection.Close();
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }

  }

  [HttpPut ("{Id}")]
  public void Update(long Id,[FromBody] Photo photo)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      UPDATE photos SET
             albumId = '{photo.albumId}',
             title = '{photo.title}',
             url = '{photo.url}',
             thumbnailUrl = '{photo.thumbnailUrl}'
             where id={Id}
",connection);

      command.ExecuteNonQuery();
         connection.Close();
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }

  }

}

