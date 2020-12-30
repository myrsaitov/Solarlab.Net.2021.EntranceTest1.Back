namespace BusinessLogic.Services.Contracts.Models
{
    /// <summary>
    /// Базовая ДТО создания-обновления категории 
    /// </summary>
    public class BaseCategoryComposeDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
