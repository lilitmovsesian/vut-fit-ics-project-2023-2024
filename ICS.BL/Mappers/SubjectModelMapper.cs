﻿using ICS.BL.Models;
using ICS.DAL;
using ICS.DAL.Entities;

namespace ICS.BL.Mappers;

public class SubjectModelMapper (ActivityModelMapper activityModelMapper, StudentSubjectModelMapper studentSubjectModelMapper) : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailModel>, ISubjectModelMapper
{
    public override SubjectListModel MapToListModel(SubjectEntity? entity)
         => entity is null
        ? SubjectListModel.Empty
        : new SubjectListModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Abbreviation = entity.Abbreviation
        };

    public override SubjectDetailModel MapToDetailModel(SubjectEntity? entity)
        => entity is null
        ? SubjectDetailModel.Empty
        : new SubjectDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Abbreviation = entity.Abbreviation,
            Activity = activityModelMapper.MapToListModel(entity.Activity).ToObservableCollection(),
            Students = studentSubjectModelMapper.MapToListModel(entity.Students).ToObservableCollection()
        };

    public override SubjectEntity MapDetailModelToEntity(SubjectDetailModel model)
    {
        return new SubjectEntity
        {
            Id = model.Id,
            Name = model.Name,
            Abbreviation = model.Abbreviation,
        };
    }


}
