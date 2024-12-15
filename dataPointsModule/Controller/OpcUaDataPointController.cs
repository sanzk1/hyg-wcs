using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dataPointsModule.Controller;


[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class OpcUaDataPointController  : ControllerBase
{
    
}