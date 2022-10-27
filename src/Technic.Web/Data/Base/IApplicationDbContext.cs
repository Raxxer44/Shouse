using Microsoft.EntityFrameworkCore;
using Technic.Web.Data.Entites;

namespace Technic.Web.Data.Base
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Сохраняет все изменения, сделанные в этом контексте, в базу данных.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Сохраняет все изменения, сделанные в этом контексте, в базу данных.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        /// Указывает, вызывается ли <seecref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> после внесения изменений.
        /// успешно отправлено в базу данных.
        /// </param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);
        /// <summary>
        /// Асинхронный метод. Сохраняет все изменения, сделанные в этом контексте, в базу данных.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// Асинхронный метод. Сохраняет все изменения, сделанные в этом контексте, в базу данных.
        /// </summary>
        /// <param name="cancellationToken">
        /// <see cref="T:System.Threading.CancellationToken" /> распространяет уведомление о том, что операции следует отменить
        /// </param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
        /// <summary>
        /// Асинхронный метод. Сохраняет все изменения, сделанные в этом контексте, в базу данных.
        /// </summary>
        /// <param name="cancellationToken">
        /// <see cref = "T:System.Threading.CancellationToken" /> распространяет уведомление о том, что операции следует отменить
        /// </param>
        /// <param name="acceptAllChangesOnSuccess">
        /// Указывает, вызывается ли <seecref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> после внесения изменений.
        /// успешно отправлено в базу данных.
        /// </param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new());
    }
}
