using GraphQL.Types;

public class BlogInputType : InputObjectGraphType
{
    public BlogInputType()
    {
        Name = "BlogInput";

        Field<NonNullGraphType<IntGraphType>>("id");
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<NonNullGraphType<StringGraphType>>("title");
        Field<NonNullGraphType<IntGraphType>>("categoryId");
        Field<StringGraphType>("content");
        Field<StringGraphType>("topicImages");
    }
}
