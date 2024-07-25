using FindUsHere.General.Interfaces;
using LinqToDB.Mapping;

namespace FindUsHere.DbConnector.Models
{
    [Table("Category")]
    internal class DbCategory : ICategory
    {
        #region DB Fields
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("Name"), NotNull]
        public string Name { get; set; }
        #endregion

        #region constructor
        public DbCategory()
        {

        }
        public DbCategory(string categoryName)
        {
            Name = categoryName;
        }
        #endregion
    }
}
