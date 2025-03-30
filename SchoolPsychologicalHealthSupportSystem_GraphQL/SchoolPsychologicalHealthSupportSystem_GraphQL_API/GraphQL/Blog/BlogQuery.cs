using GraphQL;
using GraphQL.Types;
using SchoolPsychologicalHealthSupportSystem_Service;

public class BlogQuery : ObjectGraphType
{
    public BlogQuery(IBlogService blogService)
    {
        // Lấy danh sách Blog
        Field<ListGraphType<BlogType>>(
           "blogs",
           resolve: context =>
           {
               var blogs = blogService.GetAll().Result;
               if (blogs == null)
               {
                   throw new ExecutionError("No blogs found.");
               }
               return blogs;
           }
       );

        // Lấy Blog theo ID
        Field<BlogType>(
            "blog",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: context =>
            {
                var blog = blogService.GetById(context.GetArgument<int>("id")).Result;
                if (blog == null)
                {
                    throw new ExecutionError("Blog not found.");
                }
                return blog;
            }
        );

        Field<ListGraphType<BlogType>>(
            "searchBlogs",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "title" },
                new QueryArgument<StringGraphType> { Name = "cateName" }
            ),
            resolve: context =>
            {
                var name = context.GetArgument<string>("name", null);
                var title = context.GetArgument<string>("title", null);
                var cateName = context.GetArgument<string>("cateName", null);

                var blog = blogService.Search(name, title, cateName).Result;
                if (blog == null)
                {
                    throw new ExecutionError("Blog not found.");
                }
                return blog;
            }
        );

    }
}
