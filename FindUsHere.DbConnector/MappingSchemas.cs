using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.DbConnector
{
    /// <summary>
    /// Mapping schemas for Linq2db
    /// </summary>
    internal static class MappingSchemas
    {
        private static readonly MappingSchema mappingSchema = new();
        /// <summary>
        /// Implementation for mapping schemas
        /// </summary>
        /// <returns>MappingSchema</returns>
        public static MappingSchema Get()
        {
            mappingSchema.SetConverter<TimeSpan, TimeOnly>(TimeOnly.FromTimeSpan);

            return mappingSchema;
        }
    }
}
