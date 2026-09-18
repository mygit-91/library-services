using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Commons.Constants
{
    public class AppMessage
    {
        public const string SUCCESS = "Successfuly";
        public const string INSERT_SUCCESS = "Insert data successfully";
        public const string UPDATE_SUCCESS = "Update data successfully";
        public const string DELETE_SUCCESS = "Delete data successfully";
        public const string DUPLICATE_DATA = "Data is duplicated";
        public const string DATA_NOT_FOUND = "Data not found";
        public const string BOOK_NOT_AVAILABLE = "Book is not available";
        public const string CATEGORY_IN_USED = "This category is currently being used by books and cannot be deleted.";
        public const string FINES_IS_REQUIRED = "Fines Amount, Payment Status and Paid Date is required";
        public const string NO_MEMBER_FOUND = "No member found.";
        public const string INVALID_USER_LOGIN = "Invalid Username or Password";
    }
}
