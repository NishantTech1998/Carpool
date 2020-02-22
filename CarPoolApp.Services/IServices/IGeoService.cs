using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApp.Services.IServices
{
    public interface IGeoService
    {
        bool IsCityAvailable(string city);
        double Distance(string source, string destination);
    }
}
