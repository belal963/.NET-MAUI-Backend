using FindUsHere.General.Interfaces;

namespace FindUsHere.RestApi.Model
{
    public class RestApiCategory : ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
