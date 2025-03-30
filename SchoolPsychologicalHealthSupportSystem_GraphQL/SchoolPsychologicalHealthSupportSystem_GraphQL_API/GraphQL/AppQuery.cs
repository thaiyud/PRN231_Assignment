using GraphQL;
using GraphQL.Types;

public class AppQuery : ObjectGraphType
{
    public AppQuery(BlogQuery blogQuery, CategoryQuery categoryQuery)
    {
        AddField(blogQuery.GetField("blogs"));
        AddField(blogQuery.GetField("searchBlogs"));
        AddField(blogQuery.GetField("blog"));
        AddField(categoryQuery.GetField("categories"));
        AddField(categoryQuery.GetField("category"));
    }
}
