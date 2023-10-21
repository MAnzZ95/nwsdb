using Nwsdb.Web.Api.Models.LandHistories;

namespace Nwsdb.Web.Api.Services.Foundations.LandHistories
{
    public interface ILandHistoryService
    {
        ValueTask<LandHistory> AddLandHistoryAsync(LandHistory landHistory);
        IQueryable<LandHistory> RetrieveAllLandHistories();
        ValueTask<LandHistory> RetrieveLandHistoryById(Guid id);
        ValueTask<LandHistory> ModifyLandHistoryAsync(LandHistory land);
    }
}
