using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;

namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
  private readonly string _connectionString =
    "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<Comment> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM comments", connection);
      var reader = command.ExecuteReader();  
      var comments = new List<Comment>();
      while (reader.Read())
      {
        var comment = new Comment
      {
        postId = Convert.ToInt32(reader[0]),
        Id = Convert.ToInt32(reader[1]),
        name = (string)reader[2],
        email = (string)reader[3],
        body = (string)reader[4]
      };
        comments.Add(comment);
      }
      connection.Close();
    
      return comments;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public void Add([FromBody] Comment comment)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      INSERT INTO comments(
            postId,
            name,
            email,
            body
     ) values (
               '{comment.postId}',
               '{comment.name}',
               '{comment.email}',
               '{comment.body}'
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
      DELETE FROM comments where id={Id};
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
  public void Update(long Id,[FromBody] Comment comment)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      UPDATE comments SET
             postId = '{comment.postId}',
             name = '{comment.name}',
             email = '{comment.email}',
             body = '{comment.body}'
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

