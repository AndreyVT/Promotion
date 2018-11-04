namespace Promotion.Server.Base
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class PBaseController: ControllerBase
    {
    }
}
