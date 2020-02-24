using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApp.Services.IServices
{
    public interface IService:IBookingService,IGeoService,IRideService,IUserService
    {
    }
}
