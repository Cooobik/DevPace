using DevPace.Application.Constants;
using DevPace.Domain.Exceptions.CustomerExceptions;
using DevPace.Domain.Models.CustomerModels;
using System.Text.RegularExpressions;

namespace DevPace.Application.Helpers
{
    public static class ValidationHelper
    {
        private static bool IsValidSearchItem(string searchItem)
        {
            return Regex.IsMatch(searchItem, @"^[A-Za-z0-9\s]+$");
        }

        public static void ValidateCustomersQueryParams(CustomerFindNSortModel customerFindNSortModel)
        {
            if (!string.IsNullOrEmpty(customerFindNSortModel.SearchItem) && !IsValidSearchItem(customerFindNSortModel.SearchItem))
            {
                throw new SearchItemIsNotValidException();
            }

            if (customerFindNSortModel.RowCount < ValidationConstants.minRowCount && customerFindNSortModel.PageNumber < ValidationConstants.minPageNumber)
            {
                throw new PaginationIsNoValidException(customerFindNSortModel.RowCount, customerFindNSortModel.PageNumber);
            }
        }
    }
}
