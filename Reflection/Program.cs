using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;

namespace Reflection
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class CustomAttributte : Attribute 
    {
        public CustomAttributte( bool isNeeded = true)
        {
            IsNeedSerialize = isNeeded;
        }
        public bool IsNeedSerialize { get; set; }
    }

    public class CustomAttributte2 : Attribute
    {
        public CustomAttributte2(bool isNeeded = true)
        {
            IsNeedSerialize2 = isNeeded;
        }
        public bool IsNeedSerialize2 { get; set; }
    }


    public class Parent
    {
        [CustomAttributte(true)]
        [CustomAttributte2(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public List<Child> Childs { get; set; } = new List<Child>(100);
        public Child Child1 { get; set; } = new Child() { Name = "Child"};
    }
    public class Child
    {
        [CustomAttributte(true)]
        [CustomAttributte2(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }

        class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom(@"C:\Users\Admin\source\repos\Reflection\Sample\bin\Debug\Sample.dll");
            foreach(Type type in assembly.GetTypes())
            {
                // Type
                Console.WriteLine($"Type: {type.Name}");

                object instance = Activator.CreateInstance(type);

                // Field
                Console.WriteLine("==========================================");

                foreach(FieldInfo field in type.GetFields( BindingFlags.Public |
                                                           BindingFlags.NonPublic | 
                                                           BindingFlags.Instance  |
                                                           BindingFlags.DeclaredOnly))
                {
                    Console.WriteLine($"Field: {field.Name}");

                    // Set value
                    field.SetValue(instance, "DuyNT");
                }

                // Method
                Console.WriteLine("==========================================");

                foreach(MethodInfo method in type.GetMethods( BindingFlags.Public    | 
                                                              BindingFlags.NonPublic | 
                                                              BindingFlags.Instance  |
                                                              BindingFlags.Static    |
                                                              BindingFlags.DeclaredOnly
                                                              ).Where(m => !m.IsSpecialName))
                {
                    Console.WriteLine($"Method: {method.Name}");
                    if(method.GetParameters().Length != 0)
                    {
                        method.Invoke(instance, new object[] { "DuyNT2" });
                        continue;
                    }
                    if(method.ReturnType != typeof(void))
                    {
                        Console.WriteLine("Method return value: " + method.Invoke(instance, null) + "\n");
                    }
                    else
                    {
                        method.Invoke(instance, null);
                    }

                }

                // Property
                Console.WriteLine("==========================================");

                foreach(PropertyInfo property in type.GetProperties( BindingFlags.Public |
                                                                     BindingFlags.Instance ))
                {
                    Console.WriteLine($"Property: {property.Name}");
                    property.SetValue(instance, "Nguyen Thai Duy");
                    Console.WriteLine($"Property value: {property.GetValue(instance)}");
                }

                // Attribute
                Console.WriteLine("==========================================");
                foreach(Attribute attribute in type.GetCustomAttributes())
                {
                    Console.WriteLine($"Attribute: {attribute.GetType().Name}");
                }


            }
            Console.ReadKey();
        }
    }
}
