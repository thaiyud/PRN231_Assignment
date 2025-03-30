using SchoolPsychologicalHealthSupportSystem.Models;
using SchoolPsychologicalHealthSupportSystem_Service;

namespace SchoolPsychologicalHealthSupportSystem_API.GraphQL
{
    public class Mutation
    {
        private readonly IBlogService _blogService;
        public Mutation(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<int> CreateBlog(Blog blog)
        {
            try
            {
                var result =  await _blogService.Create(blog);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UpdateBlog(Blog blog)
        {
            try
            {
                var result = await _blogService.Update(blog);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteBlog(int id)
        {
            try
            {
                var result = await _blogService.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
