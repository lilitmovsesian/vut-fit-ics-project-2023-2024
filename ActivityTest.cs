﻿using ICSProj;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using System.Data;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;

namespace DAL_Tests
{
    public class ActivityTest
    {
        [Fact]
        public async Task Activity_Add_new()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                context.Subjects.Add(subject);
                //context.Activities.Add(entity);
                context.SaveChanges();
            }

            // Assert
            using (var context = new SchoolContext())
            {
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
            }
        }

        [Fact]
        public async Task Activity_Delete()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();
            

            // Assert
            
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                context.Activities.Remove(new_activity);
                context.SaveChanges();
                var deleted_activity = await context.Activities.SingleOrDefaultAsync(i => i.activityId == entity.activityId);
                Assert.Null(deleted_activity);
            }
        }

        [Fact]
        public async Task Activity_Update()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();
            

            // Assert
            
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                new_activity.name = "Seminar";
                context.SaveChanges();
                var updated_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.Equal("Seminar", updated_activity.name);
            }
        }

        [Fact]
        public async Task Rating_Add_To_Activity()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                entity.rating = new RatingEntity
                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();
            

            // Assert
              
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                var new_rating = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(new_rating);
                Assert.Equal(entity.rating.body, new_rating.body);
                Assert.Equal(entity.rating.note, new_rating.note);
                Assert.Equal(entity.rating.studentId, new_rating.studentId);
            }
        }
        [Fact]
        public async Task Rating_Delete_From_Activity()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                
                var rating = new RatingEntity

                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                entity.rating = rating;
                subject.activity.Add(entity);
                context.Subjects.Add(subject);
                context.SaveChanges();
            
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                var new_rating = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(new_rating);
                Assert.Equal(entity.rating.ratingId, new_rating.ratingId);
                Assert.Equal(entity.rating.note, new_rating.note);
                Assert.Equal(entity.rating.studentId, new_rating.studentId);

                context.Rating.Remove(new_rating);
                context.SaveChanges();
                var activity_without_rating = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(activity_without_rating);
                var deleted_rating = await context.Rating.SingleOrDefaultAsync(i => i.ratingId == rating.ratingId);
                Assert.Null(deleted_rating);
            }
        }

        [Fact]
        public async Task Rating_Update()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                
                var rating = new RatingEntity

                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                entity.rating = rating;
                subject.activity.Add(entity);
                context.Subjects.Add(subject);
                context.SaveChanges();
            
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                var new_rating = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(new_rating);
                Assert.Equal(entity.rating.ratingId, new_rating.ratingId);
                Assert.Equal(entity.rating.note, new_rating.note);
                Assert.Equal(entity.rating.studentId, new_rating.studentId);

                new_rating.body = 4;
                context.SaveChanges();
                var updated_rating = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.Equal(4, updated_rating.body);
            }
        }

        [Fact]
        public async Task Activity_Get_Information()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();
                context.Activities.GetAsyncEnumerator();
            

            // Assert
            
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                Assert.Equal(entity.name, new_activity.name);
                Assert.Equal(entity.start, new_activity.start);
                Assert.Equal(entity.end, new_activity.end);
                Assert.Equal(entity.room, new_activity.room);
                Assert.Equal(entity.activityTypeTag, new_activity.activityTypeTag);
                Assert.Equal(entity.description, new_activity.description);
            }
        }

        [Fact]
        public async Task Activity_Get_Rating()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                
                var rating = new RatingEntity

                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                entity.rating = rating;
                subject.activity.Add(entity);
                context.Subjects.Add(subject);
                context.SaveChanges();
            
                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                var new_rating = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(new_rating);
                Assert.Equal(entity.rating.ratingId, new_rating.ratingId);
                Assert.Equal(entity.rating.body, new_rating.body);
                Assert.Equal(entity.rating.note, new_rating.note);
                Assert.Equal(entity.rating.studentId, new_rating.studentId);
            }
        }

        [Fact]
        public async Task Activity_Delete_From_Subject_with_two_activitiese()
        {
            ActivityEntity first_activity, second_activity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                first_activity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                second_activity = new ActivityEntity
                {
                    name = "Exam",
                    start = new DateTime(2021, 12, 1, 8, 0, 0),
                    end = new DateTime(2021, 12, 1, 10, 0, 0),
                    room = "A03",
                    activityTypeTag = "Exam",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(first_activity);
                subject.activity.Add(second_activity);  
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();
                var activity_to_delete = await context.Activities.SingleAsync(i => i.activityId == first_activity.activityId);
                context.Activities.Remove(activity_to_delete);
                context.SaveChanges();
                var deleted_activity = await context.Activities.SingleOrDefaultAsync(i => i.activityId == first_activity.activityId);
                Assert.Null(deleted_activity);
                var remainig_activity = await context.Activities.SingleAsync(i => i.activityId == second_activity.activityId);
                Assert.NotNull(remainig_activity);
                Assert.Equal(second_activity.activityId, remainig_activity.activityId);
            }
        }

        [Fact]
        public async Task Activity_Delete_and_test_its_absence_in_subject()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();


                // Assert

                var new_activity = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(new_activity);
                Assert.Equal(entity.activityId, new_activity.activityId);
                context.Activities.Remove(new_activity);
                context.SaveChanges();
                var deleted_activity = await context.Activities.SingleOrDefaultAsync(i => i.activityId == entity.activityId);
                Assert.Null(deleted_activity);
                var subject_without_activity = await context.Subjects.SingleAsync(i => i.subjectId == subject.subjectId);
                Assert.Empty(subject_without_activity.activity);
            }
        }

        [Fact]
        public async Task Activity_Delete_Subject_with_activities()
        {
            ActivityEntity first_activity, second_activity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                first_activity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                second_activity = new ActivityEntity
                {
                    name = "Exam",
                    start = new DateTime(2021, 12, 1, 8, 0, 0),
                    end = new DateTime(2021, 12, 1, 10, 0, 0),
                    room = "A03",
                    activityTypeTag = "Exam",
                    description = "Introduction to Database Management",
                };

                subject.activity.Add(first_activity);
                subject.activity.Add(second_activity);
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();
                var subject_to_delete = await context.Subjects.SingleAsync(i => i.subjectId == subject.subjectId);
                Assert.NotNull(subject_to_delete);
                var second_activity_was_added = await context.Activities.SingleAsync(i => i.activityId == second_activity.activityId);
                Assert.NotNull(second_activity_was_added);
                var first_activity_was_added = await context.Activities.SingleAsync(i => i.activityId == first_activity.activityId);
                Assert.NotNull(first_activity_was_added);

                context.Subjects.Remove(subject_to_delete);
                context.SaveChanges();

                var deleted_subject = await context.Subjects.SingleOrDefaultAsync(i => i.subjectId == subject.subjectId);
                Assert.Null(deleted_subject);
                var is_first_activity_deleted = await context.Activities.SingleOrDefaultAsync(i => i.activityId == first_activity.activityId);
                Assert.Null(is_first_activity_deleted);
                var is_second_activity_deleted = await context.Activities.SingleOrDefaultAsync(i => i.activityId == second_activity.activityId);
                Assert.Null(is_second_activity_deleted);
            }
        }

        [Fact]
        public async Task Activity_Delete_with_Rating()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                var rating = new RatingEntity

                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                entity.rating = rating;
                subject.activity.Add(entity);
                context.Subjects.Add(subject);
                context.SaveChanges();

                var is_activity_added = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(is_activity_added);
                Assert.Equal(entity.activityId, is_activity_added.activityId);
                var is_rating_added = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(is_rating_added);
                Assert.Equal(entity.rating.ratingId, is_rating_added.ratingId);
            

                context.Activities.Remove(is_activity_added);
                context.SaveChanges();

                var deleted_activity = await context.Activities.SingleOrDefaultAsync(i => i.activityId == entity.activityId);
                Assert.Null(deleted_activity);
                var deleted_rating = await context.Rating.SingleOrDefaultAsync(i => i.ratingId == rating.ratingId);
                Assert.Null(deleted_rating);
            }
        }

        [Fact]
        public async Task Rating_Delete_and_test_its_absece_in_Activity()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                var rating = new RatingEntity

                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                entity.rating = rating;
                subject.activity.Add(entity);
                context.Subjects.Add(subject);
                context.SaveChanges();

                var is_activity_added = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(is_activity_added);
                Assert.Equal(entity.activityId, is_activity_added.activityId);
                var is_rating_added = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(is_rating_added);
                Assert.Equal(entity.rating.ratingId, is_rating_added.ratingId);

                context.Rating.Remove(is_rating_added);
                context.SaveChanges();

                var deleted_rating = await context.Rating.SingleOrDefaultAsync(i => i.ratingId == rating.ratingId);
                Assert.Null(deleted_rating);
                var activity_without_rating = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.Null(activity_without_rating.rating);
            }
        }

        [Fact]
        public async Task ActivityF_Delete_through_Subject()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                context.Subjects.Add(subject);
                context.SaveChanges();

                var is_activity_added = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(is_activity_added);
                Assert.Equal(entity.activityId, is_activity_added.activityId);
                var subject_with_activity = await context.Subjects.SingleAsync(i => i.subjectId == subject.subjectId);
                Assert.NotEmpty(subject_with_activity.activity);
                Assert.Equal(subject_with_activity.subjectId, subject.subjectId);

                subject_with_activity.activity.Remove(is_activity_added);
                context.SaveChanges();

                var deleted_activity = await context.Activities.SingleOrDefaultAsync(i => i.activityId == entity.activityId);
                Assert.Null(deleted_activity);
                var subject_without_activity = await context.Subjects.SingleAsync(i => i.subjectId == subject.subjectId);
                Assert.Empty(subject_without_activity.activity);
            }
        }

        [Fact]
        public async Task Rating_Delete_through_ActivityJI()
        {
            ActivityEntity entity;
            SubjectEntity subject;
            StudentEntity student;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };
                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                var rating = new RatingEntity

                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                entity.rating = rating;
                subject.activity.Add(entity);
                context.Subjects.Add(subject);
                context.SaveChanges();

                var is_activity_added = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.NotNull(is_activity_added);
                Assert.Equal(entity.activityId, is_activity_added.activityId);
                var is_rating_added = await context.Rating.SingleAsync(i => i.ratingId == entity.rating.ratingId);
                Assert.NotNull(is_rating_added);
                Assert.Equal(entity.rating.ratingId, is_rating_added.ratingId);

                is_activity_added.rating = null;
                context.SaveChanges();

                var deleted_rating = await context.Rating.SingleOrDefaultAsync(i => i.ratingId == rating.ratingId);
                Assert.Null(deleted_rating);
                var activity_without_rating = await context.Activities.SingleAsync(i => i.activityId == entity.activityId);
                Assert.Null(activity_without_rating.rating);
            }
        }

        [Fact]
        public async Task Activity_Add_without_Subject()
        {
            ActivityEntity entity;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };
                // Act
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    context.Activities.Add(entity);
                    await context.SaveChangesAsync();
                });

            }
        }

        [Fact]
        public async Task Rating_Add_without_Activity()
        {
            RatingEntity entity;
            StudentEntity student;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };

                // Arrange
                entity = new RatingEntity
                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                // Act
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    context.Rating.Add(entity);
                    await context.SaveChangesAsync();
                });

            }
        }

        [Fact]
        public async Task Activity_Add_with_Rating_without_Student()
        {
            ActivityEntity entity;
            RatingEntity rating;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                subject.activity.Add(entity);


                rating = new RatingEntity
                {
                    body = 5,
                    note = "Great lecture",

                };
                entity.rating = rating;
                // Act
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    context.Subjects.Add(subject);
                    await context.SaveChangesAsync();
                });

            }
        }

        [Fact]
        public async Task Rating_Add_with_Activity_without_Student()
        {
            ActivityEntity entity;
            RatingEntity rating;
            SubjectEntity subject;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Arrange
                entity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                rating = new RatingEntity
                {
                    body = 5,
                    note = "Great lecture",
                };
                entity.rating = rating;

                subject = new SubjectEntity
                {
                    abbreviation = "IDM",
                    name = "Introduction to Database Management",
                };
                subject.activity.Add(entity);
                // Act
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    context.Subjects.Add(subject);
                    await context.SaveChangesAsync();
                });

            }
        }

        [Fact]
        public async Task Rating_Add_without_Subject()
        {
            RatingEntity rating;
            StudentEntity student;
            ActivityEntity activity;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                student = new StudentEntity
                {
                    firstName = "Hermione",
                    lastName = "Granger",
                    fotoURL = "https://upload.wikimedia.org/wikipedia/en/d/d3/Hermione_Granger_poster.jpg",
                };

                activity = new ActivityEntity
                {
                    name = "Lecture",
                    start = new DateTime(2021, 11, 1, 8, 0, 0),
                    end = new DateTime(2021, 11, 1, 10, 0, 0),
                    room = "A1",
                    activityTypeTag = "Lecture",
                    description = "Introduction to Database Management",
                };

                // Arrange
                rating = new RatingEntity
                {
                    body = 5,
                    note = "Great lecture",
                    student = student,
                };
                activity.rating = rating;
                // Act
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    context.Rating.Add(rating);
                    await context.SaveChangesAsync();
                });

            }
        }

        [Fact]
        public async Task Rating_Add_pure()
        {
            RatingEntity entity;
            using (var context = new SchoolContext())
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Arrange
                entity = new RatingEntity
                {
                    body = 5,
                    note = "Great lecture",
                };
                // Act and Assert
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    context.Rating.Add(entity);
                    await context.SaveChangesAsync();
                });
            }
        }
 

    }

}
