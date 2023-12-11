using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Todo;

[Route("api/[controller]")]
[ApiController]
public class TodoController:ControllerBase
{
    private ResponseDto _response; //create an instance of response dto
    private readonly IMapper _mapper;
    public TodoController(IMapper mapper)
    {
        //Initialize response dto
        _response = new ResponseDto(); //We create an instance by ourselves
        _mapper=mapper; //An instance in created for us and we save it in a variable
        
    }

    private static List<Todo> Tasks= new List<Todo>()
    {
        new Todo(){Task="Learn C#", Start="18:00 PM", Duration="1hr 30min"},
        new Todo(){Task="Learn Python", Start="20:00 PM", Duration="1 Hour"},
        new Todo(){Task="Learn Angular", Start="22:00 PM", Duration="45 Minutes"}

    };

    [HttpGet]
    public ActionResult<ResponseDto> getAllTasks(){
        _response.Result=Tasks;
        _response.StatusCode=HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{Id}")]
    public ActionResult<ResponseDto> GetOneTask(Guid Id){
        var Task=Tasks.Find(k=>k.TaskId==Id);
        if(Task == null){
            _response.Result=null;
            _response.Message="Task not found";
            _response.StatusCode=HttpStatusCode.NotFound;
            return NotFound(_response);
        
        }
        _response.Result=Task;
        return Ok(_response);

    }

    [HttpPost]
    public ActionResult<ResponseDto> AddTask(AddTaskDto newTask){
        // var newTodo= new Todo(){
        //     Task=newTask.Task,
        //     Start=newTask.Start,
        //     Duration=newTask.Duration

        // };
        var newTodo=_mapper.Map<Todo>(newTask);
        // _response.Result=newTodo;
        _response.Result="New task added";
        _response.StatusCode= HttpStatusCode.Created;
        Tasks.Add(newTodo);
        return Created($"api/Todo/{newTodo.TaskId}",_response);
    }

    //Update
    [HttpPut("{Id}")]
    public ActionResult<ResponseDto> UpdateTask(Guid Id, AddTaskDto UpdatedTask){
        var Task=Tasks.Find(k=>k.TaskId==Id);
        //check if task exists
        if(Task == null){
            _response.Result=null;
            _response.Message="Task not found";
            _response.StatusCode=HttpStatusCode.NotFound;
            return NotFound(_response);
        
        }
        //Update the task
        // Task.Task=UpdatedTask.Task;
        // Task.Start=UpdatedTask.Start;
        // Task.Duration=UpdatedTask.Duration;

        //Using automapper
        _mapper.Map(UpdatedTask,Task);

        _response.Result=Task;
        return Ok(_response);

    }

    //Delete Task

    [HttpDelete("{Id}")]

    public ActionResult<ResponseDto> DeleteTask(Guid Id){
        var Task=Tasks.Find(k=>k.TaskId==Id);

        //Check if Task exists
        if(Task == null){
            _response.Result=null;
            _response.Message="Task not found";
            _response.StatusCode=HttpStatusCode.NotFound;
            return NotFound(_response);
        
        }
        Tasks.Remove(Task);
        _response.Result="Deleted";
        _response.Message="Task deleted successfully";
        _response.StatusCode=HttpStatusCode.NoContent;
        return NoContent();

    }

}
