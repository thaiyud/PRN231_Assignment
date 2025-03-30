using GraphQL;
using GraphQL.Types;
using SchoolPsychologicalHealthSupportSystem_Service;
using SchoolPsychologicalHealthSupportSystem.Models;
using System.Collections.Generic;

public class CategoryQuery : ObjectGraphType
{
    public CategoryQuery(ICategoryService categoryService)
    {
        // Lấy tất cả danh mục
        Field<ListGraphType<CategoryType>>(
            "categories",
            resolve: context => categoryService.GetAll().Result
        );

        // Lấy danh mục theo ID
        Field<CategoryType>(
            "category",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
            ),
            resolve: context =>
            {
                int id = context.GetArgument<int>("id");
                return categoryService.GetById(id);
            }
        );
    }
}
