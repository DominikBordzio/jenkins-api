using vagrant2_api.Models;

namespace vagrant2_api.Dto
{
    public record PersonAddDto(string FirstName, string LastName, int Age)
    {
        public Person ToPerson() => new Person()
        {
            PersonId = default,
            FirstName = this.FirstName,
            LastName = this.LastName,
            Age = this.Age
        };
    };
}