namespace CoursesApi.Dtos;

public record class GetCourseDto(
    int Id,
    string Name,
    string Description,
    int NoOfChapters,
    string InstructorId
);
