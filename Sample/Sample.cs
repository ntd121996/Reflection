using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Sample
{
    [Serializable]
    class Sample
    {
        private string name;

        public string Name {
            get { return this.name; }
            set { this.name = value; }
        }

        public void PublicPrint()
        {
            Console.WriteLine("Invoke: Public Print method\n");
        }

        public void PrintParam(string param)
        {
            Console.WriteLine($"Invoke: Print param: {param}\n");
        }

        public string GetName()
        {
            return this.name;
        }

        private void PrivatePrint()
        {
            Console.WriteLine("Invoke: Private Print method\n");
        }
        static private void StaticPrint()
        {
            Console.WriteLine("Invoke: Static Print method\n");
        }

    }
}
