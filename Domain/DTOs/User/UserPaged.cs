using Domain.Utils;

namespace Domain.DTOs.User
{
    public class UserPaged: PagedCriteria
    {
        private string? _searchWords;  // the name field
        public string? SearchWords {

            get => _searchWords!;
            set
            {
                if(value != null)
                    _searchWords = Patterns.GetFilteredValue(value, Patterns.FilterOnlyAlphabet);
            }
        }
    }
}
