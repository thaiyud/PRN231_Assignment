using SchoolPsychologicalHealthSupportSystem_Service;
using SchoolPsychologicalHealthSupportSystem.Models;

namespace SchoolPsychologicalHealthSupportSystem_API.GraphQL
{
    public class BlogsQuery 
    {
        private readonly IBlogService _blogService;
    
        public BlogsQuery(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<List<Blog>> GetBlogs()
        {
            try
            {
                var list = await _blogService.GetAll();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<Blog> GetBlogsById(int id)
        {
            try
            {
                var item = await _blogService.GetById(id);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
