using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;

namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly string _connectionString = "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<Album> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM albums", connection);
      var reader = command.ExecuteReader();  
      var albums = new List<Album>();
      while (reader.Read())
      {
        var album = new Album 
      {
        Id = Convert.ToInt32(reader[0]),
        title = (string)reader[1],
        userId = Convert.ToInt32(reader[2])
      };
     albums.Add(album);
      }
      connection.Close();
    
      return albums;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public void Add([FromBody] Album album)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      INSERT INTO albums(
            userid,
            title
     ) values (
               '{album.userId}',
               '{album.title}'
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
      DELETE FROM albums where Id={Id};
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
  public void Update(long Id,[FromBody] Album album)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      UPDATE albums SET
             userId = '{album.userId}',
             title = '{album.title}' 
             where Id={Id}
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

