using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using Study.Domain.Entities;
namespace Study.Domain.Entities;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
  private readonly string _connectionString =
    "Host=localhost;Database=Study;Username=iskender-crypto;Password=Iskndr-2003";
  [HttpGet]
  public IEnumerable<Student> Get()
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open(); 
      var command = new NpgsqlCommand($"SELECT * FROM \"Students\"", connection);
      var reader = command.ExecuteReader();  
      var students = new List<Student>();
      while (reader.Read())
      {
        var student = new Student
      {
        Id = Convert.ToInt32(reader[0]),
        FirstName = (string)reader[1],
        LastName = (string)reader[2],
        Patronymic = (string)reader[3],
        BirthDate = (string)reader[4],
        Phone = (string)reader[5],
      };
     students.Add(student);
      }
      connection.Close();
    
      return students;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpPost]
  public void Add([FromBody] Student student)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var q = "\"";
      var command = new NpgsqlCommand($@"
      INSERT INTO {q}Students{q}(
            {q}FirstName{q},
            {q}LastName{q},
            {q}Patronymic{q},
            {q}BirthDate{q},
            {q}Phone{q}
     ) values (
               '{student.FirstName}',
               '{student.LastName}',
               '{student.Patronymic}',
               '{student.BirthDate}',
               '{student.Phone}'
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
      var q = "\"";
      var command = new NpgsqlCommand($@"
      DELETE FROM {q}Students{q} where Id={Id};
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
  public void Update(long Id,[FromBody] Student student)
  {
    try
    {
      var connection = new NpgsqlConnection(_connectionString);
      connection.Open();
      var q = "\"";
      var command = new NpgsqlCommand($@"
      UPDATE {q}Students{q} SET
             {q}FirstName{q} = '{student.FirstName}',
             {q}LastName{q} = '{student.LastName}',
             {q}Patronymic{q} = '{student.Patronymic}',
             {q}BirthDate{q} = '{student.BirthDate}',
             {q}Phone{q} = '{student.Phone}' 
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

