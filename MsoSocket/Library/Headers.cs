using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsoSocket.Library
{
    enum PackageType: byte
    {
        Header,
        Text,
        Object,
        Command,
        Notification,
        File,
        Image,
        Sound,
        Video
    }

    enum Headers
    {
        Connect,
        Disconnect,
        Authorization,
        ReConnect
    }
}
