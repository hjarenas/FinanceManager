using System.Reflection;
using AutoMapper;
namespace FinanceManager.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mappingInterfacesInfo = new [] 
            {
                (InterfaceType: typeof(IMapFrom<>), MappingMethodName: nameof(IMapFrom<object>.MappingFrom)),
                (InterfaceType: typeof(IMapTo<>), MappingMethodName: nameof(IMapTo<object>.MappingTo)) 
            };
        foreach(var mappingInterfaceInfo in mappingInterfacesInfo)
        {
            ApplyMappingsForType(assembly, mappingInterfaceInfo);
        }
    }
    bool HasInterface(Type t, Type mapFromType) => 
        t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
    private void ApplyMappingsForType(
        Assembly assembly, 
        (Type InterfaceType, string MappingMethodName) mappingInterfaceInfo)
    {
        
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => HasInterface(i, mappingInterfaceInfo.InterfaceType)))
            .ToList();
        var argumentTypes = new Type[] { typeof(Profile) };
        foreach (var type in types)
        {
            InvokeProfiles(mappingInterfaceInfo, argumentTypes, type);
        }
    }

    private void InvokeProfiles(
        (Type InterfaceType, string MappingMethodName) mappingInterfaceInfo, 
        Type[] argumentTypes, 
        Type type)
    {
        var instance = Activator.CreateInstance(type);
        var methodInfo = type.GetMethod(mappingInterfaceInfo.MappingMethodName);
        if (methodInfo != null)
        {
            methodInfo.Invoke(instance, new object[] { this });
        }
        else
        {
            var interfaces = type.GetInterfaces()
                .Where(i => HasInterface(i, mappingInterfaceInfo.InterfaceType))
                .ToList();
            if (interfaces.Count > 0)
            {
                foreach (var @interface in interfaces)
                {
                    var interfaceMethodInfo = @interface.GetMethod(
                        mappingInterfaceInfo.MappingMethodName, 
                        argumentTypes);
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}