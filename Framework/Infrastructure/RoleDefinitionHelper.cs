

namespace Framework.Infrastructure
{
    public static class RoleDefinitionHelper
    {
        public static RoleDefinition Admin { get; } = new(-1, "ادمین");
        public static RoleDefinition Salesman { get; } = new(-2, "مسؤل فروش");
        public static RoleDefinition WarehouseOperator { get; } = new(-3, "مسؤل انبار");
        public static RoleDefinition ContentUploader { get; } = new(-4, "محتوا گذار");
        public static RoleDefinition NormalUser { get; } = new(-5, "کاربر معمولی");

    }
    public struct RoleDefinition
    {
        public long Id { get;private set; }
        public string Name { get;private set; }

        public RoleDefinition(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
