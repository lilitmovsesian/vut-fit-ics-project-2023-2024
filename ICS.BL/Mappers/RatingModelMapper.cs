﻿using ICS.BL.Models;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class RatingModelMapper() : ModelMapperBase<RatingEntity, RatingListModel, RatingDetailModel>, IRatingModelMapper
{
    public override RatingListModel MapToListModel(RatingEntity? entity)
         => entity is null
        ? RatingListModel.Empty
        : new RatingListModel
        {
            Id = entity.Id,
            points = entity.Points,
            studentId = entity.StudentId,
            activityId = entity.ActivityId,
        };

    public override RatingDetailModel MapToDetailModel(RatingEntity? entity)
        => entity is null
        ? RatingDetailModel.Empty
        : new RatingDetailModel
        {
            Id = entity.Id,
            points = entity.Points,
            note = entity.Note,
            studentId = entity.StudentId
            
        };

    public override RatingEntity MapDetailModelToEntity(RatingDetailModel model)
    {
        return new RatingEntity
        {
            Id = model.Id,
            Points = model.points,
            Note = model.note,
            StudentId = model.studentId
        };
    }
}
