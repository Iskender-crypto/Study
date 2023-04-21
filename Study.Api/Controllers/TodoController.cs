using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;

namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
  private readonly string _connectionString =
    "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<Todo> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM todos", connection);
      var reader = command.ExecuteReader();  
      var todos = new List<Todo>();
      while (reader.Read())
      {
        var todo = new Todo
      {
        userId = Convert.ToInt32(reader[0]),
        Id = Convert.ToInt32(reader[1]),
        title = (string)reader[2],
        completed = (bool)reader[3]
      };
     todos.Add(todo);
      }
      connection.Close();
    
      return todos;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public void Add([FromBody] Todo todo)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      INSERT INTO todos(
            userId,
            title,
            completed
     ) values (
               '{todo.userId}',
               '{todo.title}',
               '{todo.completed}'
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
      DELETE FROM todos where id={Id};
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
  public void Update(long Id,[FromBody] Todo todo)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var command = new NpgsqlCommand($@"
      UPDATE todos SET
             userId = '{todo.userId}',
             title = '{todo.title}',
             completed = '{todo.completed}'
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

