namespace Common
{
    public static class GlobalConstants
    {
        // Categories constants
        public const string Long_Description = "Long description";
        public const string Category_Name_Is_Required = "Category name is required";
        public const string Id_Mismatch = "Id mismatch";
        public const string Updated = "Updated";
        public const string Deleted = "Deleted";
        public const string Category_Added = "Category added";
        public const int Category_Description_Max_Length = 1000;
        public const int Category_Name_Max_Length = 200;

        // Products constants
        public const string Ten_Percent = "10%";
        public const string Price_Must_Be_Greater_Than_Zero = "Price must be greater than zero";
        public const string Invalid_Price = "Invalid price";
        public const string Product_Not_Found = "Product not found";
        public const string Cannot_Delete_Expensive_Products = "Cannot delete expensive products";
        public const string Cannot_Delete_Product = "Products priced over $50 cannot be deleted.";
        public const string Product_Added = "Product added";
        public const int Product_Name_Max_Length = 200;
        public const string Cannot_Delete_Product_With_Review = "Product cannot be deleted because it has customer reviews.";

        // Reviews constants
        public const string Rating_Must_Be_Between_1_And_5 = "Rating must be between 1 and 5";
        public const string Comment_Cannot_Be_Empty = "Comment cannot be empty";
        public const string Reviewer_Name_Is_Required = "Reviewer name is required";
    }
}
