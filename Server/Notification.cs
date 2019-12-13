using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Server
{
    public class Notification
    {
        public static SqlDependency MessChange = new SqlDependency();
        public Notification()
        {
        }
    }
}
