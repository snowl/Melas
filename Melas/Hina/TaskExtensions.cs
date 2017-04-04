using System.Threading.Tasks;

// Taken from https://github.com/imiuka/rtmp-sharp/blob/master/src/_Sky/Hina/Threading/Extensions/TaskExtensions.cs
namespace Hina
{
    static class TaskExtensions
    {
        public static void Forget(this Task task) { }
    }
}
