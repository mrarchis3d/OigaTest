using Domain.Utils;
using System.Text.RegularExpressions;

namespace Domain.DTOs.User
{
    public class UserPaged: PagedCriteria
    {
        private string? _searchWords;  // the name field
        public string SearchWords {

            get => _searchWords!;
            set
            {
                _searchWords = Patterns.GetFilteredValue(value, Patterns.FilterOnlyAlphabet);
            }
        }
    }
}
