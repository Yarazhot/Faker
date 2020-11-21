using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class DTOGenerator : IGenerator
    {
        private int dtoLevel;
        private const int MAX_DTO_LEVEL = 4;
        private const int DEFAULT_OBJECTS_COUNT_IN_LIST = 3;
        private IGenerator notDTOGenerator;

        public DTOGenerator(IGenerator notDtoGenerator)
        {
            this.notDTOGenerator = notDtoGenerator;
            dtoLevel = 0;
        }

        public object Generate(Type type)
        {
            if (type.GetInterface("IDTO") != null)
            {
                return generateDTO(type);
            }

            if (type.GetInterface("IList") != null)
            {
                return generateList(type);
            }

            return notDTOGenerator.Generate(type);
        }

        private IDTO generateDTO(Type type)
        {
            dtoLevel++;
            if (dtoLevel > MAX_DTO_LEVEL)
            {
                dtoLevel--;
                return null;
            }

            IDTO result = getEmptyDTO(type);
            setupFields(result);
            setupProperties(result);
            dtoLevel--;
            return result;
        }

        private IDTO getEmptyDTO(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                return (IDTO)Activator.CreateInstance(type);
            }

            var constructor = constructors[0];
            var constructorParams = new List<object>();
            foreach (var parameter in constructor.GetParameters())
            {
                var parameterType = parameter.ParameterType;
                constructorParams.Add(this.Generate(parameterType));
            }

            return (IDTO)constructors[0].Invoke(constructorParams.ToArray());
        }

        private void setupFields(object obj)
        {
            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldValue = Generate(field.FieldType);
                field.SetValue(obj, fieldValue);
            }
        }

        private void setupProperties(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    if (property.CanRead)
                    {
                        if (property.GetValue(obj) != null)
                        {
                            continue;
                        }

                        if (property.CanWrite)
                        {
                            property.SetValue(obj, Generate(property.PropertyType));
                        }
                    }
                }
            }
        }

        public IList generateList(Type type)
        {
            var list = (IList)Activator.CreateInstance(type);
            Type objType = type.GetGenericArguments()[0];
            for (int i = 0; i < DEFAULT_OBJECTS_COUNT_IN_LIST; i++)
            {
                var value = Generate(objType);
                if (value != null)
                    list.Add(value);
            }

            return list;
        }
    }
}
