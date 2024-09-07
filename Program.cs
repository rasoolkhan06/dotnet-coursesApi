using CoursesApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

List<GetCourseDto> courses = [
    
    new (
        1,
        "Node Backend Development",
        "This is a demo course",
        20,
        "1"
    ),
    new (
        2,
        "React Development",
        "This is a Full course",
        10,
        "2"
    ),
    new (
        3,
        "Java with OOP Internship Bootcamp",
        "This is a Full course",
        20,
        "2"
    )
];

app.MapGet("/", () => "Hello World!");

app.MapGet("courses", () => courses);

app.MapGet("courses/{id:int}", (int id) =>
{
    return courses.Find(course => course.Id == id);
}).WithName("GetCourses");

app.MapPost("courses", (CreateCourseDto newCourse) =>
{
    int id = courses.Count + 1;
    GetCourseDto course = new(id, newCourse.Name, newCourse.Description, newCourse.NoOfChapters,
        newCourse.InstructorId);
    
    courses.Add(course);
    
    return Results.CreatedAtRoute("GetCourses", new { id = id }, course);
});

app.MapPut("courses/{id}",(int id, CreateCourseDto updatedCourse) =>{
    GetCourseDto? currCourse = courses.Find(course=> course.Id == id);
    if(currCourse == null){
        return Results.NotFound();
    }
    GetCourseDto newCourse = new(
        id,
        updatedCourse.Name,
        updatedCourse.Description,
        updatedCourse.NoOfChapters,
        updatedCourse.InstructorId
        
    );
    courses[id-1] = newCourse;
    return Results.Ok();
});

app.MapDelete("courses/{id}",(int id)=> {
    int courseId = courses.FindIndex(course => course.Id == id);
    if(courseId == -1){
        return Results.NoContent();
    }
    courses.RemoveAt(id-1);
    return Results.NoContent();
});

app.UseHttpsRedirection();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
