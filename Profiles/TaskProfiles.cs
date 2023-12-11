using AutoMapper;

namespace Todo;

public class TaskProfiles:Profile
{
    public TaskProfiles()
    {
        CreateMap<AddTaskDto, Todo>().ReverseMap(); 
        //CreateMap<Todo, AddTaskDto>();
    }

}
