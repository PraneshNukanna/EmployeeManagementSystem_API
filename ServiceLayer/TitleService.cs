using API_ManagementSystem_ClassActivity.Models;
using API_ManagementSystem_ClassActivity.RequestsLayer;
using API_ManagementSystem_ClassActivity.ResponseLayer;
using API_ManagementSystem_ClassActivity.Data;

namespace API_ManagementSystem_ClassActivity.ServiceLayer
{
    public class TitleService
    {
        private readonly TitleData _titleData;

        public TitleService(TitleData titleData)
        {
            _titleData = titleData;
        }

        public async Task<TitleResponse> Get(Guid id)
        {
            var title = await _titleData.Get(id);

            var response = new TitleResponse
            {
                Id = title.Id,
                Description = title.Description
            };

            return response;
        }

        public async Task<List<TitleResponse>> GetAll()
        {
            var titles = await _titleData.GetAll();

            var response = titles.Select(title => new TitleResponse
            {
                Id = title.Id,
                Description = title.Description
            }).ToList();

            return response;
        }

        public async Task<TitleResponse> Create(TitleRequest request)
        {
            var title = new Title
            {
                Description = request.Description
            };

            await _titleData.Create(title);

            return new TitleResponse
            {
                Id = title.Id,
                Description = title.Description
            };
        }

        public async Task<TitleResponse> Update(Guid id, TitleRequest request)
        {
            var title = await _titleData.Get(id);

            title.Description = request.Description;

            await _titleData.Update(title);

            return new TitleResponse
            {
                Id = title.Id,
                Description = title.Description
            };
        }

        public async Task Delete(Guid id)
        {
            await _titleData.Delete(id);
        }
    }
}
