using OneOf;
using QuizGamePerssistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace QuizGamePerssistence.Repositories
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetCollections(CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> GetCollectionByID(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> AddCollection(Collection collection, bool addNewQuizzes, CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> DeleteCollection(Guid id, CancellationToken cancellationToken);
        Task<OneOf<Collection, RequestError>> UpdateCollection(Guid id, Collection collection, CancellationToken cancellationToken);
    }
}
