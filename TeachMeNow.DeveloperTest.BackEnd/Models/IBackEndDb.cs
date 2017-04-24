using NMemory.Tables;

namespace TeachMeNow.DeveloperTest.BackEnd.Models {
    public interface IBackendDb {
        ITable<User> Users { get; }

        ITable<Class> Classes { get; }
    }
}