namespace CoursesApi.Dtos;

public record class CreateCourseDto(
    string Name, 
    string Description,
    int NoOfChapters,
    string InstructorId
);