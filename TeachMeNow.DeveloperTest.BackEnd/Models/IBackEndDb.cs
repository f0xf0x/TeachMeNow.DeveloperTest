using NMemory.Tables;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    public interface IBackEndDb {
        ITable<User> Users { get; }

        ITable<Class> Classes { get; }
    }
}