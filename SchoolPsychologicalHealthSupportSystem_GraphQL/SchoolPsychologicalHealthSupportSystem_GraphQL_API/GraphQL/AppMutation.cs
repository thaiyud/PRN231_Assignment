using GraphQL.Types;

public class AppMutation : ObjectGraphType
{
    public AppMutation(BlogMutation blogMutation)
    {
        Field<BlogMutation>("blog", resolve: context => blogMutation);
        //Field<CategoryMutation>("category", resolve: context => categoryMutation);
    }
}
