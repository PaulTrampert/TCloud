using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using MongoDB.Driver;

namespace TCloud.Api.Models
{
    public class ListRequest<T> : IValidatableObject
    {
        public int Start { get; set; }

        public int PageSize { get; set; } = 50;

        public string SortBy { get; set; } = 
            typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<DefaultSortAttribute>() != null)?
                .Name
            ?? typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<SortableAttribute>() != null)?
                .Name;

        [RegularExpression("^asc|desc$", ErrorMessage = "SortDir must be either 'asc' or 'desc'")]
        public string SortDir { get; set; } = "asc";
        
        public string Query { get; set; }

        public FilterDefinition<T> FilterDefinition => string.IsNullOrWhiteSpace(Query) 
            ? new FilterDefinitionBuilder<T>().Empty 
            : new FilterDefinitionBuilder<T>().Text(Query);

        public SortDefinition<T> SortDefinition => SortDir.ToLowerInvariant() == "asc"
            ? new SortDefinitionBuilder<T>().Ascending(SortBy)
            : new SortDefinitionBuilder<T>().Descending(SortBy);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var sortableProps = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<SortableAttribute>() != null ||
                            p.GetCustomAttribute<DefaultSortAttribute>() != null)
                .Select(p => p.Name)
                .ToList();
            if (SortBy != null && !sortableProps.Contains(SortBy))
            {
                yield return new ValidationResult(
                    $"{nameof(SortBy)} must be one of {string.Join(", ", sortableProps)}",
                    new []{nameof(SortBy)}
                );
            }
        }
    }
}