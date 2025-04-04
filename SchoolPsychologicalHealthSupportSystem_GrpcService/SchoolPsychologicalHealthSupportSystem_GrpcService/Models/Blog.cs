﻿

namespace SchoolPsychologicalHealthSupportSystem_GrpcService.Models;

public partial class Blog
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string TopicImages { get; set; }

    public int? LikeCount { get; set; }

    public string Hashtag { get; set; }

    public int? Dislike { get; set; }

    public double? StarAverage { get; set; }

    public int? ReviewCount { get; set; }

    public bool? Status { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Category Category { get; set; }
}