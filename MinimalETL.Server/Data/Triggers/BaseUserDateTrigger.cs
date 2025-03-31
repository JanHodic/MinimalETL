using Microsoft.EntityFrameworkCore;
using MinimalETL.Server.Data.Contexts;
using MinimalETL.Server.Models.Bases;
using EntityFrameworkCore.Triggered;

namespace MinimalETL.Server.Data.Triggers
{
    public class BaseUserDateTrigger : IBeforeSaveTrigger<BaseDateEntity>
    {
        private readonly TestETLDbContext _dbContext;
        public BaseUserDateTrigger(
            TestETLDbContext entityDbContext
            )
        {
            _dbContext = entityDbContext;
        }

        /// <summary>
        /// Sets automatically the date of data changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="DbUpdateException"></exception>
        public Task BeforeSave(ITriggerContext<BaseDateEntity> context, CancellationToken cancellationToken)
        {

            switch (context.ChangeType)
            {
                case ChangeType.Added:
                    context.Entity.Created = DateTime.Now;
                    context.Entity.Updated = DateTime.Now;
                    break;

                case ChangeType.Modified:
                    var originalValidTo = _dbContext.Entry(context.Entity).OriginalValues["ValidTo"];
                    var originalDeletedAt = _dbContext.Entry(context.Entity).OriginalValues["DeletedAt"];
                    if (originalValidTo != null && originalDeletedAt != null) // protection against rewriting later
                    {
                        throw new DbUpdateException();
                    }

                    context.Entity.Updated = DateTime.Now;
                    break;

                default: break;
            }
            return Task.CompletedTask;
        }
    }
}
