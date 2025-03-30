using GraphQL.Types;
using SchoolPsychologicalHealthSupportSystem.Models;

public class CategoryType : ObjectGraphType<Category>
{
    public CategoryType()
    {
        Field(x => x.Id).Description("ID của danh mục");
        Field(x => x.Name).Description("Tên danh mục");
        Field(x => x.CreateAt, nullable: true).Description("Ngày tạo danh mục");
        Field(x => x.UpdateAt, nullable: true).Description("Ngày cập nhật danh mục");
        Field<ListGraphType<BlogType>>("blogs", resolve: context => context.Source.Blogs);
    }
}
