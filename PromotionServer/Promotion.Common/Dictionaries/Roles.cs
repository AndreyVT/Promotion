namespace Promotion.Common.Dictionaries
{
    using Promotion.Entities.Dictionary;

    public class Roles
    {
        public static PRole Admin = new PRole() { Name = "Администратор", LogicalName = "Admin", Description = "Администратор" };
        public static PRole Manager = new PRole() { Name = "Менеджер", LogicalName = "Manager", Description = "Менеджер" };
        public static PRole User = new PRole() { Name = "Пользователь", LogicalName = "User", Description = "Пользователь" };
    }
}
