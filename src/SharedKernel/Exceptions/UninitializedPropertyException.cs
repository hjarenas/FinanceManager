using System.Runtime.CompilerServices;
namespace SharedKernel.Exceptions;
public class UninitializedPropertyException : Exception 
{
    private const string ErrorMessage = "Error accessing the uninitialized property: ";
    public UninitializedPropertyException([CallerMemberName] string propertyName = "")
        : base(ErrorMessage + propertyName)
    {
    }
}