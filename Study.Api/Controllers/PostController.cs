using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;

namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
  private readonly string _connectionString =
    "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<Post> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM posts", connection);
      var reader = command.ExecuteReader();  
      var posts = new List<Post>();
      while (reader.Read())
      {
        var post = new Post
      {
        userId = Convert.ToInt32(reader[0]),
        Id = Convert.ToInt32(reader[1]),
        title = (string)reader[2],
        body = (string)reader[3],
      };
     posts.Add(post);
      }
      connection.Close();
    
      return posts;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public void Add([FromBody] Post post)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      INSERT INTO posts(
            userId,
            title,
            body
     ) values (
               '{post.userId}',
               '{post.title}',
               '{post.body}'
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
      DELETE FROM posts where id={Id};
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
  public void Update(long Id,[FromBody] Post post)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      UPDATE posts SET
             userId = '{post.userId}',
             title = '{post.title}',
             body = '{post.body}'
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

