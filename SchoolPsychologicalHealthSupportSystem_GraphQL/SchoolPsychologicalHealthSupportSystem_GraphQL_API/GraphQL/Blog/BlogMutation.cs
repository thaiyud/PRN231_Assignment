using GraphQL;
using GraphQL.Types;
using SchoolPsychologicalHealthSupportSystem_Service;
using SchoolPsychologicalHealthSupportSystem.Models;
using System.Threading.Tasks; // Quan trọng: Thêm namespace này

public class BlogMutation : ObjectGraphType
{
    public BlogMutation(IBlogService blogService)
    {
        // Thêm Blog
              Field<IntGraphType>(
            "createBlog",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<BlogInputType>> { Name = "blogInput" } 
            ),
            resolve: context =>
            {
                var blogInput = context.GetArgument<Blog>("blogInput"); 

                var blog = new Blog
                {
                    Id = blogInput.Id,
                    Name = blogInput.Name,
                    Title = blogInput.Title,
                    CategoryId = blogInput.CategoryId,
                    Content = blogInput.Content,
                    TopicImages = blogInput.TopicImages,
                    CreateAt = DateTime.UtcNow
                };

                return blogService.Create(blog).Result;
            }
        );


        // Cập nhật Blog
        Field<IntGraphType>(
            "updateBlog",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<BlogInputType>> { Name = "blogInput" }
            ),
            resolve: context =>
            {
                var blogInput = context.GetArgument<Blog>("blogInput");

                var existingBlog = blogService.GetById(blogInput.Id).GetAwaiter().GetResult();

                if (existingBlog == null)
                {
                    throw new ExecutionError("Blog không tồn tại!");
                }

                existingBlog.Name = blogInput.Name;
                existingBlog.Title = blogInput.Title;
                existingBlog.CategoryId = blogInput.CategoryId;
                existingBlog.Content = blogInput.Content;
                existingBlog.TopicImages = blogInput.TopicImages;
                existingBlog.UpdateAt = DateTime.UtcNow;

                return blogService.Update(existingBlog).Result;
            }
        );


        // Xóa Blog
        Field<BooleanGraphType>(
            "deleteBlog",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
            ),
            resolve: context =>
            {
                int id = context.GetArgument<int>("id");
                var existingBlog = blogService.GetById(id).GetAwaiter().GetResult();

                if (existingBlog == null)
                {
                    throw new ExecutionError("Blog không tồn tại!");
                }

                return blogService.Delete(id).Result;
            }
        );
    }
}
