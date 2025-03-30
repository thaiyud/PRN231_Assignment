using GraphQL.Types;
using SchoolPsychologicalHealthSupportSystem.Models;

public class BlogType : ObjectGraphType<Blog>
{
    public BlogType()
    {
        Field(x => x.Id).Description("Blog ID");
        Field(x => x.Name).Description("Tên Blog");
        Field(x => x.Title).Description("Tiêu đề Blog");
        Field(x => x.Content, nullable: true).Description("Nội dung Blog");
        Field(x => x.TopicImages, nullable: true).Description("Hình ảnh chủ đề");
        Field(x => x.LikeCount, nullable: true).Description("Số lượt thích");
        Field(x => x.Hashtag, nullable: true).Description("Hashtag liên quan");
        Field(x => x.Dislike, nullable: true).Description("Số lượt không thích");
        Field(x => x.StarAverage, nullable: true).Description("Đánh giá trung bình");
        Field(x => x.ReviewCount, nullable: true).Description("Số lượt đánh giá");
        Field(x => x.Status, nullable: true).Description("Trạng thái");
        Field(x => x.CreateBy, nullable: true).Description("Người tạo");
        Field(x => x.CreateAt, nullable: true).Description("Ngày tạo");
        Field(x => x.UpdateAt, nullable: true).Description("Ngày cập nhật");

        Field<CategoryType>("category", "Danh mục của Blog");
    }
}
