namespace Study.Domain.Entities;

public class Student
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string BirthDate { get; set; }
    public string Phone { get; set; }
}