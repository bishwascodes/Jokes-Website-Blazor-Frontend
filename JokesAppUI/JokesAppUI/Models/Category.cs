﻿namespace JokesAppUI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class NewCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
